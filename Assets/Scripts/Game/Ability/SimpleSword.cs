using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class SimpleSword : ViewController
    {
        private float mCurrentSeconds = 0;

        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;

            if (mCurrentSeconds >= Global.SimpleAbilityDuration.Value)
            {
                mCurrentSeconds = 0;

                Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

                foreach (Enemy enemy in enemies)
                {
                    float distance = Vector2.Distance(Player.Default.transform.position, enemy.transform.position);

                    if (distance <= 5)
                    {
                        //enemy.Hurt(Global.SimpleAbilityDamage.Value);
                        Sword.Instantiate()
                            .Position(enemy.Position() + Vector3.left * 0.25f)
                            .Show()
                            .Self(self =>
                            {
                                BoxCollider2D selfCache = self;
                                selfCache.OnTriggerEnter2DEvent(collider2D =>
                                {
                                    HurtBox hurtBox = collider2D.GetComponent<HurtBox>();
                                    if (hurtBox != null)
                                    {
                                        if (hurtBox.Owner.CompareTag("Enemy"))
                                        {
                                            enemy.Hurt(Global.SimpleAbilityDamage.Value);
                                        }
                                    }

                                }).UnRegisterWhenGameObjectDestroyed(self);

                                // 劈砍动画
                                ActionKit.Sequence()    //开启一个队列
                                    .Callback(() =>
                                    {
                                        // 先取消碰撞
                                        selfCache.enabled = false;
                                    })
                                    .Parallel(p =>     // 开一个并行的任务
                                    {
                                        // 抬起
                                        p.Lerp(0, 10, 0.2f, z =>
                                        {
                                            selfCache.transform.localEulerAngles = selfCache.transform.localEulerAngles.Z(z);
                                        });

                                        p.Append(ActionKit.Sequence()
                                            // 放大
                                            .Lerp(0, 1.25f, 0.1f, scale => selfCache.LocalScale(scale))
                                            // 稍微缩小
                                            .Lerp(1.25f, 1f, 0.1f, scale => selfCache.LocalScale(scale))
                                        );
                                    })
                                    .Callback(() =>
                                    {
                                        // 打开碰撞
                                        selfCache.enabled = true;
                                    })
                                    .Parallel(p =>
                                    {
                                        // 向下砍
                                        p.Lerp(10, -180, 0.2f, z =>
                                        {
                                            selfCache.transform.localEulerAngles = selfCache.transform.localEulerAngles.Z(z);
                                        });

                                        p.Append(ActionKit.Sequence()
                                            // 稍微放大
                                            .Lerp(1, 1.25f, 0.1f, scale => selfCache.LocalScale(scale))
                                            // 稍微缩小
                                            .Lerp(1.25f, 1f, 0.1f, scale => selfCache.LocalScale(scale))
                                        );
                                    })
                                    .Callback(() =>
                                    { 
                                        // 关闭碰撞
                                        selfCache.enabled = false; 
                                    })
                                    .Lerp(-180, 0, 0.3f, z =>
                                    {
                                        selfCache.transform.localEulerAngles = selfCache.transform.localEulerAngles.Z(z);
                                        selfCache.LocalScale(z.Abs() / 180);
                                    })
                                    .Start(this, () =>
                                    {
                                        selfCache.DestroyGameObjGracefully();
                                    });
                            });
                    }
                }
            }
        }
    }
}
