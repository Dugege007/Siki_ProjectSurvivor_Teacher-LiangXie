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
    }
}
