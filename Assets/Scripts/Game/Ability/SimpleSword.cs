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
                                        p.Lerp(0, 10, 0.2f, z =>
                                        {
                                            selfCache.transform.localEulerAngles = selfCache.transform.localEulerAngles.Z(z);
                                        });

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
                                        p.Lerp(10, -180, 0.2f, z =>
                                        {
                                            selfCache.transform.localEulerAngles = selfCache.transform.localEulerAngles.Z(z);
                                        });

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
