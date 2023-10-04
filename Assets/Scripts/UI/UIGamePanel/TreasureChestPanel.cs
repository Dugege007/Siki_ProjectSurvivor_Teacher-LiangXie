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
            // 1. �ж��Ƿ���ƥ��ģ�û�ϳɵ�
            // 2. �ж��Ƿ���û������ɵ�
            ExpUpgradeSystem expUpgradeSystem = this.GetSystem<ExpUpgradeSystem>();

            var matchedPairedItems = expUpgradeSystem.Items.Where(item =>
            {
                // �����ǰ�������� 7 ��
                if (item.CurrentLevel.Value >= 7)
                {
                    // �ֵ����Ƿ����ƥ����
                    bool containsInPair = expUpgradeSystem.Pairs.ContainsKey(item.Key);
                    // ��ȡƥ�������� Key
                    string pairedItemKey = expUpgradeSystem.Pairs[item.Key];
                    // ��������ĵȼ��Ƿ�����Ϊ 1 ��
                    bool pairedItemStartUpgrade = expUpgradeSystem.ExpUpgradeDict[pairedItemKey].CurrentLevel.Value > 0;
                    // ��������Ƿ��ѽ���
                    bool pairedUnlocked = expUpgradeSystem.PairedProperties[item.Key].Value;

                    return containsInPair && pairedItemStartUpgrade && !pairedUnlocked;
                }

                return false;
            });

            // ������������ⳬ������
            if (matchedPairedItems.Any())
            {
                ExpUpgradeItem item = matchedPairedItems.ToList().GetRandomItem();
                Content.text = "<b>" + "�ϳɺ��" + item.Key + "<b>\n";

                while (!item.UpgradeFinish)
                {
                    item.Upgrade();
                }

                expUpgradeSystem.PairedProperties[item.Key].Value = true;
            }
            // ���û�г�������
            else
            {
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