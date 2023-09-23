using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectSurvivor
{
    public class UIGameOverPanelData : UIPanelData
    {
    }
    public partial class UIGameOverPanel : UIPanel
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGameOverPanelData ?? new UIGameOverPanelData();
            // please add init code here

            // ActionKit 动作序列工具（时序异步）
            // 获得全局的 Update 周期
            ActionKit.OnUpdate.Register(() =>
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // 关掉当前面板
                    this.CloseSelf();
                    // 重置数据
                    Global.ResetData();
                    // 加载游戏场景
                    SceneManager.LoadScene("Game");
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            BackToStartBtn.onClick.AddListener(() =>
            {
                this.CloseSelf();
                Global.ResetData();
                SceneManager.LoadScene("GameStart");
            });

            RestartGameBtn.onClick.AddListener(() =>
            {
                this.CloseSelf();
                Global.ResetData();
                SceneManager.LoadScene("Game");
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
    }
}
