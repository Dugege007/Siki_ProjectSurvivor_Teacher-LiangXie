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
            Add(new ExpUpgradeItem()
                .WithKey("simple_damage_lv1")
                .WithDescription("简单能力攻击力LV1")
                .OnUpgrade(_ =>
                {
                    Global.SimpleAbilityDamage.Value *= 1.5f;
                }));
        }

        public ExpUpgradeItem Add(ExpUpgradeItem item)
        {
            Items.Add(item);
            return item;
        }
    }
}
