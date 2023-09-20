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

                // 先执行一次 ResKit.Init() 以确保 ResKit 初始化才能使用 UIKit.OpenPanel<>()
                ResKit.Init();
                // 打开 游戏结束面板
                UIKit.OpenPanel<UIGameOverPanel>();

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
