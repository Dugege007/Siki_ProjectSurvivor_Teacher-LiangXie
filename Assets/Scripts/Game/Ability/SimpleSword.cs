using UnityEngine;
using QFramework;
using System.Linq;

namespace ProjectSurvivor
{
    public partial class SimpleSword : ViewController
    {
        private float mCurrentSeconds = 0;

        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;

            if (mCurrentSeconds >= Global.SimpleSwordDuration.Value)
            {
                mCurrentSeconds = 0;

                Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

                foreach (Enemy enemy in enemies
                    .OrderBy(e => e.Direction2DFrom(Player.Default).magnitude)
                    .Where(e => Vector2.Distance(Player.Default.Position(), e.transform.position) <= Global.SimpleSwordRange.Value)
                    .Take(Global.SimpleSwordCount.Value + Global.AdditionalFlyThingCount.Value))
                // OrderBy() 从小到大排序
                // Direction2DFrom() 获取二维向量
                // Where() 筛选
                // Vector2.Distance() 同 Direction2DFrom().magnitude 用来计算距离；据说 Vector2.Distance() 比 .magnitude 更快
                // Take() 取前几个
                {
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
                                        IEnemy e = hurtBox.Owner.GetComponent<IEnemy>();
                                        DamageSystem.CalculateDamage(Global.SimpleSwordDamage.Value, e);
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
                                    p.Lerp(0, 10, 0.2f, z => selfCache.LocalEulerAnglesZ(z));

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
                                    p.Lerp(10, -180, 0.2f, z => selfCache.LocalEulerAnglesZ(z));

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
                                    selfCache.LocalEulerAnglesZ(z);
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
