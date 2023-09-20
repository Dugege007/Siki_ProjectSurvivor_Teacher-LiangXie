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
                // 销毁玩家
                this.DestroyGameObjGracefully();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 direction = new Vector2(horizontal, vertical).normalized;

            SelfRigidbody2D.velocity = direction * MovementSpeed;
        }

        private void OnDestroy()
        {
            Default = null;
        }
    }
}
