using UnityEngine;
using QFramework;
using System.Collections.Generic;
using System.Linq;

namespace ProjectSurvivor
{
    public class ExpUpgradeSystem : AbstractSystem
    {
        public static EasyEvent OnCoinUpgradeSystemChanged = new EasyEvent();

        public List<ExpUpgradeItem> Items { get; } = new();

        public Dictionary<string, ExpUpgradeItem> ExpUpgradeDict = new();

        public Dictionary<string, string> Pairs = new()
        {
            { "simple_sword", "critical_chance" },
            { "simple_bomb", "additional_damage" },
            { "simple_knife", "additional_fly_thing" },
            { "basketball", "additional_movement_speed" },
            { "rotate_sword", "additional_exp_rate" },
            // 反向
            { "critical_chance", "simple_sword" },
            { "additional_damage", "simple_bomb" },
            { "additional_fly_thing", "simple_knife" },
            { "additional_movement_speed", "basketball" },
            { "additional_exp_rate", "rotate_sword" },
        };

        public Dictionary<string, BindableProperty<bool>> PairedProperties = new()
        {
            { "simple_sword", Global.SuperSimpleSword },
            { "simple_bomb", Global.SuperBomb },
            { "simple_knife", Global.SuperKnife },
            { "basketball", Global.SuperBasketball },
            { "rotate_sword", Global.SuperRotateSword },
        };

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
            AddNewExpUpgradeItem("simple_sword", ConfigManager.Instance.SimpleSwordConfig);
            // 飞刀
            AddNewExpUpgradeItem("simple_knife", ConfigManager.Instance.SimpleKnifeConfig);
            // 守卫剑
            AddNewExpUpgradeItem("rotate_sword", ConfigManager.Instance.RotateSwordConfig);
            // 篮球
            AddNewExpUpgradeItem("basketball", ConfigManager.Instance.BasketballConfig);

            // 炸弹
            AddNewExpUpgradeItem("simple_bomb", ConfigManager.Instance.SimpleBombConfig);

            // 暴击率
            AddNewExpUpgradeItem("critical_chance", ConfigManager.Instance.CriticalChanceConfig);
            // 附加伤害值
            AddNewExpUpgradeItem("additional_damage", ConfigManager.Instance.AdditionalDamageConfig);
            // 附加移动速度
            AddNewExpUpgradeItem("additional_movement_speed", ConfigManager.Instance.AdditionalMovementSpeedConfig);
            // 附加经验值
            AddNewExpUpgradeItem("additional_exp_rate", ConfigManager.Instance.AdditionalExpRateConfig);
            // 附加飞射物
            AddNewExpUpgradeItem("additional_fly_thing", ConfigManager.Instance.AdditionalFlyThingCountConfig);
            // 拾取范围
            AddNewExpUpgradeItem("collectable_area_range", ConfigManager.Instance.CollectableAreaRangeConfig);

            ExpUpgradeDict = Items.ToDictionary(i => i.Key);
        }

