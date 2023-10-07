using UnityEngine;
using QFramework;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class Player : ViewController
    {
        public static Player Default;
        public float MovementSpeed = 5f;
        public Color DissolveColor = Color.red;
        //public bool IsDead = false;

        private bool mFaceRight;
        private AudioPlayer mWalkSfx;

        private void Awake()
        {
            Default = this;
        }

        private void Start()
        {
            // Ϊ HurtBox ���һ���¼�
            HurtBox.OnTriggerEnter2DEvent(collider2D =>
            {
                HitHurtBox hitBox = collider2D.GetComponent<HitHurtBox>();

                if (hitBox != null)
                {
                    if (hitBox.Owner.CompareTag("Enemy"))
                    {
                        Global.HP.Value--;
                        if (Global.HP.Value <= 0)
                        {
                            //IsDead = true;
                            //HurtBox.enabled = false;
                            //SelfRigidbody2D.velocity = Vector2.zero;
                            //SelfRigidbody2D.isKinematic = true;

                            // �ؽŲ���
                            if (mWalkSfx != null)
                            {
                                mWalkSfx.Stop();
                                mWalkSfx = null;
                            }

                            // ����������Ч
                            AudioKit.PlaySound("Die");
                            // ��������
                            this.DestroyGameObjGracefully();

                            // �� ��Ϸ�������
                            UIKit.OpenPanel<UIGameOverPanel>();
                        }
                        else
                        {
                            AudioKit.PlaySound("Hurt");
                        }
                    }
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            void UpdateHP()
            {
                HPValue.fillAmount = Global.HP.Value / (float)Global.MaxHP.Value;
            }

            Global.HP.RegisterWithInitValue(hp =>
            {
                UpdateHP();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.MaxHP.RegisterWithInitValue(maxHP =>
            {
                UpdateHP();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            //if (IsDead)
            //{
            //    if (mWalkSfx != null)
            //    {
            //        mWalkSfx.Stop();
            //        mWalkSfx = null;
            //    }

            //    return;
            //}

            float horizontal = Input.GetAxisRaw("Horizontal");  // Input.GetAxisRaw() �Ƚ���Ӳ�ı任
            float vertical = Input.GetAxisRaw("Vertical");

            Vector2 targetVelocity = new Vector2(horizontal, vertical).normalized * (MovementSpeed * (1 + Global.AdditionalMovementSpeed.Value)); // float ����ˣ��ٳ��������������ܸ���

            if (horizontal > 0)
                mFaceRight = true;
            else if (horizontal < 0)
                mFaceRight = false;

            if (horizontal == 0 && vertical == 0)
            {
                if (mFaceRight)
                    Sprite.Play("PlayerIdleRight");
                else
                    Sprite.Play("PlayerIdleLeft");

                if (mWalkSfx != null)
                {
                    mWalkSfx.Stop();
                    mWalkSfx = null;
                }
            }
            else
            {
                if (mFaceRight)
                    Sprite.Play("PlayerWalkRight");
                else
                    Sprite.Play("PlayerWalkLeft");

                if (mWalkSfx == null)
                    mWalkSfx = AudioKit.PlaySound(Sfx.WALK, true);
            }

            SelfRigidbody2D.velocity = Vector2.Lerp(SelfRigidbody2D.velocity, targetVelocity, 1 - Mathf.Exp(-Time.deltaTime * 5));
        }

        public void PlayWalkSFX()
        {
            AudioKit.PlaySound(Sfx.WALK);
        }

        private void OnDestroy()
        {
            Default = null;
        }
    }
}
