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

            // 剑
            AddNewExpUpgradeItem("simple_sword", Player.Default.SimpleSwordConfig);
            // 炸弹
            AddNewExpUpgradeItem("simple_bomb", Player.Default.SimpleBombConfig);
            // 飞刀
            AddNewExpUpgradeItem("simple_knife", Player.Default.SimpleKnifeConfig);
            // 守卫剑
            AddNewExpUpgradeItem("rotate_sword", Player.Default.RotateSwordConfig);
            // 篮球
            AddNewExpUpgradeItem("basketball", Player.Default.BasketballConfig);
            // 暴击率
            AddNewExpUpgradeItem("critical_rate", Player.Default.CriticalRateConfig);
            // 伤害附加值
            AddNewExpUpgradeItem("damage_rate", Player.Default.DamageRateConfig);
        }

        private void AddNewExpUpgradeItem(string key, AbilityConfig abilityConfig)
        {
            Add(new ExpUpgradeItem(true)
                .WithKey(key)
                .WithDescription(lv =>
                {
                    return UpgradeDiscription(lv, abilityConfig);
                })
                .WithMaxLevel(abilityConfig.Powers.Count)
                .OnUpgrade((_, lv) =>
                {
                    UpgradePowerValue(lv, abilityConfig);
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
                if (abilityConfig.Name == Player.Default.SimpleSwordConfig.Name)
                    Global.SimpleSwordUnlocked.Value = true;
                else if (abilityConfig.Name == Player.Default.SimpleBombConfig.Name)
                    Global.SimpleBombUnlocked.Value = true;
                else if (abilityConfig.Name == Player.Default.SimpleKnifeConfig.Name)
                    Global.SimpleKnifeUnlocked.Value = true;
                else if (abilityConfig.Name == Player.Default.RotateSwordConfig.Name)
                    Global.RotateSwordUnlocked.Value = true;
                else if (abilityConfig.Name == Player.Default.BasketballConfig.Name)
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
                        if (abilityConfig.Name == Player.Default.SimpleSwordConfig.Name)
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
                        if (abilityConfig.Name == Player.Default.SimpleBombConfig.Name)
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
                        if (abilityConfig.Name == Player.Default.SimpleKnifeConfig.Name)
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
                        if (abilityConfig.Name == Player.Default.RotateSwordConfig.Name)
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
                        if (abilityConfig.Name == Player.Default.BasketballConfig.Name)
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
                        if (abilityConfig.Name == Player.Default.CriticalRateConfig.Name)
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

                        // 伤害附加值
                        if (abilityConfig.Name == Player.Default.DamageRateConfig.Name)
                        {
                            switch (powerData.Type)
                            {
                                case AbilityPower.PowerType.Percent:
                                    Global.DamageRate.Value += powerData.Value;
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
