using System;

namespace ProjectSurvivor
{
    public class CoinUpgradeItem
    {
        public bool UpgradeFinish { get; set; } = false;
        public string Key { get; private set; }
        public string Desctiption { get; private set; }
        public int Price { get; private set; }

        private Action<CoinUpgradeItem> mOnUpgrade;
        private Func<CoinUpgradeItem, bool> mCondition;

        public void Upgrade()
        {
            mOnUpgrade?.Invoke(this);
            UpgradeFinish = true;
            CoinUpgradeSystem.OnCoinUpgradeSystemChanged.Trigger(); // .Trigger() 触发一下这个事件
        }

        /// <summary>
        /// 条件检测
        /// </summary>
        /// <returns></returns>
        public bool ConditionCheck()
        {
            if (mCondition != null)
                return !UpgradeFinish && mCondition.Invoke(this);

            return !UpgradeFinish;
        }

        public CoinUpgradeItem WithKey(string key)
        {
            Key = key;
            return this;
        }

        public CoinUpgradeItem WithDescription(string description)
        {
            Desctiption = description;
            return this;
        }

        public CoinUpgradeItem WithPrice(int price)
        {
            Price = price;
            return this;
        }

        public CoinUpgradeItem OnUpgrade(Action<CoinUpgradeItem> onUpgrade)
        {
            mOnUpgrade = onUpgrade;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public CoinUpgradeItem Condition(Func<CoinUpgradeItem, bool> condition)
        {
            mCondition = condition;
            return this;
        }
    }
}
