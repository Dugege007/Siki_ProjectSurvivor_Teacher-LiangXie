/****************************************************************************
 * 2023.10 MSI
 ****************************************************************************/

using QAssetBundle;
using QFramework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

namespace ProjectSurvivor
{
    public partial class TreasureChestPanel : UIElement, IController
    {
        ResLoader mResLoader = ResLoader.Allocate();

        private void Awake()
        {
            OKBtn.onClick.AddListener(() =>
            {
                Time.timeScale = 1.0f;
                this.Hide();
            });
        }

        private void OnEnable()
        {
            // 1. 判断是否有匹配的，没合成的
            // 2. 判断是否有没升级完成的
            ExpUpgradeSystem expUpgradeSystem = this.GetSystem<ExpUpgradeSystem>();

            var matchedPairedItems = expUpgradeSystem.Items.Where(item =>
            {
                // 如果当前能力大于 9 级，配对技能的名字不是空的
                if (item.CurrentLevel.Value >= 9 && item.PairedName.IsNotNullAndEmpty())
                {
                    // 字典中是否存在匹配项
                    bool containsInPair = expUpgradeSystem.Pairs.ContainsKey(item.Key);
                    // 获取匹配能力的 Key
                    string pairedItemKey = expUpgradeSystem.Pairs[item.Key];
                    // 配对能力的等级是否至少为 1 级
                    bool pairedItemStartUpgrade = expUpgradeSystem.ExpUpgradeDict[pairedItemKey].CurrentLevel.Value > 0;
                    // 组合能力是否已解锁
                    bool pairedUnlocked = expUpgradeSystem.PairedProperties[item.Key].Value;

                    return containsInPair && pairedItemStartUpgrade && !pairedUnlocked;
                }

                return false;
            });

            // 如果可以有任意超级武器
            if (matchedPairedItems.Any())
            {
                ExpUpgradeItem item = matchedPairedItems.ToList().GetRandomItem();
                Content.text = "<b>" + item.PairedName + "</b>\n" + item.PairedDescription;

                while (!item.UpgradeFinish)
                {
                    item.Upgrade();
                }

                Icon.sprite = mResLoader.LoadSync<SpriteAtlas>("icon")
                    .GetSprite(item.PairedIconName);

                Icon.Show();

                expUpgradeSystem.PairedProperties[item.Key].Value = true;
            }
            // 如果没有超级武器
            else
            {
                List<ExpUpgradeItem> expUpgradeItems =
                    expUpgradeSystem.Items
                    .Where(item => item.CurrentLevel.Value > 1 && !item.UpgradeFinish)
                    .ToList();

                // 如果 expUpgradeItems 中有任何一个
                if (expUpgradeItems.Any())
                {
                    ExpUpgradeItem item = expUpgradeItems.GetRandomItem();
                    Content.text = item.Description;

                    Icon.sprite = mResLoader.LoadSync<SpriteAtlas>("icon")
                        .GetSprite(item.IconName);

                    Icon.Show();


                    item.Upgrade();
                }
                else
                {
                    Icon.Hide();

                    if (Random.Range(0, 1.0f) < 0.2f)
                    {
                        Content.text = "生命值和上限 + 1";
                        AudioKit.PlaySound(Sfx.HP);
                        Global.MaxHP.Value++;
                        Global.HP.Value++;

                        return;
                    }

                    Content.text = "获得 50 金币";
                    Global.Coin.Value += 50;
                }
            }
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