using QFramework;
using System;

namespace ProjectSurvivor
{
    public class ExpUpgradeItem
    {
        public EasyEvent OnChanged = new EasyEvent();

        public bool UpgradeFinish { get; set; } = false;
        public string Key { get; private set; }
        public string Description { get; private set; }

        public int MaxLevel { get; private set; }
        public int CurrentLevel { get; private set; } = 0;

        public BindableProperty<bool> Visible = new(false);

        private Action<ExpUpgradeItem, int> mOnUpgrade;

        public void Upgrade()
        {
            CurrentLevel++;
            mOnUpgrade?.Invoke(this, CurrentLevel);

            if (CurrentLevel >= MaxLevel)
                UpgradeFinish = true;

            OnChanged.Trigger();
        }

        public ExpUpgradeItem WithKey(string key)
        {
            Key = key;
            return this;
        }

        public ExpUpgradeItem WithDescription(string description)
        {
            Description = description;
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
