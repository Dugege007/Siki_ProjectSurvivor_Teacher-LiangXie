/****************************************************************************
 * 2023.10 MSI
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.U2D;

namespace ProjectSurvivor
{
    public partial class UnlockedIconPanel : UIElement, IController
    {
        // 记录当前已解锁的 Keys 的字典
        private Dictionary<string, System.Tuple<ExpUpgradeItem, Image>> mUnlockedKeys = new Dictionary<string, System.Tuple<ExpUpgradeItem, Image>>();

        // 用 ResLoader 动态加载贴图资源
        ResLoader mResLoader = ResLoader.Allocate();
        private SpriteAtlas mIconAtlas;

        private void Awake()
        {
            UnlockedIconTemplate.Hide();

            mIconAtlas = mResLoader.LoadSync<SpriteAtlas>("icon");
            foreach (ExpUpgradeItem expUpgradeItem in this.GetSystem<ExpUpgradeSystem>().Items)
            {
                ExpUpgradeItem cachedItem = expUpgradeItem;
                expUpgradeItem.CurrentLevel.RegisterWithInitValue(level =>
                {
                    if (level > 1)
                    {
                        if (mUnlockedKeys.ContainsKey(cachedItem.Key) == false)
                        {
                            UnlockedIconTemplate.InstantiateWithParent(UnlockedIconRoot)
                                .Self(self =>
                                {
                                    self.sprite = mIconAtlas.GetSprite(cachedItem.IconName);
                                    // 将 cachedItem 和 self 组成一个 Tuple 加到 mUnlockedKeys 字典中
                                    // 保证其不会重复加载
                                    mUnlockedKeys.Add(cachedItem.Key, new System.Tuple<ExpUpgradeItem, Image>(cachedItem, self));
                                })
                                .Show();
                        }
                    }

                }).UnRegisterWhenGameObjectDestroyed(gameObject);
            }

            RegistSuperAbilitiesIcon(Global.SuperSimpleSword, "simple_sword");
            RegistSuperAbilitiesIcon(Global.SuperKnife, "simple_knife");
            RegistSuperAbilitiesIcon(Global.SuperRotateSword, "rotate_sword");
            RegistSuperAbilitiesIcon(Global.SuperBasketball, "simple_basketball");
            RegistSuperAbilitiesIcon(Global.SuperBomb, "simple_bomb");
        }

        private void RegistSuperAbilitiesIcon(BindableProperty<bool> isSuperAbility, string key)
        {
            isSuperAbility.Register(unlocked =>
            {
                if (unlocked)
                {
                    if (mUnlockedKeys.ContainsKey(key))
                    {
                        ExpUpgradeItem item = mUnlockedKeys[key].Item1;
                        Sprite sprite = mIconAtlas.GetSprite(item.PairedIconName);
                        mUnlockedKeys[key].Item2.sprite = sprite;
                        // Item1 对应 ExpUpgradeItem
                        // Item2 对应 Image
                    }
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        protected override void OnBeforeDestroy()
        {
            // 记得在 OnBeforeDestroy 里把 ResLoader 置空
            mResLoader.Recycle2Cache();
            mResLoader = null;
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}