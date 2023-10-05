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
        // ��¼��ǰ�ѽ����� Keys ���ֵ�
        private Dictionary<string, System.Tuple<ExpUpgradeItem, Image>> mUnlockedKeys = new Dictionary<string, System.Tuple<ExpUpgradeItem, Image>>();

        // �� ResLoader ��̬������ͼ��Դ
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
                                    // �� cachedItem �� self ���һ�� Tuple �ӵ� mUnlockedKeys �ֵ���
                                    // ��֤�䲻���ظ�����
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
                        // Item1 ��Ӧ ExpUpgradeItem
                        // Item2 ��Ӧ Image
                    }
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        protected override void OnBeforeDestroy()
        {
            // �ǵ��� OnBeforeDestroy ��� ResLoader �ÿ�
            mResLoader.Recycle2Cache();
            mResLoader = null;
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}