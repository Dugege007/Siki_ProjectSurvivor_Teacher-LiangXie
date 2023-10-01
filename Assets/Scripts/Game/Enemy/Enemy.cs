using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Enemy : ViewController, IEnemy
    {
        public float HP = 3;
        public float MovementSpeed = 2f;
        private bool mIgnoreHurt = false;

        public Color DissolveColor = Color.yellow;

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
                FxController.Play(Sprite, DissolveColor);
                // �����Լ�
                this.DestroyGameObjGracefully();
            }
        }

        /// <summary>
        /// �����ܵ��˺�
        /// </summary>
        /// <param name="hurtValue">�˺�ֵ</param>
        /// <param name="force">�Ƿ�ǿ��</param>
        public void Hurt(float hurtValue, bool force = false)
        {
            if (mIgnoreHurt && !force) return;

            // �����˺�
            mIgnoreHurt = true;
            // ��Ϊ��ɫ
            Sprite.color = Color.red;
            // �˺�Ʈ��
            FloatingTextController.Play(transform.position + Vector3.up * 0.4f, hurtValue.ToString());
            // ������Ч
            AudioKit.PlaySound("Hit");

            // ��ʱִ��
            ActionKit.Delay(0.2f, () =>
            {
                // ��Ѫ
                HP -= hurtValue;
                // ��ذ�ɫ
                Sprite.color = Color.white;
                // �������ڼ䲻���ܵ��˺��������ͻ
                mIgnoreHurt = false;

            }).Start(this);   // ����ִ��
        }

        public void SetSpeedScale(float speedScale)
        {
            MovementSpeed *= speedScale;
        }

        public void SetHPScale(float hpScale)
        {
            HP *= hpScale;
        }

        private void OnDestroy()
        {
            EnemyGenerator.EnemyCount.Value--;
        }
    }
}
