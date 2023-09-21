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
    }
}
