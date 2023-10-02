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

        // �򵥽�
        public static BindableProperty<bool> SimpleSwordUnlocked = new(false);
        public static BindableProperty<float> SimpleSwordDamage = new(Player.Default.SimpleSwordConfig.InitDamage);
        public static BindableProperty<float> SimpleSwordDuration = new(Player.Default.SimpleSwordConfig.InitDuration);
        public static BindableProperty<float> SimpleSwordRange = new(Player.Default.SimpleSwordConfig.InitRange);
        public static BindableProperty<int> SimpleSwordCount = new(Player.Default.SimpleSwordConfig.InitCount);

        // ��ת��
        public static BindableProperty<bool> RotateSwordUnlocked = new(false);
        public static BindableProperty<float> RotateSwordDamage = new(Player.Default.RotateSwordConfig.InitDamage);
        public static BindableProperty<float> RotateSwordSpeed = new(Player.Default.RotateSwordConfig.InitSpeed);
        public static BindableProperty<float> RotateSwordRange = new(Player.Default.RotateSwordConfig.InitRange);
        public static BindableProperty<int> RotateSwordCount = new(Player.Default.RotateSwordConfig.InitCount);

        // �ɵ�
        public static BindableProperty<bool> SimpleKnifeUnlocked = new(false);
        public static BindableProperty<float> SimpleKnifeDamage = new(Player.Default.SimpleKnifeConfig.InitDamage);
        public static BindableProperty<float> SimpleKnifeDuration = new(Player.Default.SimpleKnifeConfig.InitDuration);
        public static BindableProperty<int> SimpleKnifeCount = new(Player.Default.SimpleKnifeConfig.InitCount);
        public static BindableProperty<int> SimpleKnifeAttackCount = new(Player.Default.SimpleKnifeConfig.InitAttackCount);

        // ����
        public static BindableProperty<bool> BasketballUnlocked = new(false);
        public static BindableProperty<float> BasketballDamage = new(Player.Default.BasketballConfig.InitDamage);
        public static BindableProperty<float> BasketballSpeed = new(Player.Default.BasketballConfig.InitSpeed);
        public static BindableProperty<int> BasketballCount = new(Player.Default.BasketballConfig.InitCount);

        // ը��
        public static BindableProperty<bool> SimpleBombUnlocked = new(false);
        public static BindableProperty<float> SimpleBombDamage = new(Player.Default.SimpleBombConfig.InitDamage);
        public static BindableProperty<float> SimpleBombPercent = new(Player.Default.SimpleBombConfig.InitPercent);

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
        /// ���վ������
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
            // ���� UI
            UIKit.Root.SetResolution(1920, 1080, 0.5f);



            // �򵥵Ĵ洢����
            // ��ȡ����
            Global.Coin.Value = PlayerPrefs.GetInt(nameof(Coin), 0);
            Global.CoinPercent.Value = PlayerPrefs.GetFloat(nameof(CoinPercent), 0.1f);
            Global.ExpPercent.Value = PlayerPrefs.GetFloat(nameof(ExpPercent), 0.4f);
            Global.HPPercent.Value = PlayerPrefs.GetFloat(nameof(HPPercent), 0.05f);
            Global.SimpleBombPercent.Value = PlayerPrefs.GetFloat(nameof(SimpleBombPercent), 0.1f);
            Global.GetAllExpPercent.Value = PlayerPrefs.GetFloat(nameof(GetAllExpPercent), 0.05f);

            Global.MaxHP.Value = PlayerPrefs.GetInt(nameof(MaxHP), 3);
            HP.Value = MaxHP.Value;

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

            Global.SimpleBombPercent.Register(bombPercent =>
            {
                PlayerPrefs.SetFloat(nameof(SimpleBombPercent), bombPercent);
            });

            Global.GetAllExpPercent.Register(getAllExpPercent =>
            {
                PlayerPrefs.SetFloat(nameof(GetAllExpPercent), getAllExpPercent);
            });

            Global.MaxHP.Register(maxHp =>
            {
                PlayerPrefs.SetInt(nameof(MaxHP), maxHp);
            });

            // ������ʼ��
            IArchitecture _ = Interface;
        }

        /// <summary>
        /// ��������
        /// </summary>
        public static void ResetData()
        {
            // �������
            HP.Value = MaxHP.Value;
            Exp.Value = 0;
            Level.Value = 1;
            CurrentSeconds.Value = 0;
            EnemyGenerator.EnemyCount.Value = 0;

            // ��������
            // �򵥽�
            AbilityConfig simpleSwordConfig = Player.Default.SimpleSwordConfig;
            SimpleSwordUnlocked.Value = false;
            SimpleSwordDamage.Value = simpleSwordConfig.InitDamage;
            SimpleSwordDuration.Value = simpleSwordConfig.InitDuration;
            SimpleSwordRange.Value = simpleSwordConfig.InitRange;
            SimpleSwordCount.Value = simpleSwordConfig.InitCount;
            // ��ת��
            AbilityConfig rotateSwordConfig = Player.Default.RotateSwordConfig;
            RotateSwordUnlocked.Value = false;
            RotateSwordDamage.Value = rotateSwordConfig.InitDamage;
            RotateSwordSpeed.Value = rotateSwordConfig.InitSpeed;
            RotateSwordRange.Value = rotateSwordConfig.InitRange;
            RotateSwordCount.Value = rotateSwordConfig.InitCount;
            // �ɵ�
            AbilityConfig simpleKnifeConfig = Player.Default.SimpleKnifeConfig;
            SimpleKnifeUnlocked.Value = false;
            SimpleKnifeDamage.Value = simpleKnifeConfig.InitDamage;
            SimpleKnifeDuration.Value = simpleKnifeConfig.InitDuration;
            SimpleKnifeCount.Value = simpleKnifeConfig.InitCount;
            SimpleKnifeAttackCount.Value = simpleKnifeConfig.InitAttackCount;
            // ����
            AbilityConfig basketballConfig = Player.Default.BasketballConfig;
            BasketballUnlocked.Value = false;
            BasketballDamage.Value = basketballConfig.InitDamage;
            BasketballSpeed.Value = basketballConfig.InitSpeed;
            BasketballCount.Value = basketballConfig.InitCount;
            // ը��
            AbilityConfig simpleBombConfig = Player.Default.SimpleBombConfig;
            SimpleBombUnlocked.Value = false;
            SimpleBombDamage.Value = simpleBombConfig.InitDamage;
            SimpleBombPercent.Value = simpleBombConfig.InitPercent;

            Interface.GetSystem<ExpUpgradeSystem>().ResetData();
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
            if (percent <= ExpPercent.Value)
            {
                // ���侭��ֵ
                PowerUpManager.Default.Exp.Instantiate()
                    .Position(gameObject.Position())
                    .Show();
            }

            percent = Random.Range(0, 1f);
            if (percent <= CoinPercent.Value)
            {
                // ������
                PowerUpManager.Default.Coin.Instantiate()
                    .Position(gameObject.Position())
                    .Show();

                return;
            }

            percent = Random.Range(0, 1f);
            if (percent <= HPPercent.Value)
            {
                // ��������ֵ
                PowerUpManager.Default.HP.Instantiate()
                    .Position(gameObject.Position())
                    .Show();

                return;
            }

            // ���ը���ѽ������ҳ�����û������ը��
            if (SimpleBombUnlocked.Value && !Object.FindObjectOfType<Bomb>())
            {
                percent = Random.Range(0, 1f);
                if (percent <= SimpleBombPercent.Value)
                {
                    // ����ը��
                    PowerUpManager.Default.Bomb.Instantiate()
                        .Position(gameObject.Position())
                        .Show();

                    return;
                }
            }

            percent = Random.Range(0, 1f);
            if (percent <= GetAllExpPercent.Value)
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
