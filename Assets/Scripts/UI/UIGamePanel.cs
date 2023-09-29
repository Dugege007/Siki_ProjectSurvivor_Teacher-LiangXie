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

            // �����������ֵ
            Global.HP.RegisterWithInitValue(hp =>
            {
                HPText.text = hp + "/" + Global.MaxHP.Value;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.MaxHP.RegisterWithInitValue(maxHp =>
            {
                HPText.text = Global.HP.Value + "/" + maxHp;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // ���µ�������UI
            EnemyGenerator.EnemyCount.RegisterWithInitValue(enemyCount =>
            {
                EnemyCountText.text = enemyCount.ToString();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

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
                ExpText.text = "(" + exp + "/" + Global.ExpToNextLevel() + ")";

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // ���µȼ�UI
            Global.Level.RegisterWithInitValue(level =>
            {
                LevelText.text = level.ToString();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            ExpUpgradePanel.Hide();
            // ���µȼ�UI
            Global.Level.Register(level =>
            {
                Time.timeScale = 0;
                ExpUpgradePanel.Show();
                AudioKit.PlaySound("LevelUp");

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            // ��������
            Global.Exp.RegisterWithInitValue(exp =>
            {
                if (exp >= Global.ExpToNextLevel())
                {
                    Global.Exp.Value -= Global.ExpToNextLevel();
                    Global.Level.Value++;
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);


            EnemyGenerator enemyGenerator = FindObjectOfType<EnemyGenerator>();
            // ��ȫ�ֵ� Update ��ע��ʱ�����ӵ�����
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
