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
            AbilityConfig simpleKnifeConfig = Player.Default.SimpleKnifeConfig;
            AbilityConfig rotateSwordConfig = Player.Default.RotateSwordConfig;

            Add(new ExpUpgradeItem()
                .WithKey("simple_sword")
                .WithDescription(lv =>
                {
                    if (lv == 1)
                        return $"{simpleSwordConfig.Name} Lv1：" + simpleSwordConfig.Description;

                    for (int i = 2; i < simpleSwordConfig.Powers.Count + 1; i++)
                    {
                        if (lv == i)
                            return simpleSwordConfig.Powers[lv - 1].GetPowerUpInfo(simpleSwordConfig.Name);
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
                                        Debug.Log("升级速度 " + powerData.Value);
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
                                        Debug.Log("升级攻击数 " + powerData.Value);
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }));

            Add(new ExpUpgradeItem()
                .WithKey("simple_knife")
                .WithDescription(lv =>
                {
                    if (lv == 1)
                        return $"{simpleKnifeConfig.Name} Lv1：" + simpleKnifeConfig.Description;

                    for (int i = 2; i < simpleKnifeConfig.Powers.Count + 1; i++)
                    {
                        if (lv == i)
                            return simpleKnifeConfig.Powers[lv - 1].GetPowerUpInfo(simpleKnifeConfig.Name);
                    }

                    return "未知等级";
                })
                .WithMaxLevel(simpleKnifeConfig.Powers.Count)
                .OnUpgrade((_, lv) =>
                {
                    for (int i = 1; i < simpleKnifeConfig.Powers.Count + 1; i++)
                    {
                        if (lv == i)
                        {
                            Debug.Log("当前升级：" + simpleKnifeConfig.Name + simpleKnifeConfig.Powers[lv - 1].Lv);

                            foreach (PowerData powerData in simpleKnifeConfig.Powers[lv - 1].PowerDatas)
                            {
                                switch (powerData.Type)
                                {
                                    case AbilityPower.PowerType.Damage:
                                        Global.SimpleKnifeDamage.Value += powerData.Value;
                                        Debug.Log("升级攻击力 " + powerData.Value);
                                        break;

                                    case AbilityPower.PowerType.Speed:
                                        Debug.Log("升级速度 " + powerData.Value);
                                        break;

                                    case AbilityPower.PowerType.Duration:
                                        Global.SimpleKnifeDuration.Value += powerData.Value;
                                        Debug.Log("升级间隔 " + powerData.Value);
                                        break;

                                    case AbilityPower.PowerType.Range:
                                        Debug.Log("升级范围 " + powerData.Value);
                                        break;

                                    case AbilityPower.PowerType.Count:
                                        Global.SimpleKnifeCount.Value += (int)powerData.Value;
                                        Debug.Log("升级数量 " + powerData.Value);
                                        break;

                                    case AbilityPower.PowerType.AttackCount:
                                        Global.SimpleKnifeAttackCount.Value += (int)powerData.Value;
                                        Debug.Log("升级攻击数 " + powerData.Value);
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }));

            Add(new ExpUpgradeItem()
                .WithKey("rotate_sword")
                .WithDescription(lv =>
                {
                    if (lv == 1)
                        return $"{rotateSwordConfig.Name} Lv1：" + rotateSwordConfig.Description;

                    for (int i = 2; i < rotateSwordConfig.Powers.Count + 1; i++)
                    {
                        if (lv == i)
                            return rotateSwordConfig.Powers[lv - 1].GetPowerUpInfo(rotateSwordConfig.Name);
                    }

                    return "未知等级";
                })
                .WithMaxLevel(rotateSwordConfig.Powers.Count)
                .OnUpgrade((_, lv) =>
                {
                    for (int i = 1; i < rotateSwordConfig.Powers.Count + 1; i++)
                    {
                        if (lv == i)
                        {
                            Debug.Log("当前升级：" + rotateSwordConfig.Name + rotateSwordConfig.Powers[lv - 1].Lv);

                            foreach (PowerData powerData in rotateSwordConfig.Powers[lv - 1].PowerDatas)
                            {
                                switch (powerData.Type)
                                {
                                    case AbilityPower.PowerType.Damage:
                                        Global.RotateSwordDamage.Value += powerData.Value;
                                        Debug.Log("升级攻击力 " + powerData.Value);
                                        break;

                                    case AbilityPower.PowerType.Speed:
                                        Global.RotateSwordSpeed.Value += powerData.Value;
                                        Debug.Log("升级速度 " + powerData.Value);
                                        break;

                                    case AbilityPower.PowerType.Duration:
                                        Debug.Log("升级间隔 " + powerData.Value);
                                        break;

                                    case AbilityPower.PowerType.Range:
                                        Global.RotateSwordRange.Value += powerData.Value;
                                        Debug.Log("升级范围 " + powerData.Value);
                                        break;

                                    case AbilityPower.PowerType.Count:
                                        Global.RotateSwordCount.Value += (int)powerData.Value;
                                        Debug.Log("升级数量 " + powerData.Value);
                                        break;

                                    case AbilityPower.PowerType.AttackCount:
                                        Debug.Log("升级攻击数 " + powerData.Value);
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

            foreach( ExpUpgradeItem item in Items.Where(item => !item.UpgradeFinish).Take(3))
            {
                if (item == null)
                {
                    Debug.LogError("没有可用的升级项");
                    return;
                }

                item.Visible.Value = true;
            }
        }
    }
}
