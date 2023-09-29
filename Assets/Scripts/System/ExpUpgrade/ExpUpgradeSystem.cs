using UnityEngine;
using QFramework;
using System.Collections.Generic;

namespace ProjectSurvivor
{
    public class ExpUpgradeSystem : AbstractSystem
    {
        public static EasyEvent OnCoinUpgradeSystemChanged = new EasyEvent();

        public List<ExpUpgradeItem> Items { get; } = new List<ExpUpgradeItem>();

        protected override void OnInit()
        {
            ExpUpgradeItem simpleDamageLv1 = Add(new ExpUpgradeItem()
                .WithKey("simple_damage_lv1")
                .WithDescription("简单能力攻击力LV1")
                .OnUpgrade(_ =>
                {
                    Global.SimpleAbilityDamage.Value *= 1.5f;
                }));

            ExpUpgradeItem simpleDamageLv2 = Add(new ExpUpgradeItem()
                .WithKey("simple_damage_lv2")
                .WithDescription("简单能力攻击力LV2")
                .Condition(item => simpleDamageLv1.UpgradeFinish)
                .OnUpgrade(_ =>
                {
                    Global.SimpleAbilityDamage.Value *= 1.5f;
                }));

            ExpUpgradeItem simpleDamageLv3 = Add(new ExpUpgradeItem()
                .WithKey("simple_damage_lv3")
                .WithDescription("简单能力攻击力LV3")
                .Condition(item => simpleDamageLv2.UpgradeFinish)
                .OnUpgrade(_ =>
                {
                    Global.SimpleAbilityDamage.Value *= 1.5f;
                }));

            ExpUpgradeItem simpleDurationLv1 = Add(new ExpUpgradeItem()
                .WithKey("simple_duration_lv1")
                .WithDescription("简单能力攻击速度LV1")
                .OnUpgrade(_ =>
                {
                    Global.SimpleAbilityDuration.Value *= 0.8f;
                }));

            ExpUpgradeItem simpleDurationLv2 = Add(new ExpUpgradeItem()
                .WithKey("simple_duration_lv2")
                .WithDescription("简单能力攻击速度LV2")
                .Condition(item => simpleDurationLv1.UpgradeFinish)
                .OnUpgrade(_ =>
                {
                    Global.SimpleAbilityDuration.Value *= 0.8f;
                }));

            ExpUpgradeItem simpleDurationLv3 = Add(new ExpUpgradeItem()
                .WithKey("simple_duration_lv3")
                .WithDescription("简单能力攻击速度LV3")
                .Condition(item => simpleDurationLv2.UpgradeFinish)
                .OnUpgrade(_ =>
                {
                    Global.SimpleAbilityDuration.Value *= 0.8f;
                }));

            simpleDamageLv1.OnChanged.Register(() =>
            {
                simpleDamageLv2.OnChanged.Trigger();
            });

            simpleDamageLv2.OnChanged.Register(() =>
            {
                simpleDamageLv3.OnChanged.Trigger();
            });

            simpleDurationLv1.OnChanged.Register(() =>
            {
                simpleDurationLv2.OnChanged.Trigger();
            });

            simpleDurationLv2.OnChanged.Register(() =>
            {
                simpleDurationLv3.OnChanged.Trigger();
            });
            // 上面的代码有些冗长，后续需要改进
        }

        public ExpUpgradeItem Add(ExpUpgradeItem item)
        {
            Items.Add(item);
            return item;
        }
    }
}
