using QFramework;

namespace ProjectSurvivor
{
    public class Global
    {
        /// <summary>
        /// ����ֵ
        /// </summary>
        public static BindableProperty<int> Exp = new BindableProperty<int>(0);

        /// <summary>
        /// �ȼ�
        /// </summary>
        public static BindableProperty<int> Level = new BindableProperty<int>(1);

        public static BindableProperty<float> CurrentSeconds = new BindableProperty<float>(0);

        /// <summary>
        /// �������Ĺ�����
        /// </summary>
        public static BindableProperty<float> SimpleAbilityDamage = new BindableProperty<float>(1);
    }
}
