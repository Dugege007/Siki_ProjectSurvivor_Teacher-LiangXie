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

            // ÔÝÍ£ÓÎÏ·
            Time.timeScale = 0;

            BackToStartBtn.onClick.AddListener(() =>
            {
                this.CloseSelf();
                Player.Default.DestroyGameObjGracefully();
                Global.ResetData();
                SceneManager.LoadScene("GameStart");
            });

            RestartGameBtn.onClick.AddListener(() =>
            {
                this.CloseSelf();
                Player.Default.DestroyGameObjGracefully();
                Global.ResetData();
                SceneManager.LoadScene("Game");
            });

            AudioKit.PlaySound("GamePass");
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
            // »Ö¸´Ê±¼ä
            Time.timeScale = 1;
        }
    }
}
