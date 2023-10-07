using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class SimpleAxe : ViewController
    {
        private float mCurrentSeconds = 0;

        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;

            if (mCurrentSeconds >= 1.0f)
            {
                Axe.Instantiate()
                    .Show()
                    .Position(this.Position())
                    .Self(self =>
                    {
                        // 斧头的初速度
                        Rigidbody2D rigidbody2D = self.GetComponent<Rigidbody2D>();
                        float randomX = RandomUtility.Choose(-5, -2, 2, 5);
                        float randomY = RandomUtility.Choose(2, 5);
                        rigidbody2D.velocity = new Vector2(randomX, randomY);

                        // 为斧头添加碰撞事件
                        self.OnTriggerEnter2DEvent(collider =>
                        {
                            HitHurtBox hurtBox = collider.GetComponent<HitHurtBox>();
                            if (hurtBox)
                            {
                                if (hurtBox.Owner.CompareTag("Enemy"))
                                {
                                    hurtBox.Owner.GetComponent<Enemy>().Hurt(2);
                                }
                            }

                        }).UnRegisterWhenGameObjectDestroyed(self);

                        // 斧头超过一定距离后销毁自身
                        ActionKit.OnUpdate.Register(() =>
                        {
                            if (Player.Default)
                            {
                                if (Player.Default.Position().y - self.PositionY() > Screen.height)
                                {
                                    self.DestroyGameObjGracefully();
                                }
                            }

                        }).UnRegisterWhenGameObjectDestroyed(self);
                    });

                mCurrentSeconds = 0;
            }
        }
    }
}
