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
            ExpUpgradeItem simpleDamage = Add(new ExpUpgradeItem()
                .WithKey("simple_damage")
                .WithDescription("简单能力攻击力")
                .WithMaxLevel(10)
                .OnUpgrade((_, level) =>
                {
                    if (level == 1)
                    {
                    }
                    Global.SimpleAbilityDamage.Value *= 1.5f;
                }));

            ExpUpgradeItem simpleDuration = Add(new ExpUpgradeItem()
                .WithKey("simple_duration")
                .WithDescription("简单能力攻击速度")
                .OnUpgrade((_, level) =>
                {
                    if (level == 1)
                    {
                    }
                    Global.SimpleAbilityDuration.Value *= 0.8f;
                }));

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

        public void Roll()
        {
            foreach (ExpUpgradeItem expUpgradeItem in Items)
            {
                expUpgradeItem.Visible.Value = false;
            }

            ExpUpgradeItem item = Items.Where(item => item.UpgradeFinish == false)
                .ToList()
                .GetRandomItem();   // 获取随机的单位

            if (item == null)
            {
                Debug.LogError("没有可用的升级项");
                return;
            }

            item.Visible.Value = true;
        }
    }
}
