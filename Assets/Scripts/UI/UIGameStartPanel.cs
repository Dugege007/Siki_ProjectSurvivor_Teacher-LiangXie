using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;
using QAssetBundle;

namespace ProjectSurvivor
{
    public class UIGameStartPanelData : UIPanelData
    {
    }

    public partial class UIGameStartPanel : UIPanel, IController, ICanSave
    {
        protected override void OnInit(IUIData uiData = null)
        {
            CoinUpgradePanel.Hide();
            AchievementPanel.Hide();

            mData = uiData as UIGameStartPanelData ?? new UIGameStartPanelData();
            // please add init code here

            Time.timeScale = 1.0f;

            StartGameBtn.onClick.AddListener(() =>
            {
                AudioKit.PlaySound(Sfx.BUTTONCLICK);
                this.CloseSelf();
                // 重置数据
                Global.ResetData();
                SceneManager.LoadScene("Game");
            });

            CoinUpgradeBtn.onClick.AddListener(() =>
            {
                AudioKit.PlaySound(Sfx.BUTTONCLICK);
                CoinUpgradePanel.Show();
            });

            AchievementBtn.onClick.AddListener(() =>
            {
                AudioKit.PlaySound(Sfx.BUTTONCLICK);
                AchievementPanel.Show();
            });

            ResetUpgradeBtn.onClick.AddListener(() =>
            {
                AudioKit.PlaySound(Sfx.BUTTONCLICK);
                // 重置已升级的数据
                Global.MaxHP.Value = 3;
                ResetUpgrade();
            });
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

        public void Save()
        {

        }

        public void ResetUpgrade()
        {
            SaveSystem saveSystem = this.GetSystem<SaveSystem>();

            saveSystem.SaveFloat(nameof(Global.CoinPercent), 0.05f);
            saveSystem.SaveFloat(nameof(Global.ExpPercent), 0.4f);
            saveSystem.SaveFloat(nameof(Global.HPPercent), 0.02f);
            saveSystem.SaveFloat(nameof(Global.SimpleBombChance), ConfigManager.Instance.SimpleBombConfig.InitChance);
            saveSystem.SaveFloat(nameof(Global.GetAllExpPercent), 0.02f);
            saveSystem.SaveFloat(nameof(Global.TreasureChestPercent), 0.001f);

            CoinUpgradeSystem coinUpgradeSystem = this.GetSystem<CoinUpgradeSystem>();

            foreach (var coinUpgradeItem in coinUpgradeSystem.Items)
            {
                saveSystem.SaveBool(coinUpgradeItem.Key, false);
            }
        }

        public void Load()
        {
            
        }
    }
}
