using UnityEngine;
using QFramework;
using System.Linq;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class SimpleKnife : ViewController
    {
        private float mCurrentSeconds = 0;

        public BindableProperty<bool> SuperKnife = new(true);

        private void Start()
        {
            SuperKnife.RegisterWithInitValue(unlocked =>
            {
                if (unlocked)
                    this.LocalScale(2);
                else
                    this.LocalScale(1);

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;

            if (mCurrentSeconds >= Global.SimpleKnifeDuration.Value)
            {
                mCurrentSeconds = 0;

                if (Player.Default.IsDead == false)
                {
                    // 获取当前存在的敌人列表
                    var enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                        .OrderBy(e => Player.Default.Distance2D(e))
                        .Take(Global.SimpleKnifeCount.Value + Global.AdditionalFlyThingCount.Value);

                    int i = 0;
                    foreach (Enemy enemy in enemies)
                    {
                        if (i < 4)
                        {
                            ActionKit.DelayFrame(10 * i, () => AudioKit.PlaySound(Sfx.KNIFE)).StartGlobal();
                            i++;
                        }

                        if (enemy)
                        {
                            Knife.Instantiate()
                                .Position(this.Position())
                                .Show()
                                .Self(self =>
                                {
                                    CircleCollider2D selfCache = self;

                                    // 设置运动方向
                                    Vector2 direction = (enemy.Position() - Player.Default.Position()).normalized;
                                    self.transform.up = direction;
                                    Rigidbody2D rigidbody2D = self.GetComponent<Rigidbody2D>();
                                    rigidbody2D.velocity = direction * 10;

                                    int attackCount = 0;
                                    // 添加碰撞事件
                                    self.OnTriggerEnter2DEvent(collider =>
                                    {
                                        HurtBox hurtBox = collider.GetComponent<HurtBox>();
                                        if (hurtBox)
                                        {
                                            // 碰到敌人就对其造成伤害
                                            if (hurtBox.Owner.CompareTag("Enemy"))
                                            {
                                                int damageTimes = SuperKnife.Value ? Random.Range(1, 2) + 1 : 1;

                                                IEnemy e = hurtBox.Owner.GetComponent<IEnemy>();
                                                DamageSystem.CalculateDamage(Global.SimpleKnifeDamage.Value * damageTimes, e);
                                                attackCount++;

                                                int additionalAttackCount = SuperKnife.Value ? 3 : 0;

                                                if (attackCount >= Global.SimpleKnifeAttackCount.Value + additionalAttackCount)
                                                {
                                                    selfCache.DestroyGameObjGracefully();
                                                }
                                            }
                                        }

                                    }).UnRegisterWhenGameObjectDestroyed(self);

                                    // 注册距离过远销毁自身事件
                                    ActionKit.OnUpdate.Register(() =>
                                    {
                                        if (Player.Default)
                                        {
                                            if (Vector3.Distance(Player.Default.Position(), selfCache.Position()) > 20)
                                            {
                                                selfCache.DestroyGameObjGracefully();
                                            }
                                        }

                                    }).UnRegisterWhenGameObjectDestroyed(self);
                                });
                        }
                    }
                }
            }
        }
    }
}
