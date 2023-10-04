using UnityEngine;
using UnityEngine.UI;
using QFramework;
using QAssetBundle;

namespace ProjectSurvivor
{
    public class UIGamePanelData : UIPanelData
    {
    }
    public partial class UIGamePanel : UIPanel
    {
        public static EasyEvent FlashScreen = new EasyEvent();
        public static EasyEvent OpenTreasurePanel = new EasyEvent();

        protected override void OnInit(IUIData uiData = null)
        {
            mData = uiData as UIGamePanelData ?? new UIGamePanelData();
            // please add init code here

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
                ExpValue.fillAmount = exp / (float)Global.ExpToNextLevel();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 更新等级UI
            Global.Level.RegisterWithInitValue(level =>
            {
                LevelText.text = level.ToString();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            ExpUpgradePanel.Hide();
            // 更新等级UI
            Global.Level.Register(level =>
            {
                Time.timeScale = 0;
                ExpUpgradePanel.Show();
                AudioKit.PlaySound(Sfx.LEVELUP);

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


            EnemyGenerator enemyGenerator = FindObjectOfType<EnemyGenerator>();
            // 在全局的 Update 中注册时间增加的任务
            ActionKit.OnUpdate.Register(() =>
            {
                Global.CurrentSeconds.Value += Time.deltaTime;

                if (enemyGenerator.IsLastWave &&
                    enemyGenerator.CurrentWave == null &&
                    EnemyGenerator.EnemyCount.Value == 0)
                {
                    this.CloseSelf();
                    UIKit.OpenPanel<UIGamePassPanel>();
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.Coin.RegisterWithInitValue(coin =>
            {
                CoinText.text = coin.ToString();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 用于测试的按钮
            // 加经验
            ExpUpTestBtn.onClick.AddListener(() =>
            {
                Global.Exp.Value += 10 * (1 + Global.AdditionalExpRate.Value);
                AudioKit.PlaySound(Sfx.GETALLEXP);
            });

            // 清屏
            ClearEnemyBtn.onClick.AddListener(() =>
            {
                // 与炸弹的代码相同
                foreach (var enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    Enemy enemy = enemyObj.GetComponent<Enemy>();

                    if (enemy && enemy.gameObject.activeSelf)
                    {
                        enemy.Hurt(enemy.HP);
                    }
                }

                AudioKit.PlaySound(Sfx.BOMB);
                CameraController.Shake();
            });

            // 闪屏效果
            FlashScreen.Register(() =>
            {
                ActionKit.Sequence()
                    .Lerp(0, 0.5f, 0.1f, alpha => ScreenColor.ColorAlpha(alpha))
                    .Lerp(0.5f, 0, 0.2f, alpha => ScreenColor.ColorAlpha(alpha),
                    () => ScreenColor.ColorAlpha(0))
                    .Start(this);

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // 注册宝箱面板
            OpenTreasurePanel.Register(() =>
            {
                Time.timeScale = 0f;
                TreasureChestPanel.Show();

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