        private void AddNewExpUpgradeItem(string key, AbilityConfig abilityConfig)
        {
            Add(new ExpUpgradeItem(abilityConfig.IsWeapon)
                .WithKey(key)
                .WithName(abilityConfig.Name)
                .WithIconName(key + "_icon")
                .WithPairedName("合成的" + abilityConfig.Name)
                .WithPairedIconName("paired_" + key + "_icon")
                .WithPairedDescription(abilityConfig.PairedDescription)
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
            if (lv == 1 && abilityConfig.IsWeapon)
            {
                return $"{abilityConfig.Name} Lv1" + "\n" + abilityConfig.Description;
            }

            for (int i = 1; i < abilityConfig.Powers.Count + 1; i++)
            {
                if (lv == i)
                    return abilityConfig.Powers[lv - 1].GetPowerUpInfo(abilityConfig.Name);
            }

            // 不加这行会报错
            return "未知等级";
        }

        private void UpgradePowerValue(int lv, AbilityConfig abilityConfig)
        {
            // 解锁武器
            if (lv == 1)
            {
                if (abilityConfig.Name == ConfigManager.Instance.SimpleSwordConfig.Name)
                    Global.SimpleSwordUnlocked.Value = true;
                else if (abilityConfig.Name == ConfigManager.Instance.SimpleBombConfig.Name)
                    Global.SimpleBombUnlocked.Value = true;
                else if (abilityConfig.Name == ConfigManager.Instance.SimpleKnifeConfig.Name)
                    Global.SimpleKnifeUnlocked.Value = true;
                else if (abilityConfig.Name == ConfigManager.Instance.RotateSwordConfig.Name)
                    Global.RotateSwordUnlocked.Value = true;
                else if (abilityConfig.Name == ConfigManager.Instance.BasketballConfig.Name)
                    Global.BasketballUnlocked.Value = true;
            }

            // 升级能力
            for (int i = 1; i < abilityConfig.Powers.Count + 1; i++)
            {
                if (lv == i)
                {
                    Debug.Log("当前升级：" + abilityConfig.Name + abilityConfig.Powers[lv - 1].Lv);

                    foreach (PowerData powerData in abilityConfig.Powers[lv - 1].PowerDatas)
                    {
                        // 剑
                        if (abilityConfig.Name == ConfigManager.Instance.SimpleSwordConfig.Name)
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

                        // 飞刀
                        if (abilityConfig.Name == ConfigManager.Instance.SimpleKnifeConfig.Name)
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
                        if (abilityConfig.Name == ConfigManager.Instance.RotateSwordConfig.Name)
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
                        if (abilityConfig.Name == ConfigManager.Instance.BasketballConfig.Name)
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

                        // 炸弹
                        if (abilityConfig.Name == ConfigManager.Instance.SimpleBombConfig.Name)
                        {
                            switch (powerData.Type)
                            {
                                case AbilityPower.PowerType.Damage:
                                    Global.SimpleBombDamage.Value += powerData.Value;
                                    break;
                                case AbilityPower.PowerType.Chance:
                                    Global.SimpleBombChance.Value += powerData.Value;
                                    break;
                                default:
                                    break;
                            }
                        }

                        // 暴击率
                        if (abilityConfig.Name == ConfigManager.Instance.CriticalChanceConfig.Name)
                        {
                            switch (powerData.Type)
                            {
                                case AbilityPower.PowerType.Chance:
                                    Global.CriticalChance.Value += powerData.Value;
                                    break;
                                default:
                                    break;
                            }
                        }

                        // 附加伤害值
                        if (abilityConfig.Name == ConfigManager.Instance.AdditionalDamageConfig.Name)
                        {
                            switch (powerData.Type)
                            {
                                case AbilityPower.PowerType.Rate:
                                    Global.AdditionalDamage.Value += powerData.Value;
                                    break;
                                default:
                                    break;
                            }
                        }

                        // 附加移动速度
                        if (abilityConfig.Name == ConfigManager.Instance.AdditionalMovementSpeedConfig.Name)
                        {
                            switch (powerData.Type)
                            {
                                case AbilityPower.PowerType.Rate:
                                    Global.AdditionalMovementSpeed.Value += powerData.Value;
                                    break;
                                default:
                                    break;
                            }
                        }

                        // 附加经验值
                        if (abilityConfig.Name == ConfigManager.Instance.AdditionalExpRateConfig.Name)
                        {
                            switch (powerData.Type)
                            {
                                case AbilityPower.PowerType.Rate:
                                    Global.AdditionalExpRate.Value += powerData.Value;
                                    break;
                                default:
                                    break;
                            }
                        }

                        // 附加飞射物
                        if (abilityConfig.Name == ConfigManager.Instance.AdditionalFlyThingCountConfig.Name)
                        {
                            switch (powerData.Type)
                            {
                                case AbilityPower.PowerType.Count:
                                    Global.AdditionalFlyThingCount.Value += (int)powerData.Value;
                                    break;
                                default:
                                    break;
                            }
                        }

                        // 拾取范围
                        if (abilityConfig.Name == ConfigManager.Instance.CollectableAreaRangeConfig.Name)
                        {
                            switch (powerData.Type)
                            {
                                case AbilityPower.PowerType.Range:
                                    Global.CollectableAreaRange.Value += powerData.Value;
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
                Debug.Log("没有可用的升级项");
                return;
            }
        }
    }
}
