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

        /// <summary>
        /// 刀攻击力
        /// </summary>
        public static BindableProperty<float> SimpleSwordDamage = new(AbilityConfig.InitSimpleSwordDamage);
        /// <summary>
        /// 刀攻击间隔
        /// </summary>
        public static BindableProperty<float> SimpleSwordDuration = new(AbilityConfig.InitSimpleSwordDuration);
        /// <summary>
        /// 刀范围
        /// </summary>
        public static BindableProperty<float> SimpleSwordRange = new(AbilityConfig.InitSimpleSwordRange);
        /// <summary>
        /// 刀数量
        /// </summary>
        public static BindableProperty<int> SimpleSwordCount = new(AbilityConfig.InitSimpleSwordCount);

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
        /// 炸弹掉率
        /// </summary>
        public static BindableProperty<float> BombPercent = new(0.1f);
        /// <summary>
        /// 炸弹掉率
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
            UIKit.Root.SetResolution(1920, 1080, 0.5f);

            Global.MaxHP.Value = PlayerPrefs.GetInt(nameof(MaxHP), 3);
            HP.Value = MaxHP.Value;

            // 简单的存储功能
            // 读取金币数据
            Global.Coin.Value = PlayerPrefs.GetInt(nameof(Coin), 0);
            Global.CoinPercent.Value = PlayerPrefs.GetFloat(nameof(CoinPercent), 0.1f);
            Global.ExpPercent.Value = PlayerPrefs.GetFloat(nameof(ExpPercent), 0.4f);
            Global.HPPercent.Value = PlayerPrefs.GetFloat(nameof(HPPercent), 0.05f);
            Global.BombPercent.Value = PlayerPrefs.GetFloat(nameof(BombPercent), 0.1f);
            Global.GetAllExpPercent.Value = PlayerPrefs.GetFloat(nameof(GetAllExpPercent), 0.05f);

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
            SimpleSwordDamage.Value = AbilityConfig.InitSimpleSwordDamage;
            SimpleSwordDuration.Value = AbilityConfig.InitSimpleSwordDuration;
            SimpleSwordRange.Value = AbilityConfig.InitSimpleSwordRange;
            SimpleSwordCount.Value = AbilityConfig.InitSimpleSwordCount;

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

            percent = Random.Range(0, 1f);
            if (percent <= BombPercent.Value)
            {
                // 掉落炸弹
                PowerUpManager.Default.Bomb.Instantiate()
                    .Position(gameObject.Position())
                    .Show();

                return;
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
