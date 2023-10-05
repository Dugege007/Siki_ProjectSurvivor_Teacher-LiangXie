using UnityEngine;
using QFramework;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class Player : ViewController
    {
        public AbilityConfig SimpleSwordConfig;
        public AbilityConfig RotateSwordConfig;
        public AbilityConfig SimpleKnifeConfig;
        public AbilityConfig BasketballConfig;
        public AbilityConfig SimpleBombConfig;
        public AbilityConfig CriticalChanceConfig;
        public AbilityConfig AdditionalExpRateConfig;
        public AbilityConfig AdditionalDamageConfig;
        public AbilityConfig AdditionalMovementSpeedConfig;
        public AbilityConfig AdditionalFlyThingCountConfig;
        public AbilityConfig CollectableAreaRangeConfig;

        public static Player Default;
        public float MovementSpeed = 5f;
        public Color DissolveColor = Color.red;
        public bool IsDead = false;

        private bool mFaceRight;
        private AudioPlayer mWalkSfx;

        private void Awake()
        {
            Default = this;
        }

        private void Start()
        {
            IsDead = false;

            // 为 HurtBox 添加一个事件
            HurtBox.OnTriggerEnter2DEvent(collider2D =>
            {
                HitBox hitBox = collider2D.GetComponent<HitBox>();

                if (hitBox != null)
                {
                    if (hitBox.Owner.CompareTag("Enemy"))
                    {
                        Global.HP.Value--;
                        if (Global.HP.Value <= 0)
                        {
                            IsDead = true;
                            HurtBox.enabled = false;
                            SelfRigidbody2D.velocity = Vector2.zero;
                            SelfRigidbody2D.isKinematic = true;

                            AudioKit.PlaySound("Die");
                            // 打开 游戏结束面板
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
            if (IsDead)
                return;

            float horizontal = Input.GetAxisRaw("Horizontal");  // Input.GetAxisRaw() 比较生硬的变换
            float vertical = Input.GetAxisRaw("Vertical");

            Vector2 targetVelocity = new Vector2(horizontal, vertical).normalized * (MovementSpeed * (1 + Global.AdditionalMovementSpeed.Value)); // float 先相乘，再乘向量，这样性能更高

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
