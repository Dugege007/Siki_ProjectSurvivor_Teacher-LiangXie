using UnityEngine.UI;
using QFramework;
using System.Linq;

namespace ProjectSurvivor
{
    public partial class CoinUpgradePanel : UIElement, IController
    {
        private void Awake()
        {
            CoinUpgradeItemTemplete.Hide();

            CoinUpgradeSystem.OnCoinUpgradeSystemChanged.Register(() =>
            {
                Refresh();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Refresh();

            // ¼àÌý¹Ø±Õ°´Å¥
            CloseBtn.onClick.AddListener(() =>
            {
                this.Hide();
            });
        }

        private void Refresh()
        {
            CoinUpgradeItemRoot.DestroyChildren();

            foreach (CoinUpgradeItem coinUpgradeItem in this.GetSystem<CoinUpgradeSystem>().Items.Where(item => item.ConditionCheck()))
            {
                CoinUpgradeItemTemplete.InstantiateWithParent(CoinUpgradeItemRoot)
                    .Self(self =>
                    {
                        CoinUpgradeItem itemCache = coinUpgradeItem;
                        self.GetComponentInChildren<Text>().text = coinUpgradeItem.Desctiption + $" {coinUpgradeItem.Price} ½ð±Ò";
                        self.onClick.AddListener(() =>
                        {
                            itemCache.Upgrade();
                            AudioKit.PlaySound("AbilityLevelUp");
                        });

                        Button selfCache = self;
                        // ×¢²á¼àÌý½ð±Ò±ä¸ü
                        Global.Coin.RegisterWithInitValue(coin =>
                        {
                            CoinText.text = "½ð±Ò£º" + coin;

                            if (coin >= itemCache.Price)
                            {
                                selfCache.interactable = true;
                            }
                            else
                            {
                                selfCache.interactable = false;
                            }

                        }).UnRegisterWhenGameObjectDestroyed(self);

                    }).Show();
            }
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