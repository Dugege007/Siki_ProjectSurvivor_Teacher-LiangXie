/****************************************************************************
 * 2023.10 MSI
 ****************************************************************************/

using QAssetBundle;
using QFramework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProjectSurvivor
{
    public partial class TreasureChestPanel : UIElement, IController
    {
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
            ExpUpgradeSystem expUpgradeSystem = this.GetSystem<ExpUpgradeSystem>();
            List<ExpUpgradeItem> expUpgradeItems = 
                expUpgradeSystem.Items
                .Where(item => item.CurrentLevel.Value > 1 && !item.UpgradeFinish)
                .ToList();

            // ��� expUpgradeItems �����κ�һ��
            if (expUpgradeItems.Any())
            {
                ExpUpgradeItem item = expUpgradeItems.GetRandomItem();
                Content.text = item.Description;
                item.Upgrade();
            }
            else
            {
                if (Random.Range(0, 1.0f) < 0.2f)
                {
                    Content.text = "����ֵ������ + 1";
                    AudioKit.PlaySound(Sfx.HP);
                    Global.MaxHP.Value++;
                    Global.HP.Value++;

                    return;
                }

                Content.text = "��� 50 ���";
                Global.Coin.Value += 50;
                
            }
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