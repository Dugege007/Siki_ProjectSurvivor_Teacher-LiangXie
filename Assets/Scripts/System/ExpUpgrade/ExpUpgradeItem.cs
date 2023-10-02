using QFramework;
using System;

namespace ProjectSurvivor
{
    public class ExpUpgradeItem
    {
        public bool IsWeapon = false;
        public bool UpgradeFinish { get; set; } = false;
        public string Key { get; private set; }
        public string Description => mDescriptionFactory(CurrentLevel.Value);

        public int MaxLevel { get; private set; }
        public BindableProperty<int> CurrentLevel = new(1);

        public BindableProperty<bool> Visible = new(false);

        private Action<ExpUpgradeItem, int> mOnUpgrade;
        private Func<int, string> mDescriptionFactory;

        public ExpUpgradeItem(bool isWeapon = false)
        {
            IsWeapon = isWeapon;
        }

        public void Upgrade()
        {
            mOnUpgrade?.Invoke(this, CurrentLevel.Value);
            CurrentLevel.Value++;

            if (CurrentLevel.Value > MaxLevel)
                UpgradeFinish = true;
        }

        public ExpUpgradeItem WithKey(string key)
        {
            Key = key;
            return this;
        }

        public ExpUpgradeItem WithDescription(Func<int, string> descriptionFactory)
        {
            mDescriptionFactory = descriptionFactory;
            return this;
        }

        public ExpUpgradeItem OnUpgrade(Action<ExpUpgradeItem, int> onUpgrade)
        {
            mOnUpgrade = onUpgrade;
            return this;
        }

        public ExpUpgradeItem WithMaxLevel(int maxLevel)
        {
            MaxLevel = maxLevel;
            return this;
        }
    }
}
