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
    public partial class ExpUpgradePanel : UIElement, IController
    {
        private void Awake()
        {
            ExpUpgradeItemTempleteBtn.Hide();

            ExpUpgradeSystem expUpgradeSystem = this.GetSystem<ExpUpgradeSystem>();

            foreach (ExpUpgradeItem expUpgradeItem in expUpgradeSystem.Items)
            {
                ExpUpgradeItemTempleteBtn.InstantiateWithParent(UpgradeRoot)
                    .Self(self =>
                    {
                        ExpUpgradeItem itemCache = expUpgradeItem;
                        self.onClick.AddListener(() =>
                        {
                            Time.timeScale = 1.0f;
                            itemCache.Upgrade();
                            this.Hide();
                            AudioKit.PlaySound("AbilityLevelUp");
                        });

                        Button selfCache = self;
                        selfCache.Hide();

                        itemCache.Visible.RegisterWithInitValue(visible =>
                        {
                            if (visible)
                            {
                                selfCache.Show();

                                Text pairInfoText = selfCache.transform.Find("PairedName").GetComponent<Text>();

                                if (expUpgradeSystem.Pairs.TryGetValue(itemCache.Key, out string pairedName))
                                {
                                    ExpUpgradeItem pairedItem = expUpgradeSystem.ExpUpgradeDict[pairedName];

                                    if (pairedItem.CurrentLevel.Value > 1 && itemCache.CurrentLevel.Value == 1)
                                    {
                                        pairInfoText.text = "Åä¶Ô¼¼ÄÜ\n" + pairedItem.Key;
                                        pairInfoText.Show();
                                    }
                                    else
                                    {
                                        pairInfoText.Hide();
                                    }
                                }
                                else
                                {
                                    pairInfoText.Hide();
                                }

                            }
                            else
                            {
                                selfCache.Hide();
                            }

                        }).UnRegisterWhenGameObjectDestroyed(selfCache);

                        itemCache.CurrentLevel.RegisterWithInitValue(lv =>
                        {
                            selfCache.GetComponentInChildren<Text>().text = expUpgradeItem.Description;

                        }).UnRegisterWhenGameObjectDestroyed(selfCache);
                    });
            }
        }

        private void Start()
        {
            SkipBtn.onClick.AddListener(() =>
            {
                Time.timeScale = 1.0f;
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