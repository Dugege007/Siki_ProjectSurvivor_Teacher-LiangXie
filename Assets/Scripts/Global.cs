using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public class Global : Architecture<Global>
    {
        protected override void Init()
        {
            // ע��ģ��

        }

        #region Model
        /// <summary>
        /// ����ֵ
        /// </summary>
        public static BindableProperty<int> Exp = new BindableProperty<int>(0);

        /// <summary>
        /// ���
        /// </summary>
        public static BindableProperty<int> Coin = new BindableProperty<int>(0);

        /// <summary>
        /// �ȼ�
        /// </summary>
        public static BindableProperty<int> Level = new BindableProperty<int>(1);

        /// <summary>
        /// ��ǰʱ��
        /// </summary>
        public static BindableProperty<float> CurrentSeconds = new BindableProperty<float>(0);

        /// <summary>
        /// �������Ĺ�����
        /// </summary>
        public static BindableProperty<float> SimpleAbilityDamage = new BindableProperty<float>(1);

        /// <summary>
        /// �������Ĺ������
        /// </summary>
        public static BindableProperty<float> SimpleAbilityDuration = new BindableProperty<float>(1.5f);

        /// <summary>
        /// �������
        /// </summary>
        public static BindableProperty<float> ExpPercent = new BindableProperty<float>(0.4f);

        /// <summary>
        /// ��ҵ���
        /// </summary>
        public static BindableProperty<float> CoinPercent = new BindableProperty<float>(0.05f);
        #endregion

        /// <summary>
        /// �Զ���ʼ��
        /// </summary>
        /// <remark>�÷���������ʱ�Զ�����һ��</remark>
        [RuntimeInitializeOnLoadMethod]
        public static void AutoInit()
        {
            UIKit.Root.SetResolution(1920, 1080, 0.5f);

            // �򵥵Ĵ洢����
            // ��ȡ�������
            Global.Coin.Value = PlayerPrefs.GetInt(nameof(Coin), 0);
            Global.CoinPercent.Value = PlayerPrefs.GetFloat(nameof(ExpPercent), 0.1f);
            Global.ExpPercent.Value = PlayerPrefs.GetFloat(nameof(ExpPercent), 0.4f);

            // ����������
            Global.Coin.Register(coin =>
            {
                PlayerPrefs.SetInt(nameof(Coin), coin);

            }); // ��ʱ������ע��

            Global.CoinPercent.Register(coinPercent =>
            {
                PlayerPrefs.SetFloat(nameof(CoinPercent), coinPercent);
            });

            Global.ExpPercent.Register(expPercent =>
            {
                PlayerPrefs.SetFloat(nameof(ExpPercent), expPercent);
            });
        }

        /// <summary>
        /// ��������
        /// </summary>
        public static void ResetData()
        {
            Exp.Value = 0;
            Level.Value = 1;
            CurrentSeconds.Value = 0;
            SimpleAbilityDamage.Value = 1;
            SimpleAbilityDuration.Value = 1.5f;
            EnemyGenerator.EnemyCount.Value = 0;
        }

        public static int ExpToNextLevel()
        {
            return Level.Value * 5;
        }

        /// <summary>
        /// ���ɵ�����Ʒ
        /// </summary>
        /// <param name="gameObject">������Ʒ������</param>
        public static void GeneratePowerUp(GameObject gameObject)
        {
            float percent = Random.Range(0, 1f);
            if (percent <= ExpPercent)
            {
                // 90% ���侭��ֵ
                PowerUpManager.Default.Exp.Instantiate()
                    .Position(gameObject.Position())
                    .Show();
            }

            percent = Random.Range(0, 1f);
            if (percent <= CoinPercent)
            {
                // ������
                PowerUpManager.Default.Coin.Instantiate()
                    .Position(gameObject.Position())
                    .Show();
            }
        }
    }
}
