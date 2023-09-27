/****************************************************************************
 * 2023.9 MSI
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
    public partial class CoinUpgradePanel : UIElement, IController
    {
        private void Awake()
        {
            this.GetSystem<CoinUpgradeSystem>().Items.ForEach(coinUpgradeItem =>
            {
                CoinUpgradeItemTemplete.InstantiateWithParent(CoinUpgradeItemRoot)
                    .Self(self =>
                    {
                        CoinUpgradeItem itemCache = coinUpgradeItem;
                        self.GetComponentInChildren<Text>().text = coinUpgradeItem.Desctiption + " " + coinUpgradeItem.Price + " ���";
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
            });



            // �����رհ�ť
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