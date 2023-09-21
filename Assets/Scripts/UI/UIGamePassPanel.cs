using UnityEngine;
using UnityEngine.UI;
using QFramework;
using UnityEngine.SceneManagement;

namespace ProjectSurvivor
{
    public class UIGamePassPanelData : UIPanelData
    {
    }
    public partial class UIGamePassPanel : UIPanel
    {
        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGamePassPanelData ?? new UIGamePassPanelData();
            // please add init code here

            // 暂停游戏
            Time.timeScale = 0;

            // 在全局 Update 中注册监听按键
            ActionKit.OnUpdate.Register(() =>
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // 关掉当前面板
                    this.CloseSelf();
                    // 重置数据
                    Global.ResetData();
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
            // 恢复时间
            Time.timeScale = 1;
        }
    }
}
