using QFramework;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSurvivor
{
    public class CoinUpgradeSystem : AbstractSystem
    {
        public List<CoinUpgradeItem> Items { get; } = new List<CoinUpgradeItem>();

        protected override void OnInit()
        {
            //Items.Add(new CoinUpgradeItem()
            //    .WithKey("AAA")
            //    .WithDescription("BBB")
            //    .WithPrice(333)
            //    .OnUpgrade(item =>
            //    {
            //        Debug.Log("CCC");
            //    }));

            Items.Add(new CoinUpgradeItem()
                .WithKey("coin_percent")
                .WithDescription("金币掉落概率提升")
                .WithPrice(5)
                .OnUpgrade(item =>
                {
                    Global.CoinPercent.Value += 0.1f;
                    Global.Coin.Value -= item.Price;
                }));

            Items.Add(new CoinUpgradeItem()
                .WithKey("exp_percent")
                .WithDescription("经验值掉落概率提升")
                .WithPrice(5)
                .OnUpgrade(item =>
                {
                    Global.ExpPercent.Value += 0.1f;
                    Global.Coin.Value -= item.Price;
                }));

            Items.Add(new CoinUpgradeItem()
                .WithKey("max_hp")
                .WithDescription("主角的最大血量+1")
                .WithPrice(30)
                .OnUpgrade(item =>
                {
                    Global.MaxHP.Value++;
                    Global.Coin.Value -= item.Price;
                }));
        }

        public void Say()
        {
            Debug.Log("Hello CoinUpGradeSystem!");
        }
    }
}
