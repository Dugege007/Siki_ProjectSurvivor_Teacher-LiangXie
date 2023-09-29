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
    public partial class ExpUpgradePanel : UIElement,IController
    {
        private void Awake()
        {
            ExpUpgradeSystem expUpgradeSystem = this.GetSystem<ExpUpgradeSystem>();

            foreach (ExpUpgradeItem expUpgradeItem in expUpgradeSystem.Items)
            {
                ExpUpgradeItemTempleteBtn.InstantiateWithParent(UpgradeRoot)
                    .Self(self =>
                    {
                        ExpUpgradeItem itemCache = expUpgradeItem;
                        self.GetComponentInChildren<Text>().text = expUpgradeItem.Description + $" {expUpgradeItem.Price} 经验";
                        self.onClick.AddListener(() =>
                        {
                            Time.timeScale = 1.0f;
                            itemCache.Upgrade();
                            this.Hide();
                            AudioKit.PlaySound("AbilityLevelUp");
                        });

                        Button selfCache = self;
                        expUpgradeItem.OnChanged.Register(() =>
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
                    });
            }

            //// 注册按钮事件
            //UpgradeBtn.onClick.AddListener(() =>
            //{
            //    Time.timeScale = 1;
            //    Global.SimpleAbilityDamage.Value *= 1.5f;
            //    UpgradeRoot.Hide();

            //    AudioKit.PlaySound("AbilityLevelUp");
            //});

            //SimpleDurationUpgradeBtn.onClick.AddListener(() =>
            //{
            //    Time.timeScale = 1;
            //    Global.SimpleAbilityDuration.Value *= 0.8f;
            //    UpgradeRoot.Hide();

            //    AudioKit.PlaySound("AbilityLevelUp");
            //});
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