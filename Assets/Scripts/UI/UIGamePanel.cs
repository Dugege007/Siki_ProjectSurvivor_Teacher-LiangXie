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

            // 更新当前时间UI
            Global.CurrentSeconds.RegisterWithInitValue(currentSeconds =>
            {
                // 每 20 帧更新一次
                if (Time.frameCount % 20 == 0)
                {
                    int currentSecondsInt = Mathf.FloorToInt(currentSeconds);
                    int seconds = currentSecondsInt % 60;
                    int minutes = currentSecondsInt / 60;

                    TimeText.text = $"{minutes:00}:{seconds:00}";
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

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

            // 注册按钮事件
            UpgradeBtn.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                Global.SimpleAbilityDamage.Value *= 1.5f;
                UpgradeBtn.Hide();
            });

            // 隐藏按钮
            UpgradeBtn.Hide();

            EnemyGenerator enemyGenerator = FindObjectOfType<EnemyGenerator>();
            // 在全局的 Update 中注册时间增加的任务
            ActionKit.OnUpdate.Register(() =>
            {
                Global.CurrentSeconds.Value += Time.deltaTime;

                if (Global.CurrentSeconds.Value > 60 && enemyGenerator.IsLastWave && FindObjectOfType<Enemy>(false))    // FindObjectOfType<Enemy>(false) false 表示不包含隐藏的
                {
                    UIKit.OpenPanel<UIGamePassPanel>();
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
