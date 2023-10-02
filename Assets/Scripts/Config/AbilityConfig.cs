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

        [Header("升级值（调整顺序、添加升级项）")]
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
        }

        public string Lv;
        public PowerData[] PowerDatas = new PowerData[2];

        public string GetPowerUpInfo()
        {
            string info = $"Lv{Lv}：\n";

            foreach (var data in PowerDatas)
            {
                string powerType = "";

                switch (data.Type)
                {
                    case PowerType.Damage:
                        powerType = "攻击力";
                        break;
                    case PowerType.Speed:
                        powerType = "速度";
                        break;
                    case PowerType.Duration:
                        powerType = "间隔";
                        break;
                    case PowerType.Range:
                        powerType = "范围";
                        break;
                    case PowerType.Count:
                        powerType = "数量";
                        break;
                    case PowerType.AttackCount:
                        powerType = "攻击数";
                        break;
                }

                info += $"{powerType}+{data.Value} ";
            }

            return info.Trim();
        }
    }
}
