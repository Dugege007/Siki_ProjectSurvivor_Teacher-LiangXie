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

            // 更新玩家生命值
            Global.HP.RegisterWithInitValue(hp =>
            {
                HPText.text = hp + "/" + Global.MaxHP.Value;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.MaxHP.RegisterWithInitValue(maxHp =>
            {
                HPText.text = Global.HP.Value + "/" + maxHp;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 更新敌人数量UI
            EnemyGenerator.EnemyCount.RegisterWithInitValue(enemyCount =>
            {
                EnemyCountText.text = enemyCount.ToString();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

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
                ExpText.text = "(" + exp + "/" + Global.ExpToNextLevel() + ")";

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
                UpgradeRoot.Show();
                AudioKit.PlaySound("LevelUp");

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 升级机制
            Global.Exp.RegisterWithInitValue(exp =>
            {
                if (exp >= Global.ExpToNextLevel())
                {
                    Global.Exp.Value -= Global.ExpToNextLevel();
                    Global.Level.Value++;
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 注册按钮事件
            UpgradeBtn.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                Global.SimpleAbilityDamage.Value *= 1.5f;
                UpgradeRoot.Hide();
            });

            SimpleDurationUpgradeBtn.onClick.AddListener(() =>
            {
                Time.timeScale = 1;
                Global.SimpleAbilityDuration.Value *= 0.8f;
                UpgradeRoot.Hide();
            });

            // 隐藏按钮组
            UpgradeRoot.Hide();

            EnemyGenerator enemyGenerator = FindObjectOfType<EnemyGenerator>();
            // 在全局的 Update 中注册时间增加的任务
            ActionKit.OnUpdate.Register(() =>
            {
                Global.CurrentSeconds.Value += Time.deltaTime;

                if (enemyGenerator.IsLastWave &&
                    enemyGenerator.CurrentWave == null &&
                    EnemyGenerator.EnemyCount.Value == 0)
                {
                    UIKit.OpenPanel<UIGamePassPanel>();
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.Coin.RegisterWithInitValue(coin =>
            {
                CoinText.text = coin.ToString();

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
