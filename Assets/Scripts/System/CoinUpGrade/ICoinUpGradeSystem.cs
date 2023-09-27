using QFramework;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectSurvivor
{
    public class CoinUpgradeSystem : AbstractSystem
    {
        public static EasyEvent OnCoinUpgradeSystemChanged = new EasyEvent();

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

            CoinUpgradeItem coinPercentLv1 = Add(new CoinUpgradeItem()
                .WithKey("coin_percent_lv1")
                .WithDescription("金币掉落概率提升 LV1")
                .WithPrice(5)
                .OnUpgrade(item =>
                {
                    Global.CoinPercent.Value += 0.1f;
                    Global.Coin.Value -= item.Price;
                }));

            CoinUpgradeItem coinPercentLv2 = Add(new CoinUpgradeItem()
                .WithKey("coin_percent_lv2")
                .WithDescription("金币掉落概率提升 LV2")
                .WithPrice(7)
                .Condition((_) => coinPercentLv1.UpgradeFinish)
                .OnUpgrade(item =>
                {
                    Global.CoinPercent.Value += 0.1f;
                    Global.Coin.Value -= item.Price;
                }));

            CoinUpgradeItem coinPercentLv3 = Add(new CoinUpgradeItem()
                .WithKey("coin_percent_lv3")
                .WithDescription("金币掉落概率提升 LV3")
                .WithPrice(10)
                .Condition((_) => coinPercentLv2.UpgradeFinish)
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

        /// <summary>
        /// 将当前升级项添加到列表中
        /// </summary>
        /// <param name="item">当前升级项</param>
        /// <returns>返回当前升级项</returns>
        /// <remark>可用于链式调用</remark>
        public CoinUpgradeItem Add(CoinUpgradeItem item)
        {
            Items.Add(item);
            return item;
        }

        //public void Say()
        //{
        //    Debug.Log("Hello CoinUpGradeSystem!");
        //}
    }
}
