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

            // �����رհ�ť
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
                        self.GetComponentInChildren<Text>().text = coinUpgradeItem.Desctiption + $" {coinUpgradeItem.Price} ���";
                        self.onClick.AddListener(() =>
                        {
                            itemCache.Upgrade();
                            AudioKit.PlaySound("AbilityLevelUp");
                        });

                        Button selfCache = self;
                        // ע�������ұ��
                        Global.Coin.RegisterWithInitValue(coin =>
                        {
                            CoinText.text = "��ң�" + coin;

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