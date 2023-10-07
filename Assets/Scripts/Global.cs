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
            this.RegisterSystem(new AchievementSystem());
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
        public static BindableProperty<float> Exp = new(0);
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
        public static BindableProperty<float> SimpleSwordDamage = new(ConfigManager.Instance.SimpleSwordConfig.InitDamage);
        public static BindableProperty<float> SimpleSwordDuration = new(ConfigManager.Instance.SimpleSwordConfig.InitDuration);
        public static BindableProperty<float> SimpleSwordRange = new(ConfigManager.Instance.SimpleSwordConfig.InitRange);
        public static BindableProperty<int> SimpleSwordCount = new(ConfigManager.Instance.SimpleSwordConfig.InitCount);

        // 旋转剑
        public static BindableProperty<bool> RotateSwordUnlocked = new(false);
        public static BindableProperty<float> RotateSwordDamage = new(ConfigManager.Instance.RotateSwordConfig.InitDamage);
        public static BindableProperty<float> RotateSwordSpeed = new(ConfigManager.Instance.RotateSwordConfig.InitSpeed);
        public static BindableProperty<float> RotateSwordRange = new(ConfigManager.Instance.RotateSwordConfig.InitRange);
        public static BindableProperty<int> RotateSwordCount = new(ConfigManager.Instance.RotateSwordConfig.InitCount);

        // 飞刀
        public static BindableProperty<bool> SimpleKnifeUnlocked = new(false);
        public static BindableProperty<float> SimpleKnifeDamage = new(ConfigManager.Instance.SimpleKnifeConfig.InitDamage);
        public static BindableProperty<float> SimpleKnifeDuration = new(ConfigManager.Instance.SimpleKnifeConfig.InitDuration);
        public static BindableProperty<int> SimpleKnifeCount = new(ConfigManager.Instance.SimpleKnifeConfig.InitCount);
        public static BindableProperty<int> SimpleKnifeAttackCount = new(ConfigManager.Instance.SimpleKnifeConfig.InitAttackCount);

        // 篮球
        public static BindableProperty<bool> BasketballUnlocked = new(false);
        public static BindableProperty<float> BasketballDamage = new(ConfigManager.Instance.BasketballConfig.InitDamage);
        public static BindableProperty<float> BasketballSpeed = new(ConfigManager.Instance.BasketballConfig.InitSpeed);
        public static BindableProperty<int> BasketballCount = new(ConfigManager.Instance.BasketballConfig.InitCount);

        // 炸弹
        public static BindableProperty<bool> SimpleBombUnlocked = new(false);
        public static BindableProperty<float> SimpleBombDamage = new(ConfigManager.Instance.SimpleBombConfig.InitDamage);
        public static BindableProperty<float> SimpleBombChance = new(ConfigManager.Instance.SimpleBombConfig.InitChance);

        // 暴击率
        public static BindableProperty<float> CriticalChance = new(ConfigManager.Instance.CriticalChanceConfig.InitChance);
        // 附加伤害值
        public static BindableProperty<float> AdditionalDamage = new(ConfigManager.Instance.AdditionalDamageConfig.InitRate);
        // 附加移动速度
        public static BindableProperty<float> AdditionalMovementSpeed = new(ConfigManager.Instance.AdditionalMovementSpeedConfig.InitRate);
        // 附加经验值
        public static BindableProperty<float> AdditionalExpRate = new(ConfigManager.Instance.AdditionalExpRateConfig.InitRate);
        // 附加飞射物
        public static BindableProperty<int> AdditionalFlyThingCount = new(ConfigManager.Instance.AdditionalFlyThingCountConfig.InitCount);
        // 拾取范围
        public static BindableProperty<float> CollectableAreaRange = new(ConfigManager.Instance.CollectableAreaRangeConfig.InitRange);

        // 超级武器 开关
        public static BindableProperty<bool> SuperKnife = new(false);
        public static BindableProperty<bool> SuperSimpleSword = new(false);
        public static BindableProperty<bool> SuperRotateSword = new(false);
        public static BindableProperty<bool> SuperBasketball = new(false);
        public static BindableProperty<bool> SuperBomb = new(false);

        //public static BindableProperty<float> PowerUpPercent = new(0.5f);
        // 经验掉率
        public static BindableProperty<float> ExpPercent = new(0.4f);
        // 金币掉率
        public static BindableProperty<float> CoinPercent = new(0.05f);
        // 生命值掉率
        public static BindableProperty<float> HPPercent = new(0.02f);
        // 吸收经验掉率
        public static BindableProperty<float> GetAllExpPercent = new(0.02f);
        // 宝箱掉率
        public static BindableProperty<float> TreasureChestPercent = new(0.001f);
        #endregion

        /// <summary>
        /// 自动初始化
        /// </summary>
        /// <remark>该方法在运行时自动调用一次</remark>
        [RuntimeInitializeOnLoadMethod]
        public static void AutoInit()
        {
            // 设置音频播放模式为：相同音频在 10 帧内不重复播放
            AudioKit.PlaySoundMode = AudioKit.PlaySoundModes.IgnoreSameSoundInGlobalFrames;
            // 开始时加载一次资源
            ResKit.Init();
            // 设置 UI
            UIKit.Root.SetResolution(1920, 1080, 0.5f);

            // 简单的存储功能
            // 读取数据
            Global.Coin.Value = PlayerPrefs.GetInt(nameof(Coin), 0);
            Global.CoinPercent.Value = PlayerPrefs.GetFloat(nameof(CoinPercent), 0.05f);
            Global.ExpPercent.Value = PlayerPrefs.GetFloat(nameof(ExpPercent), 0.4f);
            Global.HPPercent.Value = PlayerPrefs.GetFloat(nameof(HPPercent), 0.02f);
            Global.GetAllExpPercent.Value = PlayerPrefs.GetFloat(nameof(GetAllExpPercent), 0.02f);
            Global.TreasureChestPercent.Value = PlayerPrefs.GetFloat(nameof(TreasureChestPercent), 0.02f);

            Global.MaxHP.Value = PlayerPrefs.GetInt(nameof(MaxHP), 3);
            HP.Value = MaxHP.Value;

            // 保存金币数据
            Global.Coin.Register(coin =>
            {
                PlayerPrefs.SetInt(nameof(Coin), coin);

            }); // 暂时不考虑注销

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
            AbilityConfig abilityConfig = ConfigManager.Instance.SimpleSwordConfig;
            SimpleSwordUnlocked.Value = false;
            SimpleSwordDamage.Value = abilityConfig.InitDamage;
            SimpleSwordDuration.Value = abilityConfig.InitDuration;
            SimpleSwordRange.Value = abilityConfig.InitRange;
            SimpleSwordCount.Value = abilityConfig.InitCount;
            // 旋转剑
            abilityConfig = ConfigManager.Instance.RotateSwordConfig;
            RotateSwordUnlocked.Value = false;
            RotateSwordDamage.Value = abilityConfig.InitDamage;
            RotateSwordSpeed.Value = abilityConfig.InitSpeed;
            RotateSwordRange.Value = abilityConfig.InitRange;
            RotateSwordCount.Value = abilityConfig.InitCount;
            // 飞刀
            abilityConfig = ConfigManager.Instance.SimpleKnifeConfig;
            SimpleKnifeUnlocked.Value = false;
            SimpleKnifeDamage.Value = abilityConfig.InitDamage;
            SimpleKnifeDuration.Value = abilityConfig.InitDuration;
            SimpleKnifeCount.Value = abilityConfig.InitCount;
            SimpleKnifeAttackCount.Value = abilityConfig.InitAttackCount;
            // 篮球
            abilityConfig = ConfigManager.Instance.BasketballConfig;
            BasketballUnlocked.Value = false;
            BasketballDamage.Value = abilityConfig.InitDamage;
            BasketballSpeed.Value = abilityConfig.InitSpeed;
            BasketballCount.Value = abilityConfig.InitCount;
            // 炸弹
            abilityConfig = ConfigManager.Instance.SimpleBombConfig;
            SimpleBombUnlocked.Value = false;
            SimpleBombDamage.Value = abilityConfig.InitDamage;
            SimpleBombChance.Value = abilityConfig.InitRate;
            // 暴击率
            abilityConfig = ConfigManager.Instance.CriticalChanceConfig;
            CriticalChance.Value = abilityConfig.InitRate;
            // 附加伤害值
            abilityConfig = ConfigManager.Instance.AdditionalDamageConfig;
            AdditionalDamage.Value = abilityConfig.InitChance;
            // 附加移动速度
            abilityConfig = ConfigManager.Instance.AdditionalMovementSpeedConfig;
            AdditionalMovementSpeed.Value = abilityConfig.InitChance;
            // 附加经验值
            abilityConfig = ConfigManager.Instance.AdditionalExpRateConfig;
            AdditionalExpRate.Value = abilityConfig.InitChance;
            // 附加飞射物
            abilityConfig = ConfigManager.Instance.AdditionalFlyThingCountConfig;
            AdditionalFlyThingCount.Value = abilityConfig.InitCount;
            // 拾取范围
            abilityConfig = ConfigManager.Instance.CollectableAreaRangeConfig;
            CollectableAreaRange.Value = abilityConfig.InitRange;

            // 超级武器 关
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
        /// 生成掉落物品
        /// </summary>
        /// <param name="gameObject">掉落物品的主体</param>
        public static void GeneratePowerUp(GameObject gameObject, bool isTreasureEnemy)
        {
            float percent = Random.Range(0, 1f);
            if (isTreasureEnemy)
            {
                if (percent <= 0.5f)
                {
                    // 掉落宝箱
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
                // 掉落经验值
                PowerUpManager.Default.Exp.Instantiate()
                    .Position(gameObject.Position())
                    .Show();

                return;
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
                if (percent <= SimpleBombChance.Value)
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

            percent = Random.Range(0, 1f);
            if (percent < TreasureChestPercent.Value && Level.Value > 9)
            {
                // 掉落宝箱
                PowerUpManager.Default.TreasureChest.Instantiate()
                    .Position(gameObject.Position())
                    .Show();

                return;
            }
        }
    }
}
