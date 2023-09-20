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

            // 获得全局的 Update 周期
            ActionKit.OnUpdate.Register(() =>
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // 关掉当前面板
                    this.CloseSelf();
                    // 加载游戏场景
                    SceneManager.LoadScene("SampleScene");
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
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
