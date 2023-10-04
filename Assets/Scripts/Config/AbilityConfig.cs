using System.Collections.Generic;
using UnityEngine;
using static ProjectSurvivor.AbilityPower;

namespace ProjectSurvivor
{
    [CreateAssetMenu]
    public class AbilityConfig : ScriptableObject
    {
        public string Name;

        [TextArea]
        [Header("说明")]
        public string Description = string.Empty;

        [Header("初始值")]
        public float InitDamage = 1f;
        public float InitSpeed = 2f;
        public float InitDuration = 1.5f;
        public float InitRange = 3f;
        public int InitCount = 3;
        public int InitAttackCount = 1;
        public float InitPercent = 0.1f;

        [Header("升级值（调整顺序）")]
        public List<AbilityPower> Powers = new List<AbilityPower>();
    }

    [System.Serializable]
    public struct PowerData
    {
        public PowerType Type;
        public float Value;
    }

    [System.Serializable]
    public class AbilityPower
    {
        public enum PowerType
        {
            Damage,
            Speed,
            Duration,
            Range,
            Count,
            AttackCount,
            Percent,
        }

        public string Lv;
        public PowerData[] PowerDatas = new PowerData[2];

        public void AddNewPowerType()
        {
            List<PowerData> powerDataList = new List<PowerData>(PowerDatas)
            {
                new PowerData { Type = PowerType.Damage, Value = 0 }
            };

            PowerDatas = powerDataList.ToArray();
        }

        public void RemoveLastPowerType()
        {
            if (PowerDatas.Length > 0)
            {
                List<PowerData> powerDataList = new List<PowerData>(PowerDatas);
                powerDataList.RemoveAt(powerDataList.Count - 1);
                PowerDatas = powerDataList.ToArray();
            }
        }

        public string GetPowerUpInfo(string name)
        {
            string info = $"{name} Lv{Lv}：\n";

            foreach (var data in PowerDatas)
            {
                string powerTypeStr = "";

                switch (data.Type)
                {
                    case PowerType.Damage:
                        powerTypeStr = "攻击力";
                        break;
                    case PowerType.Speed:
                        powerTypeStr = "速度";
                        break;
                    case PowerType.Duration:
                        powerTypeStr = "间隔";
                        break;
                    case PowerType.Range:
                        powerTypeStr = "范围";
                        break;
                    case PowerType.Count:
                        powerTypeStr = "数量";
                        break;
                    case PowerType.AttackCount:
                        powerTypeStr = "攻击数";
                        break;
                    case PowerType.Percent:
                        powerTypeStr = "概率/附加值";
                        break;
                }

                switch (data.Type)
                {
                    case PowerType.Damage:
                    case PowerType.Speed:
                    case PowerType.Duration:
                    case PowerType.Range:
                    case PowerType.Count:
                    case PowerType.AttackCount:
                        info += $"{powerTypeStr}+{data.Value} ";
                        break;
                    case PowerType.Percent:
                        info += powerTypeStr + "+" + (data.Value * 100).ToString("0") + "% ";
                        break;
                    default:
                        break;
                }
            }

            return info.Trim();
        }
    }
}
