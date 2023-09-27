using System;

namespace ProjectSurvivor
{
    public class CoinUpgradeItem
    {
        public string Key { get; private set; }
        public string Desctiption { get; private set; }
        public int Price { get; private set; }

        private Action<CoinUpgradeItem> mOnUpgrade;

        public void Upgrade()
        {
            mOnUpgrade?.Invoke(this);
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
    }
}
