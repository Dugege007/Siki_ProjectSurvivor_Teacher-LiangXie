using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Player : ViewController
    {
        public AbilityConfig SimpleSwordConfig;
        public AbilityConfig RotateSwordConfig;
        public AbilityConfig SimpleKnifeConfig;
        public AbilityConfig BasketballConfig;
        public AbilityConfig SimpleBombConfig;
        public AbilityConfig CriticalRateConfig;
        public AbilityConfig DamageRateConfig;

        public static Player Default;
        public float MovementSpeed = 5f;
        public Color DissolveColor = Color.red;
        public bool IsDead = false;

        private void Awake()
        {
            Default = this;
        }

        private void Start()
        {
            IsDead = false;

            // Ϊ HurtBox ���һ���¼�
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
                            // �����ܽ���Ч
                            FxController.Play(Sprite, DissolveColor);
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
            if (IsDead)
                return;

            float horizontal = Input.GetAxisRaw("Horizontal");  // Input.GetAxisRaw() �Ƚ���Ӳ�ı任
            float vertical = Input.GetAxisRaw("Vertical");
            Vector2 targetVelocity = new Vector2(horizontal, vertical).normalized * MovementSpeed;

            SelfRigidbody2D.velocity = Vector2.Lerp(SelfRigidbody2D.velocity, targetVelocity, 1 - Mathf.Exp(-Time.deltaTime * 5));
        }

        private void OnDestroy()
        {
            Default = null;
        }
    }
}
