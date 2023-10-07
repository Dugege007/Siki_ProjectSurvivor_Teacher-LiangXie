/****************************************************************************
 * 2023.10 MSI
 ****************************************************************************/

using QAssetBundle;
using QFramework;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace ProjectSurvivor
{
    public partial class AchievementPanel : UIElement, IController
    {
        ResLoader mResloader = ResLoader.Allocate();

        private void Awake()
        {
            AchievementItemTempleteBtn.Hide();

            SpriteAtlas iconAtlas = mResloader.LoadSync<SpriteAtlas>("icon");
            foreach (var achievementItem in this.GetSystem<AchievementSystem>().Items.OrderByDescending(item => item.Unlocked))
            // OrderByDescending(item => item.Unlocked) 未完成的排在前面
            {
                AchievementItemTempleteBtn.InstantiateWithParent(AchievementItemRoot)
                    .Self(self =>
                    {
                        Button selfCache = self;

                        selfCache.GetComponentInChildren<Text>().text = "<b>" + achievementItem.Name + "</b>\n" + "第一次" + achievementItem.Description;
                        if (achievementItem.Unlocked)
                        {
                            selfCache.enabled = true;
                            UnlockText.Show();
                        }
                        else
                        {
                            selfCache.enabled = false;
                            UnlockText.Hide();
                        }

                        Sprite sprite = iconAtlas.GetSprite(achievementItem.IconName);
                        selfCache.transform.Find("Icon").GetComponent<Image>().sprite = sprite;
                    })
                    .Show();
            }

            CloseBtn.onClick.AddListener(() =>
            {
                AudioKit.PlaySound(Sfx.BUTTONCLICK);
                this.Hide();
            });
        }

        protected override void OnBeforeDestroy()
        {
            mResloader.Recycle2Cache();
            mResloader = null;
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}