using UnityEngine;
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
        /// ���
        /// </summary>
        public static BindableProperty<int> Coin = new BindableProperty<int>(0);

        /// <summary>
        /// �ȼ�
        /// </summary>
        public static BindableProperty<int> Level = new BindableProperty<int>(1);

        /// <summary>
        /// ��ǰʱ��
        /// </summary>
        public static BindableProperty<float> CurrentSeconds = new BindableProperty<float>(0);

        /// <summary>
        /// �������Ĺ�����
        /// </summary>
        public static BindableProperty<float> SimpleAbilityDamage = new BindableProperty<float>(1);

        /// <summary>
        /// �������Ĺ������
        /// </summary>
        public static BindableProperty<float> SimpleAbilityDuration = new BindableProperty<float>(1.5f);

        /// <summary>
        /// ��������
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
        /// ���ɵ�����Ʒ
        /// </summary>
        /// <param name="gameObject">������Ʒ������</param>
        public static void GeneratePowerUp(GameObject gameObject)
        {
            float random = Random.Range(0, 100f);
            if (random <= 90)
            {
                // 90% ���侭��ֵ
                PowerUpManager.Default.Exp.Instantiate()
                    .Position(gameObject.Position())
                    .Show();
            }
            else
            {
                // ������
                PowerUpManager.Default.Coin.Instantiate()
                    .Position(gameObject.Position())
                    .Show();
            }
        }
    }
}
