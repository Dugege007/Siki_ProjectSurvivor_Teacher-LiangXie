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
        public int Price { get; private set; }

        private Action<ExpUpgradeItem> mOnUpgrade;
        private Func<ExpUpgradeItem, bool> mCondition;

        public void Upgrade()
        {
            mOnUpgrade?.Invoke(this);
            UpgradeFinish = true;
            OnChanged.Trigger();
            //ExpUpgradeSystem.OnCoinUpgradeSystemChanged.Trigger(); // .Trigger() 触发一下这个事件
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

        public ExpUpgradeItem WithPrice(int price)
        {
            Price = price;
            return this;
        }

        public ExpUpgradeItem OnUpgrade(Action<ExpUpgradeItem> onUpgrade)
        {
            mOnUpgrade = onUpgrade;
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ExpUpgradeItem Condition(Func<ExpUpgradeItem, bool> condition)
        {
            mCondition = condition;
            return this;
        }
    }
}
