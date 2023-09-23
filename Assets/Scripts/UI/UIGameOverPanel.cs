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

            // ActionKit �������й��ߣ�ʱ���첽��
            // ���ȫ�ֵ� Update ����
            ActionKit.OnUpdate.Register(() =>
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    // �ص���ǰ���
                    this.CloseSelf();
                    // ��������
                    Global.ResetData();
                    // ������Ϸ����
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
