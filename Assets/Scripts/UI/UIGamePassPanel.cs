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

            // ��ͣ��Ϸ
            Time.timeScale = 0;

            // ��ȫ�� Update ��ע���������
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
            // �ָ�ʱ��
            Time.timeScale = 1;
        }
    }
}
