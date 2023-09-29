using UnityEngine;
using QFramework;
using System.Linq;

namespace ProjectSurvivor
{
    public partial class SimpleKnife : ViewController
    {
        private float mCurrentSeconds = 0;

        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;

            if (mCurrentSeconds >= 1.0f)
            {
                mCurrentSeconds = 0;

                // 获取当前存在的敌人列表
                Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
                // 获取最近的敌人
                Enemy enemy = enemies.OrderBy(e => Vector3.Distance(Player.Default.transform.position, e.transform.position)).FirstOrDefault();

                if (enemy)
                {
                    Knife.Instantiate()
                        .Position(this.Position())
                        .Show()
                        .Self(self =>
                        {
                            // 设置运动方向
                            Rigidbody2D rigidbody2D = self.GetComponent<Rigidbody2D>();
                            Vector3 direction = (enemy.Position() - Player.Default.Position()).normalized;
                            rigidbody2D.velocity = direction * 10;

                            // 添加碰撞事件
                            self.OnTriggerEnter2DEvent(collider =>
                            {
                                HurtBox hurtBox = collider.GetComponent<HurtBox>();
                                if (hurtBox)
                                {
                                    // 碰到敌人就对其造成伤害
                                    if (hurtBox.Owner.CompareTag("Enemy"))
                                    {
                                        hurtBox.Owner.GetComponent<Enemy>().Hurt(5);
                                        self.DestroyGameObjGracefully();
                                    }
                                }

                            }).UnRegisterWhenGameObjectDestroyed(self);

                            // 注册距离过远销毁自身事件
                            ActionKit.OnUpdate.Register(() =>
                            {
                                if (Player.Default)
                                {
                                    if (Vector3.Distance(Player.Default.Position(), self.Position()) > 20)
                                    {
                                        self.DestroyGameObjGracefully();
                                    }
                                }

                            }).UnRegisterWhenGameObjectDestroyed(self);
                        });
                }
            }
        }
    }
}
