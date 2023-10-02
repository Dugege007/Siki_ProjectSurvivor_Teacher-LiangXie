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

            Add(new ExpUpgradeItem()
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

            Add(new ExpUpgradeItem()
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

            Add(new ExpUpgradeItem()
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

            Add(new ExpUpgradeItem()
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
        }

        private string UpgradeDiscription(int lv, AbilityConfig abilityConfig)
        {
            if (lv == 1)
                return $"{abilityConfig.Name} Lv1：" + abilityConfig.Description;

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
            for (int i = 1; i < abilityConfig.Powers.Count + 1; i++)
            {
                if (lv == i)
                {
                    Debug.Log("当前升级：" + abilityConfig.Name + abilityConfig.Powers[lv - 1].Lv);

                    foreach (PowerData powerData in abilityConfig.Powers[lv - 1].PowerDatas)
                    {
                        switch (powerData.Type)
                        {
                            case AbilityPower.PowerType.Damage:
                                if (abilityConfig.Name == mSimpleSwordConfig.Name)
                                    Global.SimpleSwordDamage.Value += powerData.Value;
                                else if (abilityConfig.Name == mSimpleKnifeConfig.Name)
                                    Global.SimpleKnifeDamage.Value += powerData.Value;
                                else if (abilityConfig.Name == mRotateSwordConfig.Name)
                                    Global.RotateSwordDamage.Value += powerData.Value;
                                else if (abilityConfig.Name == mBasketballConfig.Name)
                                    Global.BasketballDamage.Value += powerData.Value;
                                break;

                            case AbilityPower.PowerType.Speed:
                                if (abilityConfig.Name == mRotateSwordConfig.Name)
                                    Global.RotateSwordSpeed.Value += powerData.Value;
                                else if (abilityConfig.Name == mBasketballConfig.Name)
                                    Global.BasketballSpeed.Value += powerData.Value;
                                break;

                            case AbilityPower.PowerType.Duration:
                                if (abilityConfig.Name == mSimpleSwordConfig.Name)
                                    Global.SimpleSwordDuration.Value += powerData.Value;
                                else if (abilityConfig.Name == mSimpleKnifeConfig.Name)
                                    Global.SimpleKnifeDuration.Value += powerData.Value;
                                break;

                            case AbilityPower.PowerType.Range:
                                if (abilityConfig.Name == mSimpleSwordConfig.Name)
                                    Global.SimpleSwordRange.Value += powerData.Value;
                                else if (abilityConfig.Name == mRotateSwordConfig.Name)
                                    Global.RotateSwordRange.Value += powerData.Value;
                                break;

                            case AbilityPower.PowerType.Count:
                                if (abilityConfig.Name == mSimpleSwordConfig.Name)
                                    Global.SimpleSwordCount.Value += (int)powerData.Value;
                                else if (abilityConfig.Name == mSimpleKnifeConfig.Name)
                                    Global.SimpleKnifeCount.Value += (int)powerData.Value;
                                else if (abilityConfig.Name == mRotateSwordConfig.Name)
                                    Global.RotateSwordCount.Value += (int)powerData.Value;
                                else if (abilityConfig.Name == mBasketballConfig.Name)
                                    Global.BasketballCount.Value += (int)powerData.Value;
                                break;

                            case AbilityPower.PowerType.AttackCount:
                                if (abilityConfig.Name == mSimpleKnifeConfig.Name)
                                    Global.SimpleKnifeAttackCount.Value += (int)powerData.Value;
                                break;

                            default:
                                break;
                        }

                        Debug.Log($"升级{powerData.Type} " + powerData.Value);
                    }
                }
            }
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

            foreach (ExpUpgradeItem item in Items.Where(item => !item.UpgradeFinish).Take(4))
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
