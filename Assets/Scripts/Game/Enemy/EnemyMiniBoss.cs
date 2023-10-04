using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class EnemyMiniBoss : ViewController, IEnemy
    {
        public enum States
        {
            FollowingPlayer,    // 跟随玩家
            Warning,            // 警戒
            Dash,               // 冲向玩家
            Wait                // 等待
        }

        // QF 提供了状态机的操作方式
        public FSM<States> FSM = new FSM<States>();
        public float HP = 30;
        public float MovementSpeed = 1f;

        private bool mIgnoreHurt;

        private void Start()
        {
            EnemyGenerator.EnemyCount.Value++;

            // 使用 QF 提供的状态机的操作方式
            // 跟随状态
            FSM.State(States.FollowingPlayer)
                .OnFixedUpdate(() =>
                {
                    if (Player.Default)
                    {
                        // 设置方向朝玩家
                        Vector3 direction = (Player.Default.transform.position - transform.position).normalized;
                        // 移动
                        SelfRigidbody2D.velocity = direction * MovementSpeed;

                        if (Vector3.Distance(Player.Default.transform.position, transform.position) <= 15f)
                        {
                            FSM.ChangeState(States.Warning);
                        }
                    }
                    else
                    {
                        SelfRigidbody2D.velocity = Vector3.zero;
                    }
                }); // 只写 .OnFixedUpdate() 并不起作用，需要在 FixedUpdate() 中传入

            // 警戒状态
            FSM.State(States.Warning)
                .OnEnter(() =>
                {
                    // 停止移动
                    SelfRigidbody2D.velocity = Vector2.zero;
                })
                .OnUpdate(() =>
                {
                    // 21 ~ 3
                    long frames = 3 + (60 * 3 - FSM.FrameCountOfCurrentState) / 10;

                    if (FSM.FrameCountOfCurrentState/frames%2==0)
                        Sprite.color = Color.yellow;
                    else
                        Sprite.color = Color.white;

                    if (FSM.FrameCountOfCurrentState >= 60 * 3)
                        FSM.ChangeState(States.Dash);
                })
                .OnExit(() =>
                {
                    Sprite.color = Color.white;
                });

            Vector3 dashStartPos = Vector3.zero;
            float dashStartDistanceToPlayer = 0;

            // 冲刺状态
            FSM.State(States.Dash)
                .OnEnter(() =>
                {
                    Vector3 direction = (Player.Default.Position() - transform.Position()).normalized;
                    SelfRigidbody2D.velocity = direction * 10f;
                    dashStartPos = transform.Position();
                    dashStartDistanceToPlayer = Vector3.Distance(Player.Default.Position(), transform.Position());
                })
                .OnUpdate(() =>
                {
                    float distance = Vector3.Distance(transform.Position(), dashStartPos);

                    if (distance >= dashStartDistanceToPlayer * 1.8f)
                    {
                        FSM.ChangeState(States.FollowingPlayer);
                    }
                });

            FSM.StartState(States.FollowingPlayer);

            FSM.State(States.Wait)
                .OnEnter(() =>
                {
                    SelfRigidbody2D.velocity = Vector2.zero;
                })
                .OnUpdate(() =>
                {
                    if (FSM.FrameCountOfCurrentState >= 60)
                    {
                        FSM.ChangeState(States.FollowingPlayer);
                    }
                });
        }

        private void Update()
        {
            // 调用 QF 状态机中的 OnUpdate
            FSM.Update();

            if (HP <= 0)
            {
                // 掉落道具
                Global.GeneratePowerUp(gameObject, true);
                // 销毁自己
                this.DestroyGameObjGracefully();
            }
        }

        private void FixedUpdate()
        {
            // 调用 QF 状态机中的 OnFixedUpdate
            FSM.FixedUpdate();
        }

        public void Hurt(float hurtValue, bool force = false, bool critical = false)
        {
            if (mIgnoreHurt && !force) return;

            // 忽略伤害
            mIgnoreHurt = true;
            // 变为红色
            Sprite.color = Color.red;
            // 伤害飘字
            FloatingTextController.Play(transform.position + Vector3.up * 0.4f, hurtValue.ToString("0"), critical);
            // 播放音效
            AudioKit.PlaySound("Hit");

            // 延时执行
            ActionKit.Delay(0.2f, () =>
            {
                // 减血
                HP -= hurtValue;
                // 变回白色
                Sprite.color = Color.white;
                // 在受伤期间不再受到伤害，避免冲突
                mIgnoreHurt = false;

            }).Start(this);   // 自身执行
        }

        public void SetSpeedScale(float speedScale)
        {
            MovementSpeed *= speedScale;
        }

        public void SetHPScale(float hpScale)
        {
            HP *= hpScale;
        }

        private void OnDestroy()
        {
            EnemyGenerator.EnemyCount.Value--;
        }
    }
}
