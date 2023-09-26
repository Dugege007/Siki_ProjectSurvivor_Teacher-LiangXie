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

            // 监听金币变更
            Global.Coin.RegisterWithInitValue(coin =>
            {
                CoinText.text = "金币：" + coin;

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

            // 监听金币掉率升级按钮
            CoinPercentUpgradeBtn.onClick.AddListener(() =>
            {
                Global.CoinPercent.Value += 0.1f;
                Global.Coin.Value -= 5;

                AudioKit.PlaySound("AbilityLevelUp");
            });

            // 监听经验掉率升级按钮
            ExpPercentUpgradeBtn.onClick.AddListener(() =>
            {
                Global.ExpPercent.Value += 0.1f;
                Global.Coin.Value -= 5;

                AudioKit.PlaySound("AbilityLevelUp");
            });

            // 监听生命值升级按钮
            MaxHPUpgradeBtn.onClick.AddListener(() =>
            {
                Global.MaxHP.Value++;
                Global.Coin.Value -= 30;

                AudioKit.PlaySound("AbilityLevelUp");
            });

            // 监听关闭按钮
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
