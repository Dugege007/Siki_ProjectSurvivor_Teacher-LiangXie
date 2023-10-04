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

        private AbilityConfig mSimpleSwordConfig;
        private AbilityConfig mSimpleKnifeConfig;
        private AbilityConfig mRotateSwordConfig;
        private AbilityConfig mBasketballConfig;
        private AbilityConfig mSimpleBombConfig;
        private AbilityConfig mCriticalRateConfig;

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

            mSimpleSwordConfig = Player.Default.SimpleSwordConfig;
            mSimpleKnifeConfig = Player.Default.SimpleKnifeConfig;
            mRotateSwordConfig = Player.Default.RotateSwordConfig;
            mBasketballConfig = Player.Default.BasketballConfig;
            mSimpleBombConfig = Player.Default.SimpleBombConfig;
            mCriticalRateConfig = Player.Default.CriticalRateConfig;

            // 剑
            Add(new ExpUpgradeItem(true)
                .WithKey("simple_sword")
                .WithDescription(lv =>
                {
                    return UpgradeDiscription(lv, mSimpleSwordConfig);
                })
                .WithMaxLevel(mSimpleSwordConfig.Powers.Count)
                .OnUpgrade((_, lv) =>
                {
                    UpgradePowerValue(lv, mSimpleSwordConfig);
                }));

            // 炸弹
            Add(new ExpUpgradeItem()
                .WithKey("simple_bomb")
                .WithDescription(lv =>
                {
                    return UpgradeDiscription(lv, mSimpleBombConfig);
                })
                .WithMaxLevel(mSimpleBombConfig.Powers.Count)
                .OnUpgrade((_, lv) =>
                {
                    UpgradePowerValue(lv, mSimpleBombConfig);
                }));

            // 飞刀
            Add(new ExpUpgradeItem(true)
                .WithKey("simple_knife")
                .WithDescription(lv =>
                {
                    return UpgradeDiscription(lv, mSimpleKnifeConfig);
                })
                .WithMaxLevel(mSimpleKnifeConfig.Powers.Count)
                .OnUpgrade((_, lv) =>
                {
                    UpgradePowerValue(lv, mSimpleKnifeConfig);
                }));

            // 守卫剑
            Add(new ExpUpgradeItem(true)
                .WithKey("rotate_sword")
                .WithDescription(lv =>
                {
                    return UpgradeDiscription(lv, mRotateSwordConfig);
                })
                .WithMaxLevel(mRotateSwordConfig.Powers.Count)
                .OnUpgrade((_, lv) =>
                {
                    UpgradePowerValue(lv, mRotateSwordConfig);
                }));

            // 篮球
            Add(new ExpUpgradeItem(true)
               .WithKey("basketball")
               .WithDescription(lv =>
               {
                   return UpgradeDiscription(lv, mBasketballConfig);
               })
               .WithMaxLevel(mBasketballConfig.Powers.Count)
               .OnUpgrade((_, lv) =>
               {
                   UpgradePowerValue(lv, mBasketballConfig);
               }));

            // 暴击率
            Add(new ExpUpgradeItem()
               .WithKey("simple_critical")
               .WithDescription(lv =>
               {
                   return UpgradeDiscription(lv, mCriticalRateConfig);
               })
               .WithMaxLevel(mCriticalRateConfig.Powers.Count)
               .OnUpgrade((_, lv) =>
               {
                   UpgradePowerValue(lv, mCriticalRateConfig);
               }));
        }

        private string UpgradeDiscription(int lv, AbilityConfig abilityConfig)
        {
            if (lv == 1)
                return $"{abilityConfig.Name} Lv1:" + "\n" + abilityConfig.Description;

            for (int i = 2; i < abilityConfig.Powers.Count + 1; i++)
            {
                if (lv == i)
                    return abilityConfig.Powers[lv - 1].GetPowerUpInfo(abilityConfig.Name);
            }

            // 不加这行会报错
            return "未知等级";
        }

        private void UpgradePowerValue(int lv, AbilityConfig abilityConfig)
        {
            // 解锁
            if (lv == 1)
            {
                if (abilityConfig.Name == mSimpleSwordConfig.Name)
                    Global.SimpleSwordUnlocked.Value = true;
                else if (abilityConfig.Name == mSimpleBombConfig.Name)
                    Global.SimpleBombUnlocked.Value = true;
                else if (abilityConfig.Name == mSimpleKnifeConfig.Name)
                    Global.SimpleKnifeUnlocked.Value = true;
                else if (abilityConfig.Name == mRotateSwordConfig.Name)
                    Global.RotateSwordUnlocked.Value = true;
                else if (abilityConfig.Name == mBasketballConfig.Name)
                    Global.BasketballUnlocked.Value = true;
            }

            // 升级
            for (int i = 1; i < abilityConfig.Powers.Count + 1; i++)
            {
                if (lv == i)
                {
                    Debug.Log("当前升级：" + abilityConfig.Name + abilityConfig.Powers[lv - 1].Lv);

                    foreach (PowerData powerData in abilityConfig.Powers[lv - 1].PowerDatas)
                    {
                        // 剑
                        if (abilityConfig.Name == mSimpleSwordConfig.Name)
                        {
                            switch (powerData.Type)
                            {
                                case AbilityPower.PowerType.Damage:
                                    Global.SimpleSwordDamage.Value += powerData.Value;
                                    break;
                                case AbilityPower.PowerType.Duration:
                                    Global.SimpleSwordDuration.Value += powerData.Value;
                                    break;
                                case AbilityPower.PowerType.Range:
                                    Global.SimpleSwordRange.Value += powerData.Value;
                                    break;
                                case AbilityPower.PowerType.Count:
                                    Global.SimpleSwordCount.Value += (int)powerData.Value;
                                    break;
                                default:
                                    break;
                            }
                        }

                        // 炸弹
                        if (abilityConfig.Name == mSimpleBombConfig.Name)
                        {
                            switch (powerData.Type)
                            {
                                case AbilityPower.PowerType.Damage:
                                    Global.SimpleBombDamage.Value += powerData.Value;
                                    break;
                                case AbilityPower.PowerType.Percent:
                                    Global.SimpleBombPercent.Value += powerData.Value;
                                    break;
                                default:
                                    break;
                            }
                        }

                        // 飞刀
                        if (abilityConfig.Name == mSimpleKnifeConfig.Name)
                        {
                            switch (powerData.Type)
                            {
                                case AbilityPower.PowerType.Damage:
                                    Global.SimpleKnifeDamage.Value += powerData.Value;
                                    break;
                                case AbilityPower.PowerType.Duration:
                                    Global.SimpleKnifeDuration.Value += powerData.Value;
                                    break;
                                case AbilityPower.PowerType.Count:
                                    Global.SimpleKnifeCount.Value += (int)powerData.Value;
                                    break;
                                case AbilityPower.PowerType.AttackCount:
                                    Global.SimpleKnifeAttackCount.Value += (int)powerData.Value;
                                    break;
                                default:
                                    break;
                            }
                        }

                        // 守卫剑
                        if (abilityConfig.Name == mRotateSwordConfig.Name)
                        {
                            switch (powerData.Type)
                            {
                                case AbilityPower.PowerType.Damage:
                                    Global.RotateSwordDamage.Value += powerData.Value;
                                    break;
                                case AbilityPower.PowerType.Speed:
                                    Global.RotateSwordSpeed.Value += powerData.Value;
                                    break;
                                case AbilityPower.PowerType.Range:
                                    Global.RotateSwordRange.Value += powerData.Value;
                                    break;
                                case AbilityPower.PowerType.Count:
                                    Global.RotateSwordCount.Value += (int)powerData.Value;
                                    break;
                                default:
                                    break;
                            }
                        }

                        // 篮球
                        if (abilityConfig.Name == mBasketballConfig.Name)
                        {
                            switch (powerData.Type)
                            {
                                case AbilityPower.PowerType.Damage:
                                    Global.BasketballDamage.Value += powerData.Value;
                                    break;
                                case AbilityPower.PowerType.Speed:
                                    Global.BasketballSpeed.Value += powerData.Value;
                                    break;
                                case AbilityPower.PowerType.Count:
                                    Global.BasketballCount.Value += (int)powerData.Value;
                                    break;
                                default:
                                    break;
                            }
                        }

                        // 暴击率
                        if (abilityConfig.Name == mCriticalRateConfig.Name)
                        {
                            switch (powerData.Type)
                            {
                                case AbilityPower.PowerType.Percent:
                                    Global.CriticalRate.Value += powerData.Value;
                                    break;
                                default:
                                    break;
                            }
                        }

                        Debug.Log($"升级{powerData.Type} " + powerData.Value);
                    }
                }
            }
        }

        public void Roll()
        {
            if (Items.Count >= 0)
            {
                foreach (ExpUpgradeItem expUpgradeItem in Items)
                {
                    expUpgradeItem.Visible.Value = false;
                }
            }

            // 随机取几个可升级的能力
            List<ExpUpgradeItem> list = Items.Where(item => !item.UpgradeFinish).ToList();
            if (list.Count >= 4)
            {
                list.GetAndRemoveRandomItem().Visible.Value = true;
                list.GetAndRemoveRandomItem().Visible.Value = true;
                list.GetAndRemoveRandomItem().Visible.Value = true;
                list.GetAndRemoveRandomItem().Visible.Value = true;
            }
            else if (list.Count > 0)
            {
                foreach (ExpUpgradeItem item in list)
                {
                    item.Visible.Value = true;
                }
            }
            else
            {
                Debug.LogError("没有可用的升级项");
                return;
            }
        }
    }
}
