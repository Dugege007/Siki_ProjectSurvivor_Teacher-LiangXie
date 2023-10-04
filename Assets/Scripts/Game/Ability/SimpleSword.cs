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
                // OrderBy() ��С��������
                // Direction2DFrom() ��ȡ��ά����
                // Where() ɸѡ
                // Vector2.Distance() ͬ Direction2DFrom().magnitude ����������룻��˵ Vector2.Distance() �� .magnitude ����
                // Take() ȡǰ����
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

                            // ��������
                            ActionKit.Sequence()    //����һ������
                                .Callback(() =>
                                {
                                    // ��ȡ����ײ
                                    selfCache.enabled = false;
                                })
                                .Parallel(p =>     // ��һ�����е�����
                                {
                                    // ̧��
                                    p.Lerp(0, 10, 0.2f, z => selfCache.LocalEulerAnglesZ(z));

                                    p.Append(ActionKit.Sequence()
                                        // �Ŵ�
                                        .Lerp(0, 1.25f, 0.1f, scale => selfCache.LocalScale(scale))
                                        // ��΢��С
                                        .Lerp(1.25f, 1f, 0.1f, scale => selfCache.LocalScale(scale))
                                    );
                                })
                                .Callback(() =>
                                {
                                    // ����ײ
                                    selfCache.enabled = true;
                                })
                                .Parallel(p =>
                                {
                                    // ���¿�
                                    p.Lerp(10, -180, 0.2f, z => selfCache.LocalEulerAnglesZ(z));

                                    p.Append(ActionKit.Sequence()
                                        // ��΢�Ŵ�
                                        .Lerp(1, 1.25f, 0.1f, scale => selfCache.LocalScale(scale))
                                        // ��΢��С
                                        .Lerp(1.25f, 1f, 0.1f, scale => selfCache.LocalScale(scale))
                                    );
                                })
                                .Callback(() =>
                                {
                                    // �ر���ײ
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
