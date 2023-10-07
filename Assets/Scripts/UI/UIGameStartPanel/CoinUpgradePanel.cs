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

            // ע�������� UI ���
            Global.Coin.RegisterWithInitValue(coin =>
            {
                CoinText.text = "��ң�" + coin;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            foreach (CoinUpgradeItem coinUpgradeItem in this.GetSystem<CoinUpgradeSystem>().Items.Where(item => item.UpgradeFinish == false))
            {
                CoinUpgradeItemTempleteBtn.InstantiateWithParent(CoinUpgradeItemRoot)
                    .Self(self =>
                    {
                        CoinUpgradeItem itemCache = coinUpgradeItem;
                        self.GetComponentInChildren<Text>().text = coinUpgradeItem.Description + $"\n{coinUpgradeItem.Price} ���";
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

                        // ע�������ұ���������Ƿ��������
                        Global.Coin.RegisterWithInitValue(coin =>
                        {
                            if (coin >= itemCache.Price)
                                selfCache.interactable = true;
                            else
                                selfCache.interactable = false;

                        }).UnRegisterWhenGameObjectDestroyed(self);
                    });
            }

            // �����رհ�ť
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