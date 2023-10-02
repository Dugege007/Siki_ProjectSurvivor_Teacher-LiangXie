using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public class Global : Architecture<Global>
    {
        protected override void Init()
        {
            // 注册模块
            this.RegisterSystem(new SaveSystem());
            this.RegisterSystem(new CoinUpgradeSystem());
            this.RegisterSystem(new ExpUpgradeSystem());
        }

        #region Model
        /// <summary>
        /// 玩家生命值
        /// </summary>
        public static BindableProperty<int> HP = new(3);
        /// <summary>
        /// 玩家最大生命值
        /// </summary>
        public static BindableProperty<int> MaxHP = new(3);
        /// <summary>
        /// 经验值
        /// </summary>
        public static BindableProperty<int> Exp = new(0);
        /// <summary>
        /// 金币
        /// </summary>
        public static BindableProperty<int> Coin = new(0);
        /// <summary>
        /// 等级
        /// </summary>
        public static BindableProperty<int> Level = new(1);
        /// <summary>
        /// 当前时间
        /// </summary>
        public static BindableProperty<float> CurrentSeconds = new(0);

        // 简单剑
        public static BindableProperty<bool> SimpleSwordUnlocked = new(false);
        public static BindableProperty<float> SimpleSwordDamage = new(Player.Default.SimpleSwordConfig.InitDamage);
        public static BindableProperty<float> SimpleSwordDuration = new(Player.Default.SimpleSwordConfig.InitDuration);
        public static BindableProperty<float> SimpleSwordRange = new(Player.Default.SimpleSwordConfig.InitRange);
        public static BindableProperty<int> SimpleSwordCount = new(Player.Default.SimpleSwordConfig.InitCount);

        // 旋转剑
        public static BindableProperty<bool> RotateSwordUnlocked = new(false);
        public static BindableProperty<float> RotateSwordDamage = new(Player.Default.RotateSwordConfig.InitDamage);
        public static BindableProperty<float> RotateSwordSpeed = new(Player.Default.RotateSwordConfig.InitSpeed);
        public static BindableProperty<float> RotateSwordRange = new(Player.Default.RotateSwordConfig.InitRange);
        public static BindableProperty<int> RotateSwordCount = new(Player.Default.RotateSwordConfig.InitCount);

        // 飞刀
        public static BindableProperty<bool> SimpleKnifeUnlocked = new(false);
        public static BindableProperty<float> SimpleKnifeDamage = new(Player.Default.SimpleKnifeConfig.InitDamage);
        public static BindableProperty<float> SimpleKnifeDuration = new(Player.Default.SimpleKnifeConfig.InitDuration);
        public static BindableProperty<int> SimpleKnifeCount = new(Player.Default.SimpleKnifeConfig.InitCount);
        public static BindableProperty<int> SimpleKnifeAttackCount = new(Player.Default.SimpleKnifeConfig.InitAttackCount);

        // 篮球
        public static BindableProperty<bool> BasketballUnlocked = new(false);
        public static BindableProperty<float> BasketballDamage = new(Player.Default.BasketballConfig.InitDamage);
        public static BindableProperty<float> BasketballSpeed = new(Player.Default.BasketballConfig.InitSpeed);
        public static BindableProperty<int> BasketballCount = new(Player.Default.BasketballConfig.InitCount);

        // 炸弹
        public static BindableProperty<bool> SimpleBombUnlocked = new(false);
        public static BindableProperty<float> SimpleBombDamage = new(Player.Default.SimpleBombConfig.InitDamage);
        public static BindableProperty<float> SimpleBombPercent = new(Player.Default.SimpleBombConfig.InitPercent);

        /// <summary>
        /// 经验掉率
        /// </summary>
        public static BindableProperty<float> ExpPercent = new(0.4f);
        /// <summary>
        /// 金币掉率
        /// </summary>
        public static BindableProperty<float> CoinPercent = new(0.1f);
        /// <summary>
        /// 生命值掉率
        /// </summary>
        public static BindableProperty<float> HPPercent = new(0.05f);
        /// <summary>
        /// 吸收经验掉率
        /// </summary>
        public static BindableProperty<float> GetAllExpPercent = new(0.1f);
        #endregion

        /// <summary>
        /// 自动初始化
        /// </summary>
        /// <remark>该方法在运行时自动调用一次</remark>
        [RuntimeInitializeOnLoadMethod]
        public static void AutoInit()
        {
            // 设置 UI
            UIKit.Root.SetResolution(1920, 1080, 0.5f);



            // 简单的存储功能
            // 读取数据
            Global.Coin.Value = PlayerPrefs.GetInt(nameof(Coin), 0);
            Global.CoinPercent.Value = PlayerPrefs.GetFloat(nameof(CoinPercent), 0.1f);
            Global.ExpPercent.Value = PlayerPrefs.GetFloat(nameof(ExpPercent), 0.4f);
            Global.HPPercent.Value = PlayerPrefs.GetFloat(nameof(HPPercent), 0.05f);
            Global.SimpleBombPercent.Value = PlayerPrefs.GetFloat(nameof(SimpleBombPercent), 0.1f);
            Global.GetAllExpPercent.Value = PlayerPrefs.GetFloat(nameof(GetAllExpPercent), 0.05f);

            Global.MaxHP.Value = PlayerPrefs.GetInt(nameof(MaxHP), 3);
            HP.Value = MaxHP.Value;

            // 保存金币数据
            Global.Coin.Register(coin =>
            {
                PlayerPrefs.SetInt(nameof(Coin), coin);

            }); // 暂时不考虑注销

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

            // 主动初始化
            IArchitecture _ = Interface;
        }

        /// <summary>
        /// 重置数据
        /// </summary>
        public static void ResetData()
        {
            // 玩家数据
            HP.Value = MaxHP.Value;
            Exp.Value = 0;
            Level.Value = 1;
            CurrentSeconds.Value = 0;
            EnemyGenerator.EnemyCount.Value = 0;

            // 能力数据
            // 简单剑
            AbilityConfig simpleSwordConfig = Player.Default.SimpleSwordConfig;
            SimpleSwordUnlocked.Value = false;
            SimpleSwordDamage.Value = simpleSwordConfig.InitDamage;
            SimpleSwordDuration.Value = simpleSwordConfig.InitDuration;
            SimpleSwordRange.Value = simpleSwordConfig.InitRange;
            SimpleSwordCount.Value = simpleSwordConfig.InitCount;
            // 旋转剑
            AbilityConfig rotateSwordConfig = Player.Default.RotateSwordConfig;
            RotateSwordUnlocked.Value = false;
            RotateSwordDamage.Value = rotateSwordConfig.InitDamage;
            RotateSwordSpeed.Value = rotateSwordConfig.InitSpeed;
            RotateSwordRange.Value = rotateSwordConfig.InitRange;
            RotateSwordCount.Value = rotateSwordConfig.InitCount;
            // 飞刀
            AbilityConfig simpleKnifeConfig = Player.Default.SimpleKnifeConfig;
            SimpleKnifeUnlocked.Value = false;
            SimpleKnifeDamage.Value = simpleKnifeConfig.InitDamage;
            SimpleKnifeDuration.Value = simpleKnifeConfig.InitDuration;
            SimpleKnifeCount.Value = simpleKnifeConfig.InitCount;
            SimpleKnifeAttackCount.Value = simpleKnifeConfig.InitAttackCount;
            // 篮球
            AbilityConfig basketballConfig = Player.Default.BasketballConfig;
            BasketballUnlocked.Value = false;
            BasketballDamage.Value = basketballConfig.InitDamage;
            BasketballSpeed.Value = basketballConfig.InitSpeed;
            BasketballCount.Value = basketballConfig.InitCount;
            // 炸弹
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
        /// 生成掉落物品
        /// </summary>
        /// <param name="gameObject">掉落物品的主体</param>
        public static void GeneratePowerUp(GameObject gameObject)
        {
            float percent = Random.Range(0, 1f);
            if (percent <= ExpPercent.Value)
            {
                // 掉落经验值
                PowerUpManager.Default.Exp.Instantiate()
                    .Position(gameObject.Position())
                    .Show();
            }

            percent = Random.Range(0, 1f);
            if (percent <= CoinPercent.Value)
            {
                // 掉落金币
                PowerUpManager.Default.Coin.Instantiate()
                    .Position(gameObject.Position())
                    .Show();

                return;
            }

            percent = Random.Range(0, 1f);
            if (percent <= HPPercent.Value)
            {
                // 掉落生命值
                PowerUpManager.Default.HP.Instantiate()
                    .Position(gameObject.Position())
                    .Show();

                return;
            }

            // 如果炸弹已解锁，且场景中没有其他炸弹
            if (SimpleBombUnlocked.Value && !Object.FindObjectOfType<Bomb>())
            {
                percent = Random.Range(0, 1f);
                if (percent <= SimpleBombPercent.Value)
                {
                    // 掉落炸弹
                    PowerUpManager.Default.Bomb.Instantiate()
                        .Position(gameObject.Position())
                        .Show();

                    return;
                }
            }

            percent = Random.Range(0, 1f);
            if (percent <= GetAllExpPercent.Value)
            {
                // 掉落吸收经验
                PowerUpManager.Default.GetAllExp.Instantiate()
                    .Position(gameObject.Position())
                    .Show();

                return;
            }
        }
    }
}
