using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectSurvivor
{
    public class UIGameStartPanelData : UIPanelData
    {
    }

    public partial class UIGameStartPanel : UIPanel, IController
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGameStartPanelData ?? new UIGameStartPanelData();
            // please add init code here

            Time.timeScale = 1.0f;

            StartGameBtn.onClick.AddListener(() =>
            {
                this.CloseSelf();
                SceneManager.LoadScene("Game");
            });

            CoinUpgradeBtn.onClick.AddListener(() =>
            {
                CoinUpgradePanel.Show();
            });

            // ������ұ��
            Global.Coin.RegisterWithInitValue(coin =>
            {
                CoinText.text = "��ң�" + coin;

                if (coin>=5)
                {
                    CoinPercentUpgradeBtn.Show();
                    ExpPercentUpgradeBtn.Show();
                }
                else
                {
                    CoinPercentUpgradeBtn.Hide();
                    ExpPercentUpgradeBtn.Hide();
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // ������ҵ���������ť
            CoinPercentUpgradeBtn.onClick.AddListener(() =>
            {
                Global.CoinPercent.Value += 0.1f;
                Global.Coin.Value -= 5;

                AudioKit.PlaySound("AbilityLevelUp");
            });

            // �����������������ť
            ExpPercentUpgradeBtn.onClick.AddListener(() =>
            {
                Global.ExpPercent.Value += 0.1f;
                Global.Coin.Value -= 5;

                AudioKit.PlaySound("AbilityLevelUp");
            });

            // ��������ֵ������ť
            MaxHPUpgradeBtn.onClick.AddListener(() =>
            {
                Global.MaxHP.Value++;
                Global.Coin.Value -= 30;

                AudioKit.PlaySound("AbilityLevelUp");
            });

            // �����رհ�ť
            CloseBtn.onClick.AddListener(() =>
            {
                CoinUpgradePanel.Hide();
            });

            this.GetSystem<CoinUpGradeSystem>().Say();
        }
        
        protected override void OnOpen(IUIData uiData = null)
        {
        }
        
        protected override void OnShow()
        {
        }
        
        protected override void OnHide()
        {
        }
        
        protected override void OnClose()
        {
        }

        public IArchitecture GetArchitecture()
        {
            return Global.Interface;
        }
    }
}
