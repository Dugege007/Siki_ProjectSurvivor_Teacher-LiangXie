using UnityEngine.UI;
using QFramework;
using System.Linq;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class CoinUpgradePanel : UIElement, IController
    {
        private void Awake()
        {
            CoinUpgradeItemTempleteBtn.Hide();

            // 注册监听金币 UI 变更
            Global.Coin.RegisterWithInitValue(coin =>
            {
                CoinText.text = "金币：" + coin;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            foreach (CoinUpgradeItem coinUpgradeItem in this.GetSystem<CoinUpgradeSystem>().Items.Where(item => item.UpgradeFinish == false))
            {
                CoinUpgradeItemTempleteBtn.InstantiateWithParent(CoinUpgradeItemRoot)
                    .Self(self =>
                    {
                        CoinUpgradeItem itemCache = coinUpgradeItem;
                        self.GetComponentInChildren<Text>().text = coinUpgradeItem.Description + $"\n{coinUpgradeItem.Price} 金币";
                        self.onClick.AddListener(() =>
                        {
                            itemCache.Upgrade();
                            AudioKit.PlaySound("AbilityLevelUp");
                        });

                        Button selfCache = self;
                        coinUpgradeItem.OnChanged.Register(() =>
                        {
                            if (itemCache.ConditionCheck())
                                selfCache.Show();
                            else
                                selfCache.Hide();

                        }).UnRegisterWhenGameObjectDestroyed(selfCache);

                        if (itemCache.ConditionCheck())
                            selfCache.Show();
                        else
                            selfCache.Hide();

                        // 注册监听金币变更，决定是否解锁按键
                        Global.Coin.RegisterWithInitValue(coin =>
                        {
                            if (coin >= itemCache.Price)
                                selfCache.interactable = true;
                            else
                                selfCache.interactable = false;

                        }).UnRegisterWhenGameObjectDestroyed(self);
                    });
            }

            // 监听关闭按钮
            CloseBtn.onClick.AddListener(() =>
            {
                AudioKit.PlaySound(Sfx.BUTTONCLICK);
                this.Hide();
            });
        }

        protected override void OnBeforeDestroy()
        {
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}