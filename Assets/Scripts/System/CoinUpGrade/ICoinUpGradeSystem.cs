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
            Items.Add(new CoinUpgradeItem()
                .WithKey("AAA")
                .WithDescription("BBB")
                .WithPrice(333)
                .OnUpgrade(item =>
                {
                    Debug.Log("CCC");
                }));

            Items.Add(new CoinUpgradeItem()
                .WithKey("coin_percent")
                .WithDescription("½ð±ÒµôÂä¸ÅÂÊÌáÉý")
                .WithPrice(5)
                .OnUpgrade(item =>
                {
                    Global.CoinPercent.Value += 0.1f;
                    Global.Coin.Value -= item.Price;
                }));
        }

        public void Say()
        {
            Debug.Log("Hello CoinUpGradeSystem!");
        }
    }
}
