using UnityEngine;
using UnityEngine.UI;
using QFramework;

namespace ProjectSurvivor
{
    public class UIGamePanelData : UIPanelData
    {
    }
    public partial class UIGamePanel : UIPanel
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGamePanelData ?? new UIGamePanelData();
            // please add init code here

            // 更新经验值UI
            Global.Exp.RegisterWithInitValue(exp =>
            {
                ExpText.text = exp.ToString();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 更新等级UI
            Global.Level.RegisterWithInitValue(level =>
            {
                LevelText.text = level.ToString();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 更新等级UI
            Global.Level.Register(level =>
            {
                Time.timeScale = 0;
                UpgradeBtn.Show();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 升级机制
            Global.Exp.RegisterWithInitValue(exp =>
            {
                if (exp >= 5)
                {
                    Global.Exp.Value -= 5;
                    Global.Level.Value++;
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            UpgradeBtn.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                UpgradeBtn.Hide();
            });

            UpgradeBtn.Hide();
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
    }
}
