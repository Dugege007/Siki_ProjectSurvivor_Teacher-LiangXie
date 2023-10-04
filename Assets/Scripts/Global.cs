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
        public static BindableProperty<float> SimpleBombChance = new(Player.Default.SimpleBombConfig.InitChance);

        // ������
        public static BindableProperty<float> CriticalChance = new(Player.Default.CriticalChanceConfig.InitChance);
        // �����˺�ֵ
        public static BindableProperty<float> AdditionalDamage = new(Player.Default.AdditionalDamageConfig.InitRate);
        // �����ƶ��ٶ�
        public static BindableProperty<float> AdditionalMovementSpeed = new(Player.Default.AdditionalMovementSpeedConfig.InitRate);
        // ���Ӿ���ֵ
        public static BindableProperty<float> AdditionalExpRate = new(Player.Default.AdditionalExpRateConfig.InitRate);
        // ���ӷ�����
        public static BindableProperty<int> AdditionalFlyThingCount = new(Player.Default.AdditionalFlyThingCountConfig.InitCount);
        // ʰȡ��Χ
        public static BindableProperty<float> CollectableAreaRange = new(Player.Default.CollectableAreaRangeConfig.InitRange);

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
        public static BindableProperty<float> TreasureChestPercent = new(0.005f);
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
            AbilityConfig abilityConfig = Player.Default.SimpleSwordConfig;
            SimpleSwordUnlocked.Value = false;
            SimpleSwordDamage.Value = abilityConfig.InitDamage;
            SimpleSwordDuration.Value = abilityConfig.InitDuration;
            SimpleSwordRange.Value = abilityConfig.InitRange;
            SimpleSwordCount.Value = abilityConfig.InitCount;
            // ��ת��
            abilityConfig = Player.Default.RotateSwordConfig;
            RotateSwordUnlocked.Value = false;
            RotateSwordDamage.Value = abilityConfig.InitDamage;
            RotateSwordSpeed.Value = abilityConfig.InitSpeed;
            RotateSwordRange.Value = abilityConfig.InitRange;
            RotateSwordCount.Value = abilityConfig.InitCount;
            // �ɵ�
            abilityConfig = Player.Default.SimpleKnifeConfig;
            SimpleKnifeUnlocked.Value = false;
            SimpleKnifeDamage.Value = abilityConfig.InitDamage;
            SimpleKnifeDuration.Value = abilityConfig.InitDuration;
            SimpleKnifeCount.Value = abilityConfig.InitCount;
            SimpleKnifeAttackCount.Value = abilityConfig.InitAttackCount;
            // ����
            abilityConfig = Player.Default.BasketballConfig;
            BasketballUnlocked.Value = false;
            BasketballDamage.Value = abilityConfig.InitDamage;
            BasketballSpeed.Value = abilityConfig.InitSpeed;
            BasketballCount.Value = abilityConfig.InitCount;
            // ը��
            abilityConfig = Player.Default.SimpleBombConfig;
            SimpleBombUnlocked.Value = false;
            SimpleBombDamage.Value = abilityConfig.InitDamage;
            SimpleBombChance.Value = abilityConfig.InitRate;
            // ������
            abilityConfig = Player.Default.CriticalChanceConfig;
            CriticalChance.Value = abilityConfig.InitRate;
            // �����˺�ֵ
            abilityConfig = Player.Default.AdditionalDamageConfig;
            AdditionalDamage.Value = abilityConfig.InitChance;
            // �����ƶ��ٶ�
            abilityConfig = Player.Default.AdditionalMovementSpeedConfig;
            AdditionalMovementSpeed.Value = abilityConfig.InitChance;
            // ���Ӿ���ֵ
            abilityConfig = Player.Default.AdditionalExpRateConfig;
            AdditionalExpRate.Value = abilityConfig.InitChance;
            // ���ӷ�����
            abilityConfig = Player.Default.AdditionalFlyThingCountConfig;
            AdditionalFlyThingCount.Value = abilityConfig.InitCount;
            // ʰȡ��Χ
            abilityConfig = Player.Default.CollectableAreaRangeConfig;
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
            if (isTreasureEnemy)
            {
                // ���䱦��
                PowerUpManager.Default.TreasureChest.Instantiate()
                    .Position(gameObject.Position())
                    .Show();

                return;
            }

            float percent = Random.Range(0, 1f);
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
            if (percent <= TreasureChestPercent.Value)
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
