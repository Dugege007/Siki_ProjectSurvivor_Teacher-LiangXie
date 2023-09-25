using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public class Global : Architecture<Global>
    {
        protected override void Init()
        {
            // 注册模块

        }

        #region Model
        /// <summary>
        /// 经验值
        /// </summary>
        public static BindableProperty<int> Exp = new BindableProperty<int>(0);

        /// <summary>
        /// 金币
        /// </summary>
        public static BindableProperty<int> Coin = new BindableProperty<int>(0);

        /// <summary>
        /// 等级
        /// </summary>
        public static BindableProperty<int> Level = new BindableProperty<int>(1);

        /// <summary>
        /// 当前时间
        /// </summary>
        public static BindableProperty<float> CurrentSeconds = new BindableProperty<float>(0);

        /// <summary>
        /// 简单能力的攻击力
        /// </summary>
        public static BindableProperty<float> SimpleAbilityDamage = new BindableProperty<float>(1);

        /// <summary>
        /// 简单能力的攻击间隔
        /// </summary>
        public static BindableProperty<float> SimpleAbilityDuration = new BindableProperty<float>(1.5f);

        /// <summary>
        /// 经验掉率
        /// </summary>
        public static BindableProperty<float> ExpPercent = new BindableProperty<float>(0.4f);

        /// <summary>
        /// 金币掉率
        /// </summary>
        public static BindableProperty<float> CoinPercent = new BindableProperty<float>(0.05f);
        #endregion

        /// <summary>
        /// 自动初始化
        /// </summary>
        /// <remark>该方法在运行时自动调用一次</remark>
        [RuntimeInitializeOnLoadMethod]
        public static void AutoInit()
        {
            UIKit.Root.SetResolution(1920, 1080, 0.5f);

            // 简单的存储功能
            // 读取金币数据
            Global.Coin.Value = PlayerPrefs.GetInt(nameof(Coin), 0);
            Global.CoinPercent.Value = PlayerPrefs.GetFloat(nameof(ExpPercent), 0.1f);
            Global.ExpPercent.Value = PlayerPrefs.GetFloat(nameof(ExpPercent), 0.4f);

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
        }

        /// <summary>
        /// 重置数据
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
        /// 生成掉落物品
        /// </summary>
        /// <param name="gameObject">掉落物品的主体</param>
        public static void GeneratePowerUp(GameObject gameObject)
        {
            float percent = Random.Range(0, 1f);
            if (percent <= ExpPercent)
            {
                // 90% 掉落经验值
                PowerUpManager.Default.Exp.Instantiate()
                    .Position(gameObject.Position())
                    .Show();
            }

            percent = Random.Range(0, 1f);
            if (percent <= CoinPercent)
            {
                // 掉落金币
                PowerUpManager.Default.Coin.Instantiate()
                    .Position(gameObject.Position())
                    .Show();
            }
        }
    }
}
