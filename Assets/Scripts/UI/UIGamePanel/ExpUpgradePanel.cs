/****************************************************************************
 * 2023.9 MSI
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.U2D;

namespace ProjectSurvivor
{
    public partial class ExpUpgradePanel : UIElement, IController
    {
        private ResLoader mResLoader;

        private void Awake()
        {
            // 获取所有资源
            mResLoader = ResLoader.Allocate();
            // 加载 Icon 的 SpriteAtlas 资源
            SpriteAtlas iconAtlas = mResLoader.LoadSync<SpriteAtlas>("icon");
            // 可以根据 Sprite 的名称获取 Sprite
            //Sprite simpleKnifeIcon = iconAtlas.GetSprite("simple_knife_icon");

            ExpUpgradeItemTempleteBtn.Hide();

            ExpUpgradeSystem expUpgradeSystem = this.GetSystem<ExpUpgradeSystem>();

            foreach (ExpUpgradeItem expUpgradeItem in expUpgradeSystem.Items)
            {
                ExpUpgradeItemTempleteBtn.InstantiateWithParent(UpgradeRoot)
                    .Self(self =>
                    {
                        ExpUpgradeItem itemCache = expUpgradeItem;

                        self.transform.Find("Icon").GetComponent<Image>().sprite = iconAtlas.GetSprite(itemCache.IconName);

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
                                self.GetComponentInChildren<Text>().text = expUpgradeItem.Description;
                                selfCache.Show();

                                Transform pairedUpgradeName = selfCache.transform.Find("PairedUpgradeName");
                                if (expUpgradeSystem.Pairs.TryGetValue(itemCache.Key, out string pairedName))
                                {
                                    ExpUpgradeItem pairedItem = expUpgradeSystem.ExpUpgradeDict[pairedName];

                                    if (pairedItem.CurrentLevel.Value > 1 && itemCache.CurrentLevel.Value == 1)
                                    {
                                        pairedUpgradeName.transform.Find("Icon").GetComponent<Image>().sprite = iconAtlas.GetSprite(pairedItem.IconName);
                                        pairedUpgradeName.Show();
                                    }
                                    else
                                    {
                                        pairedUpgradeName.Hide();
                                    }
                                }
                                else
                                {
                                    pairedUpgradeName.Hide();
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
            mResLoader.Recycle2Cache();
            mResLoader = null;
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}