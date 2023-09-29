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
                    if (FSM.FrameCountOfCurrentState >= 60 * 2)
                    {
                        FSM.ChangeState(States.Dash);
                    }
                });

            Vector3 dashStartPos = Vector3.zero;
            float dashStartDistanceToPlayer = 0;

            // ���״̬
            FSM.State(States.Dash)
                .OnEnter(() =>
                {
                    Vector3 direction = (Player.Default.Position() - transform.Position()).normalized;
                    SelfRigidbody2D.velocity = direction * 20f;
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
        }

        private void Update()
        {
            // ���� QF ״̬���е� OnUpdate
            FSM.Update();

            if (HP <= 0)
            {
                // �������
                Global.GeneratePowerUp(gameObject);
                // �����Լ�
                this.DestroyGameObjGracefully();
            }
        }

        private void FixedUpdate()
        {
            // ���� QF ״̬���е� OnFixedUpdate
            FSM.FixedUpdate();
        }

        public void Hurt(float hurtValue, bool force = false)
        {
            if (mIgnoreHurt && !force) return;

            // �����˺�
            mIgnoreHurt = true;
            // ��Ϊ��ɫ
            Sprite.color = Color.red;
            // �˺�Ʈ��
            FloatingTextController.Play(transform.position + Vector3.up * 0.4f, hurtValue.ToString());
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

        private void OnDestroy()
        {
            EnemyGenerator.EnemyCount.Value--;
        }
    }
}
