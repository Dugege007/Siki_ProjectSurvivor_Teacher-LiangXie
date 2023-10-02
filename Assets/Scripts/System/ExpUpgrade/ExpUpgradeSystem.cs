using UnityEngine;
using QFramework;
using System.Collections.Generic;
using System.Linq;

namespace ProjectSurvivor
{
    public class ExpUpgradeSystem : AbstractSystem
    {
        public static EasyEvent OnCoinUpgradeSystemChanged = new EasyEvent();

        public List<ExpUpgradeItem> Items { get; } = new List<ExpUpgradeItem>();

        protected override void OnInit()
        {
            ResetData();

            Global.Level.Register(_ =>
            {
                Roll();
            });
        }

        public ExpUpgradeItem Add(ExpUpgradeItem item)
        {
            Items.Add(item);
            return item;
        }

        public void ResetData()
        {
            Items.Clear();

            AbilityConfig simpleSwordConfig = Player.Default.SimpleSwordConfig;

            Add(new ExpUpgradeItem()
                .WithKey("simple_sword")
                .WithDescription(lv =>
                {
                    if (lv == 1)
                        return $"剑 Lv1：" + simpleSwordConfig.Description;

                    for (int i = 2; i < simpleSwordConfig.Powers.Count + 1; i++)
                    {
                        if (lv == i)
                            return simpleSwordConfig.Powers[lv - 1].GetPowerUpInfo();
                    }

                    return "未知等级";
                })
                .WithMaxLevel(simpleSwordConfig.Powers.Count)
                .OnUpgrade((_, lv) =>
                {
                    for (int i = 1; i < simpleSwordConfig.Powers.Count + 1; i++)
                    {
                        if (lv == i)
                        {
                            Debug.Log("当前升级：" + simpleSwordConfig.Name + simpleSwordConfig.Powers[lv - 1].Lv);

                            foreach (PowerData powerData in simpleSwordConfig.Powers[lv - 1].PowerDatas)
                            {
                                switch (powerData.Type)
                                {
                                    case AbilityPower.PowerType.Damage:
                                        Global.SimpleSwordDamage.Value += powerData.Value;
                                        Debug.Log("升级攻击力 " + powerData.Value);
                                        break;

                                    case AbilityPower.PowerType.Speed:
                                        break;

                                    case AbilityPower.PowerType.Duration:
                                        Global.SimpleSwordDuration.Value += powerData.Value;
                                        Debug.Log("升级间隔 " + powerData.Value);
                                        break;

                                    case AbilityPower.PowerType.Range:
                                        Global.SimpleSwordRange.Value += powerData.Value;
                                        Debug.Log("升级范围 " + powerData.Value);
                                        break;

                                    case AbilityPower.PowerType.Count:
                                        Global.SimpleSwordCount.Value += (int)powerData.Value;
                                        Debug.Log("升级数量 " + powerData.Value);
                                        break;

                                    case AbilityPower.PowerType.AttackCount:
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }));
        }

        public void UpgradePowerValue()
        {

        }

        public void Roll()
        {
            if (Items.Count >= 1)
            {
                foreach (ExpUpgradeItem expUpgradeItem in Items)
                {
                    expUpgradeItem.Visible.Value = false;
                }
            }

            ExpUpgradeItem item = Items.Where(item => item.UpgradeFinish == false)
                .ToList()
                .GetRandomItem();   // 获取随机的单位

            Debug.Log(item.Key);

            if (item == null)
            {
                Debug.LogError("没有可用的升级项");
                return;
            }

            item.Visible.Value = true;
        }
    }
}
