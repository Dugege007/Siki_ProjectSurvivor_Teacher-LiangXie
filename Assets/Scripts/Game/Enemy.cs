using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Enemy : ViewController
    {
        public float HP = 3;

        public float MovementSpeed = 2f;

        private void Update()
        {
            if (Player.Default)
            {
                // ���÷������
                Vector3 direction = (Player.Default.transform.position - transform.position).normalized;

                // �ƶ�
                transform.Translate(direction * MovementSpeed * Time.deltaTime);
            }

            if (HP <= 0)
            {
                // �����Լ�
                this.DestroyGameObjGracefully();

                UIKit.OpenPanel<UIGamePassPanel>();
            }
        }
    }
}
