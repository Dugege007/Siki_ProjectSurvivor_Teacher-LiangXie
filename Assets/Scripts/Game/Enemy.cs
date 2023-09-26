using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Enemy : ViewController
    {
        public float HP = 3;
        public float MovementSpeed = 2f;
        private bool mIgnoreHurt = false;

        private void Start()
        {
            EnemyGenerator.EnemyCount.Value++;
        }

        private void FixedUpdate()
        {
            if (Player.Default)
            {
                // ���÷������
                Vector3 direction = (Player.Default.transform.position - transform.position).normalized;

                // �ƶ�
                SelfRigidbody2D.velocity = direction * MovementSpeed;
            }
            else
            {
                SelfRigidbody2D.velocity = Vector3.zero;
            }
        }

        private void Update()
        {
            if (HP <= 0)
            {
                // �������
                Global.GeneratePowerUp(gameObject);
                // �����Լ�
                this.DestroyGameObjGracefully();
            }
        }

        /// <summary>
        /// �����ܵ��˺�
        /// </summary>
        /// <param name="value">�˺�ֵ</param>
        /// <param name="force">�Ƿ�ǿ��</param>
        public void Hurt(float value, bool force = false)
        {
            if (mIgnoreHurt && !force) return;

            // �����˺�
            mIgnoreHurt = true;
            // ��Ϊ��ɫ
            Sprite.color = Color.red;
            AudioKit.PlaySound("Hit");

            // ��ʱִ��
            ActionKit.Delay(0.2f, () =>
            {
                // ��Ѫ
                HP -= value;
                // ��ذ�ɫ
                Sprite.color = Color.white;
                // �������ڼ䲻���ܵ��˺��������ͻ
                mIgnoreHurt = false;

            }).Start(this);   // ����ִ��
        }

        private void OnDestroy()
        {
            EnemyGenerator.EnemyCount.Value--;
        }
    }
}
