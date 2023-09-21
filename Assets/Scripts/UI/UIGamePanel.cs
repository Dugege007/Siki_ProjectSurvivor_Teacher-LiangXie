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

            // ���µ�ǰʱ��UI
            Global.CurrentSeconds.RegisterWithInitValue(currentSeconds =>
            {
                // ÿ 20 ֡����һ��
                if (Time.frameCount % 20 == 0)
                {
                    int currentSecondsInt = Mathf.FloorToInt(currentSeconds);
                    int seconds = currentSecondsInt % 60;
                    int minutes = currentSecondsInt / 60;

                    TimeText.text = $"{minutes:00}:{seconds:00}";
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // ���¾���ֵUI
            Global.Exp.RegisterWithInitValue(exp =>
            {
                ExpText.text = exp.ToString();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // ���µȼ�UI
            Global.Level.RegisterWithInitValue(level =>
            {
                LevelText.text = level.ToString();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // ���µȼ�UI
            Global.Level.Register(level =>
            {
                Time.timeScale = 0;
                UpgradeBtn.Show();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // ��������
            Global.Exp.RegisterWithInitValue(exp =>
            {
                if (exp >= 5)
                {
                    Global.Exp.Value -= 5;
                    Global.Level.Value++;
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // ע�ᰴť�¼�
            UpgradeBtn.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                Global.SimpleAbilityDamage.Value *= 1.5f;
                UpgradeBtn.Hide();
            });

            // ���ذ�ť
            UpgradeBtn.Hide();

            // ��ȫ�ֵ� Update ��ע��ʱ�����ӵ�����
            ActionKit.OnUpdate.Register(() =>
            {
                Global.CurrentSeconds.Value += Time.deltaTime;

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
