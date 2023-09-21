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
                // �������
                Global.GeneratePowerUp(gameObject);
                // �����Լ�
                this.DestroyGameObjGracefully();
            }
        }

        public void Hurt(float value)
        {
            if (mIgnoreHurt) return;

            // �����˺�
            mIgnoreHurt = true;
            // ��Ϊ��ɫ
            Sprite.color = Color.red;

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
