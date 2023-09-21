using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public class Global
    {
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
            float random = Random.Range(0, 100f);
            if (random <= 90)
            {
                // 90% 掉落经验值
                PowerUpManager.Default.Exp.Instantiate()
                    .Position(gameObject.Position())
                    .Show();
            }
            else
            {
                // 掉落金币
                PowerUpManager.Default.Coin.Instantiate()
                    .Position(gameObject.Position())
                    .Show();
            }
        }
    }
}
