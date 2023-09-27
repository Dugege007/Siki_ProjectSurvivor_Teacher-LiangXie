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

            foreach (CoinUpgradeItem coinUpgradeItem in this.GetSystem<CoinUpgradeSystem>().Items.Where(item => item.UpgradeFinish == false))
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
                    });
            }

            // ¼àÌý¹Ø±Õ°´Å¥
            CloseBtn.onClick.AddListener(() =>
            {
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