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
            this.RegisterSystem(new AchievementSystem());
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
        public static BindableProperty<float> Exp = new(0);
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
        public static BindableProperty<float> SimpleSwordDamage = new(ConfigManager.Instance.SimpleSwordConfig.InitDamage);
        public static BindableProperty<float> SimpleSwordDuration = new(ConfigManager.Instance.SimpleSwordConfig.InitDuration);
        public static BindableProperty<float> SimpleSwordRange = new(ConfigManager.Instance.SimpleSwordConfig.InitRange);
        public static BindableProperty<int> SimpleSwordCount = new(ConfigManager.Instance.SimpleSwordConfig.InitCount);

        // ��ת��
        public static BindableProperty<bool> RotateSwordUnlocked = new(false);
        public static BindableProperty<float> RotateSwordDamage = new(ConfigManager.Instance.RotateSwordConfig.InitDamage);
        public static BindableProperty<float> RotateSwordSpeed = new(ConfigManager.Instance.RotateSwordConfig.InitSpeed);
        public static BindableProperty<float> RotateSwordRange = new(ConfigManager.Instance.RotateSwordConfig.InitRange);
        public static BindableProperty<int> RotateSwordCount = new(ConfigManager.Instance.RotateSwordConfig.InitCount);

        // �ɵ�
        public static BindableProperty<bool> SimpleKnifeUnlocked = new(false);
        public static BindableProperty<float> SimpleKnifeDamage = new(ConfigManager.Instance.SimpleKnifeConfig.InitDamage);
        public static BindableProperty<float> SimpleKnifeDuration = new(ConfigManager.Instance.SimpleKnifeConfig.InitDuration);
        public static BindableProperty<int> SimpleKnifeCount = new(ConfigManager.Instance.SimpleKnifeConfig.InitCount);
        public static BindableProperty<int> SimpleKnifeAttackCount = new(ConfigManager.Instance.SimpleKnifeConfig.InitAttackCount);

        // ����
        public static BindableProperty<bool> BasketballUnlocked = new(false);
        public static BindableProperty<float> BasketballDamage = new(ConfigManager.Instance.BasketballConfig.InitDamage);
        public static BindableProperty<float> BasketballSpeed = new(ConfigManager.Instance.BasketballConfig.InitSpeed);
        public static BindableProperty<int> BasketballCount = new(ConfigManager.Instance.BasketballConfig.InitCount);

        // ը��
        public static BindableProperty<bool> SimpleBombUnlocked = new(false);
        public static BindableProperty<float> SimpleBombDamage = new(ConfigManager.Instance.SimpleBombConfig.InitDamage);
        public static BindableProperty<float> SimpleBombChance = new(ConfigManager.Instance.SimpleBombConfig.InitChance);

        // ������
        public static BindableProperty<float> CriticalChance = new(ConfigManager.Instance.CriticalChanceConfig.InitChance);
        // �����˺�ֵ
        public static BindableProperty<float> AdditionalDamage = new(ConfigManager.Instance.AdditionalDamageConfig.InitRate);
        // �����ƶ��ٶ�
        public static BindableProperty<float> AdditionalMovementSpeed = new(ConfigManager.Instance.AdditionalMovementSpeedConfig.InitRate);
        // ���Ӿ���ֵ
        public static BindableProperty<float> AdditionalExpRate = new(ConfigManager.Instance.AdditionalExpRateConfig.InitRate);
        // ���ӷ�����
        public static BindableProperty<int> AdditionalFlyThingCount = new(ConfigManager.Instance.AdditionalFlyThingCountConfig.InitCount);
        // ʰȡ��Χ
        public static BindableProperty<float> CollectableAreaRange = new(ConfigManager.Instance.CollectableAreaRangeConfig.InitRange);

        // �������� ����
        public static BindableProperty<bool> SuperKnife = new(false);
        public static BindableProperty<bool> SuperSimpleSword = new(false);
        public static BindableProperty<bool> SuperRotateSword = new(false);
        public static BindableProperty<bool> SuperBasketball = new(false);
        public static BindableProperty<bool> SuperBomb = new(false);

        //public static BindableProperty<float> PowerUpPercent = new(0.5f);
        // �������
        public static BindableProperty<float> ExpPercent = new(0.4f);
        // ��ҵ���
        public static BindableProperty<float> CoinPercent = new(0.05f);
        // ����ֵ����
        public static BindableProperty<float> HPPercent = new(0.02f);
        // ���վ������
        public static BindableProperty<float> GetAllExpPercent = new(0.02f);
        // �������
        public static BindableProperty<float> TreasureChestPercent = new(0.001f);
        #endregion

        /// <summary>
        /// �Զ���ʼ��
        /// </summary>
        /// <remark>�÷���������ʱ�Զ�����һ��</remark>
        [RuntimeInitializeOnLoadMethod]
        public static void AutoInit()
        {
            // ������Ƶ����ģʽΪ����ͬ��Ƶ�� 10 ֡�ڲ��ظ�����
            AudioKit.PlaySoundMode = AudioKit.PlaySoundModes.IgnoreSameSoundInGlobalFrames;
            // ��ʼʱ����һ����Դ
            ResKit.Init();
            // ���� UI
            UIKit.Root.SetResolution(1920, 1080, 0.5f);

            // �򵥵Ĵ洢����
            // ��ȡ����
            Global.Coin.Value = PlayerPrefs.GetInt(nameof(Coin), 0);
            Global.CoinPercent.Value = PlayerPrefs.GetFloat(nameof(CoinPercent), 0.05f);
            Global.ExpPercent.Value = PlayerPrefs.GetFloat(nameof(ExpPercent), 0.4f);
            Global.HPPercent.Value = PlayerPrefs.GetFloat(nameof(HPPercent), 0.02f);
            Global.GetAllExpPercent.Value = PlayerPrefs.GetFloat(nameof(GetAllExpPercent), 0.02f);
            Global.TreasureChestPercent.Value = PlayerPrefs.GetFloat(nameof(TreasureChestPercent), 0.02f);

            Global.MaxHP.Value = PlayerPrefs.GetInt(nameof(MaxHP), 3);
            HP.Value = MaxHP.Value;

            // ����������
            Global.Coin.Register(coin =>
            {
                PlayerPrefs.SetInt(nameof(Coin), coin);

            }); // ��ʱ������ע��

            Global.CoinPercent.Register(percent =>
            {
                PlayerPrefs.SetFloat(nameof(CoinPercent), percent);
            });

            Global.ExpPercent.Register(percent =>
            {
                PlayerPrefs.SetFloat(nameof(ExpPercent), percent);
            });

            Global.HPPercent.Register(percent =>
            {
                PlayerPrefs.SetFloat(nameof(HPPercent), percent);
            });

            Global.GetAllExpPercent.Register(percent =>
            {
                PlayerPrefs.SetFloat(nameof(GetAllExpPercent), percent);
            });

            Global.TreasureChestPercent.Register(percent =>
            {
                PlayerPrefs.SetFloat(nameof(TreasureChestPercent), percent);
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
            AbilityConfig abilityConfig = ConfigManager.Instance.SimpleSwordConfig;
            SimpleSwordUnlocked.Value = false;
            SimpleSwordDamage.Value = abilityConfig.InitDamage;
            SimpleSwordDuration.Value = abilityConfig.InitDuration;
            SimpleSwordRange.Value = abilityConfig.InitRange;
            SimpleSwordCount.Value = abilityConfig.InitCount;
            // ��ת��
            abilityConfig = ConfigManager.Instance.RotateSwordConfig;
            RotateSwordUnlocked.Value = false;
            RotateSwordDamage.Value = abilityConfig.InitDamage;
            RotateSwordSpeed.Value = abilityConfig.InitSpeed;
            RotateSwordRange.Value = abilityConfig.InitRange;
            RotateSwordCount.Value = abilityConfig.InitCount;
            // �ɵ�
            abilityConfig = ConfigManager.Instance.SimpleKnifeConfig;
            SimpleKnifeUnlocked.Value = false;
            SimpleKnifeDamage.Value = abilityConfig.InitDamage;
            SimpleKnifeDuration.Value = abilityConfig.InitDuration;
            SimpleKnifeCount.Value = abilityConfig.InitCount;
            SimpleKnifeAttackCount.Value = abilityConfig.InitAttackCount;
            // ����
            abilityConfig = ConfigManager.Instance.BasketballConfig;
            BasketballUnlocked.Value = false;
            BasketballDamage.Value = abilityConfig.InitDamage;
            BasketballSpeed.Value = abilityConfig.InitSpeed;
            BasketballCount.Value = abilityConfig.InitCount;
            // ը��
            abilityConfig = ConfigManager.Instance.SimpleBombConfig;
            SimpleBombUnlocked.Value = false;
            SimpleBombDamage.Value = abilityConfig.InitDamage;
            SimpleBombChance.Value = abilityConfig.InitRate;
            // ������
            abilityConfig = ConfigManager.Instance.CriticalChanceConfig;
            CriticalChance.Value = abilityConfig.InitRate;
            // �����˺�ֵ
            abilityConfig = ConfigManager.Instance.AdditionalDamageConfig;
            AdditionalDamage.Value = abilityConfig.InitChance;
            // �����ƶ��ٶ�
            abilityConfig = ConfigManager.Instance.AdditionalMovementSpeedConfig;
            AdditionalMovementSpeed.Value = abilityConfig.InitChance;
            // ���Ӿ���ֵ
            abilityConfig = ConfigManager.Instance.AdditionalExpRateConfig;
            AdditionalExpRate.Value = abilityConfig.InitChance;
            // ���ӷ�����
            abilityConfig = ConfigManager.Instance.AdditionalFlyThingCountConfig;
            AdditionalFlyThingCount.Value = abilityConfig.InitCount;
            // ʰȡ��Χ
            abilityConfig = ConfigManager.Instance.CollectableAreaRangeConfig;
            CollectableAreaRange.Value = abilityConfig.InitRange;

            // �������� ��
            SuperSimpleSword.Value = false;
            SuperRotateSword.Value = false;
            SuperKnife.Value = false;
            SuperBasketball.Value = false;
            SuperBomb.Value = false;

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
        public static void GeneratePowerUp(GameObject gameObject, bool isTreasureEnemy)
        {
            float percent = Random.Range(0, 1f);
            if (isTreasureEnemy)
            {
                if (percent <= 0.5f)
                {
                    // ���䱦��
                    PowerUpManager.Default.TreasureChest
                        .Instantiate()
                        .Position(gameObject.Position())
                        .Show();

                    return;
                }
            }

            percent = Random.Range(0, 1f);
            if (percent <= ExpPercent.Value)
            {
                // ���侭��ֵ
                PowerUpManager.Default.Exp.Instantiate()
                    .Position(gameObject.Position())
                    .Show();

                return;
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
                if (percent <= SimpleBombChance.Value)
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

            percent = Random.Range(0, 1f);
            if (percent < TreasureChestPercent.Value && Level.Value > 9)
            {
                // ���䱦��
                PowerUpManager.Default.TreasureChest.Instantiate()
                    .Position(gameObject.Position())
                    .Show();

                return;
            }
        }
    }
}
