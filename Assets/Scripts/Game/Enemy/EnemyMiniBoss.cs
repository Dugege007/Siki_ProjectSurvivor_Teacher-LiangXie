using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class EnemyMiniBoss : ViewController, IEnemy
    {
        public enum States
        {
            FollowingPlayer,    // �������
            Warning,            // ����
            Dash,               // �������
            Wait                // �ȴ�
        }

        // QF �ṩ��״̬���Ĳ�����ʽ
        public FSM<States> FSM = new FSM<States>();
        public float HP = 30;
        public float MovementSpeed = 1f;

        private bool mIgnoreHurt;

        private void Start()
        {
            EnemyGenerator.EnemyCount.Value++;

            // ʹ�� QF �ṩ��״̬���Ĳ�����ʽ
            // ����״̬
            FSM.State(States.FollowingPlayer)
                .OnFixedUpdate(() =>
                {
                    if (Player.Default)
                    {
                        // ���÷������
                        Vector3 direction = (Player.Default.transform.position - transform.position).normalized;
                        // �ƶ�
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
                }); // ֻд .OnFixedUpdate() ���������ã���Ҫ�� FixedUpdate() �д���

            // ����״̬
            FSM.State(States.Warning)
                .OnEnter(() =>
                {
                    // ֹͣ�ƶ�
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

            // ���״̬
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
            // ���� QF ״̬���е� OnUpdate
            FSM.Update();

            if (HP <= 0)
            {
                // �������
                Global.GeneratePowerUp(gameObject, true);
                // �����Լ�
                this.DestroyGameObjGracefully();
            }
        }

        private void FixedUpdate()
        {
            // ���� QF ״̬���е� OnFixedUpdate
            FSM.FixedUpdate();
        }

        public void Hurt(float hurtValue, bool force = false, bool critical = false)
        {
            if (mIgnoreHurt && !force) return;

            // �����˺�
            mIgnoreHurt = true;
            // ��Ϊ��ɫ
            Sprite.color = Color.red;
            // �˺�Ʈ��
            FloatingTextController.Play(transform.position + Vector3.up * 0.4f, hurtValue.ToString("0"), critical);
            // ������Ч
            AudioKit.PlaySound("Hit");

            // ��ʱִ��
            ActionKit.Delay(0.2f, () =>
            {
                // ��Ѫ
                HP -= hurtValue;
                // ��ذ�ɫ
                Sprite.color = Color.white;
                // �������ڼ䲻���ܵ��˺��������ͻ
                mIgnoreHurt = false;

            }).Start(this);   // ����ִ��
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
