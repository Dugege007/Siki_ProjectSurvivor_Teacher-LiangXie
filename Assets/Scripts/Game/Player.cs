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
            // 为 HurtBox 添加一个事件
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
                            // 销毁玩家
                            this.DestroyGameObjGracefully();

                            // 打开 游戏结束面板
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
            float horizontal = Input.GetAxisRaw("Horizontal");  // Input.GetAxisRaw() 比较生硬的变换
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
