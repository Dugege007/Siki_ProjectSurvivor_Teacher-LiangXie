using UnityEngine;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectSurvivor
{
    public class UIGameStartPanelData : UIPanelData
    {
    }

    public partial class UIGameStartPanel : UIPanel, IController, ICanSave
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGameStartPanelData ?? new UIGameStartPanelData();
            // please add init code here

            Time.timeScale = 1.0f;

            StartGameBtn.onClick.AddListener(() =>
            {
                this.CloseSelf();
                // 重置数据
                Global.ResetData();
                SceneManager.LoadScene("Game");
            });

            CoinUpgradeBtn.onClick.AddListener(() =>
            {
                CoinUpgradePanel.Show();
            });

            ResetUpgradeBtn.onClick.AddListener(() =>
            {
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

            saveSystem.SaveFloat(nameof(Global.CoinPercent), 0.1f);
            saveSystem.SaveFloat(nameof(Global.ExpPercent), 0.4f);
            saveSystem.SaveFloat(nameof(Global.HPPercent), 0.05f);
            saveSystem.SaveFloat(nameof(Global.SimpleBombChance), 0.1f);
            saveSystem.SaveFloat(nameof(Global.GetAllExpPercent), 0.05f);

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
