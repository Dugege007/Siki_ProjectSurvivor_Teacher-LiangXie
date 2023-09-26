using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Player : ViewController
    {
        public static Player Default;

        public float MovementSpeed = 5f;

        private void Awake()
        {
            Default = this;
        }

        private void Start()
        {
            // Ϊ HurtBox ���һ���¼�
            HurtBox.OnTriggerEnter2DEvent(collider2D =>
            {
                HitBox hitBox = collider2D.GetComponent<HitBox>();
                if (hitBox != null)
                {
                    if (hitBox.Owner.CompareTag("Enemy"))
                    {
                        Global.HP.Value--;
                        if (Global.HP.Value<=0)
                        {
                            AudioKit.PlaySound("Die");
                            // �������
                            this.DestroyGameObjGracefully();

                            // �� ��Ϸ�������
                            UIKit.OpenPanel<UIGameOverPanel>();
                        }
                        else
                        {
                            AudioKit.PlaySound("Hurt");
                        }
                    }
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");  // Input.GetAxisRaw() �Ƚ���Ӳ�ı任
            float vertical = Input.GetAxisRaw("Vertical");
            Vector2 targetVelocity = new Vector2(horizontal, vertical).normalized * MovementSpeed;

            SelfRigidbody2D.velocity = Vector2.Lerp(SelfRigidbody2D.velocity, targetVelocity, 1 - Mathf.Exp(-Time.deltaTime * 5));
        }

        private void OnDestroy()
        {
            Default = null;
        }
    }
}
