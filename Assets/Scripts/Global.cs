using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public class Global : Architecture<Global>
    {
        protected override void Init()
        {
            // ע��ģ��
            this.RegisterSystem(new SaveSystem());
            this.RegisterSystem(new CoinUpgradeSystem());
            this.RegisterSystem(new ExpUpgradeSystem());
        }

        #region Model
        /// <summary>
        /// �������ֵ
        /// </summary>
        public static BindableProperty<int> HP = new(3);

        /// <summary>
        /// ����������ֵ
        /// </summary>
        public static BindableProperty<int> MaxHP = new(3);

        /// <summary>
        /// ����ֵ
        /// </summary>
        public static BindableProperty<int> Exp = new(0);

        /// <summary>
        /// ���
        /// </summary>
        public static BindableProperty<int> Coin = new(0);

        /// <summary>
        /// �ȼ�
        /// </summary>
        public static BindableProperty<int> Level = new(1);

        /// <summary>
        /// ��ǰʱ��
        /// </summary>
        public static BindableProperty<float> CurrentSeconds = new(0);

        /// <summary>
        /// �������Ĺ�����
        /// </summary>
        public static BindableProperty<float> SimpleAbilityDamage = new(1);

        /// <summary>
        /// �������Ĺ������
        /// </summary>
        public static BindableProperty<float> SimpleAbilityDuration = new(1.5f);

        /// <summary>
        /// �������
        /// </summary>
        public static BindableProperty<float> ExpPercent = new(0.4f);

        /// <summary>
        /// ��ҵ���
        /// </summary>
        public static BindableProperty<float> CoinPercent = new(0.1f);

        /// <summary>
        /// ����ֵ����
        /// </summary>
        public static BindableProperty<float> HPPercent = new(0.05f);

        /// <summary>
        /// ը������
        /// </summary>
        public static BindableProperty<float> BombPercent = new(0.1f);

        /// <summary>
        /// ը������
        /// </summary>
        public static BindableProperty<float> GetAllExpPercent = new(0.1f);
        #endregion

        /// <summary>
        /// �Զ���ʼ��
        /// </summary>
        /// <remark>�÷���������ʱ�Զ�����һ��</remark>
        [RuntimeInitializeOnLoadMethod]
        public static void AutoInit()
        {
            UIKit.Root.SetResolution(1920, 1080, 0.5f);

            Global.MaxHP.Value = PlayerPrefs.GetInt(nameof(MaxHP), 3);
            HP.Value = MaxHP.Value;

            // �򵥵Ĵ洢����
            // ��ȡ�������
            Global.Coin.Value = PlayerPrefs.GetInt(nameof(Coin), 0);
            Global.CoinPercent.Value = PlayerPrefs.GetFloat(nameof(CoinPercent), 0.1f);
            Global.ExpPercent.Value = PlayerPrefs.GetFloat(nameof(ExpPercent), 0.4f);
            Global.HPPercent.Value = PlayerPrefs.GetFloat(nameof(HPPercent), 0.05f);
            Global.BombPercent.Value = PlayerPrefs.GetFloat(nameof(BombPercent), 0.1f);
            Global.GetAllExpPercent.Value = PlayerPrefs.GetFloat(nameof(GetAllExpPercent), 0.05f);

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

            Global.HPPercent.Register(hpPercent =>
            {
                PlayerPrefs.SetFloat(nameof(HPPercent), hpPercent);
            });

            Global.BombPercent.Register(bombPercent =>
            {
                PlayerPrefs.SetFloat(nameof(BombPercent), bombPercent);
            });

            Global.GetAllExpPercent.Register(getAllExpPercent =>
            {
                PlayerPrefs.SetFloat(nameof(GetAllExpPercent), getAllExpPercent);
            });

            Global.MaxHP.Register(maxHp =>
            {
                PlayerPrefs.SetInt(nameof(MaxHP), maxHp);
            });
        }

        /// <summary>
        /// ��������
        /// </summary>
        public static void ResetData()
        {
            HP.Value = MaxHP.Value;
            Exp.Value = 0;
            Level.Value = 1;
            EnemyGenerator.EnemyCount.Value = 0;
            CurrentSeconds.Value = 0;
            SimpleAbilityDamage.Value = 1;
            SimpleAbilityDuration.Value = 1.5f;
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
                // ���侭��ֵ
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

                return;
            }

            percent = Random.Range(0, 1f);
            if (percent <= HPPercent)
            {
                // ��������ֵ
                PowerUpManager.Default.HP.Instantiate()
                    .Position(gameObject.Position())
                    .Show();

                return;
            }

            percent = Random.Range(0, 1f);
            if (percent <= BombPercent)
            {
                // ����ը��
                PowerUpManager.Default.Bomb.Instantiate()
                    .Position(gameObject.Position())
                    .Show();

                return;
            }

            percent = Random.Range(0, 1f);
            if (percent <= GetAllExpPercent)
            {
                // �������վ���
                PowerUpManager.Default.GetAllExp.Instantiate()
                    .Position(gameObject.Position())
                    .Show();

                return;
            }
        }
    }
}
