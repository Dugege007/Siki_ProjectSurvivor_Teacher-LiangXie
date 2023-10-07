/****************************************************************************
 * 2023.10 MSI
 ****************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using QFramework;
using System.Xml.Schema;
using UnityEngine.U2D;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class AchievementController : UIElement
    {
        ResLoader mResLoader = ResLoader.Allocate();

        private void Awake()
        {
            float originLocalPosY = AchievementItem.LocalPosition().y;

            SpriteAtlas iconAtlas = mResLoader.LoadSync<SpriteAtlas>("icon");
            AchievementSystem.OnAchievementUnlocked.Register(item =>
            {
                Title.text = $"<b>{item.Name} ´ï³É£¡</b>";
                Description.text = item.Description;
                Sprite sprite = iconAtlas.GetSprite(item.IconName);
                Icon.sprite = sprite;
                AchievementItem.Show();

                AchievementItem.LocalPositionY(-300);
                AudioKit.PlaySound(Sfx.ACHIEVEMENT);

                ActionKit.Sequence()
                    .Lerp(-300, originLocalPosY, 0.3f, y => AchievementItem.LocalPositionY(y))
                    .Delay(2)
                    .Lerp(originLocalPosY, -300, 0.3f, y => AchievementItem.LocalPositionY(y), () => AchievementItem.Hide())
                    .Start(this);

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        protected override void OnBeforeDestroy()
        {
            mResLoader.Recycle2Cache();
            mResLoader = null;
        }
    }
}