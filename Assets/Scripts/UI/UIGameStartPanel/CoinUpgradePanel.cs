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

                        self.GetComponentInChildren<Text>().text = coinUpgradeItem.Desctiption;
                        self.onClick.AddListener(() =>
                        {
                            itemCache.Upgrade();
                            AudioKit.PlaySound("AbilityLevelUp");
                        });
                    })
                    .Show();
            });

            CoinPercentUpgradeBtn.Hide();
            ExpPercentUpgradeBtn.Hide();
            MaxHPUpgradeBtn.Hide();


            // 监听金币变更
            Global.Coin.RegisterWithInitValue(coin =>
            {
                CoinText.text = "金币：" + coin;

                //if (coin >= 5)
                //{
                //    CoinPercentUpgradeBtn.Show();
                //    ExpPercentUpgradeBtn.Show();
                //}
                //else
                //{
                //    CoinPercentUpgradeBtn.Hide();
                //    ExpPercentUpgradeBtn.Hide();
                //}

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 监听金币掉率升级按钮
            CoinPercentUpgradeBtn.onClick.AddListener(() =>
            {

                AudioKit.PlaySound("AbilityLevelUp");
            });

            // 监听经验掉率升级按钮
            ExpPercentUpgradeBtn.onClick.AddListener(() =>
            {
                Global.ExpPercent.Value += 0.1f;
                Global.Coin.Value -= 5;

                AudioKit.PlaySound("AbilityLevelUp");
            });

            // 监听生命值升级按钮
            MaxHPUpgradeBtn.onClick.AddListener(() =>
            {
                Global.MaxHP.Value++;
                Global.Coin.Value -= 30;

                AudioKit.PlaySound("AbilityLevelUp");
            });

            // 监听关闭按钮
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