using UnityEngine;
using QFramework;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class Basketball : ViewController
    {
        private void Start()
        {
            SelfRigidbody2D.velocity =
                new Vector2(Random.Range(-1.0f, 1.0f),
                Random.Range(-1.0f, 1.0f) *
                    Random.Range(Global.BasketballSpeed.Value - 2,
                    Global.BasketballSpeed.Value + 2));

            HurtBox.OnTriggerEnter2DEvent(collider =>
            {
                HurtBox hurtBox = collider.GetComponent<HurtBox>();
                if (hurtBox)
                {
                    if (hurtBox.Owner.CompareTag("Enemy"))
                    {
                        IEnemy e = hurtBox.Owner.GetComponent<IEnemy>();
                        DamageSystem.CalculateDamage(Global.BasketballDamage.Value, e);

                        if (Random.Range(0, 1.0f) < 0.5f)
                        {
                            // 击退效果
                            collider.attachedRigidbody.velocity
                            = collider.NormalizedDirection2DFrom(this) * 5
                            + collider.NormalizedDirection2DFrom(Player.Default) * 10;
                        }
                    }
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            // 获取法线
            Vector2 normal = collision.GetContact(0).normal;

            if (normal.x > normal.y)
            {
                SelfRigidbody2D.velocity =
                    new Vector2(SelfRigidbody2D.velocity.x,
                    Mathf.Sign(SelfRigidbody2D.velocity.y) *
                        Random.Range(0.5f, 1.5f) *
                        Random.Range(Global.BasketballSpeed.Value - 2, Global.BasketballSpeed.Value + 2));
                // Mathf.Sign() 只取正负符号

                SelfRigidbody2D.angularVelocity = Random.Range(-360, 360);
            }
            else
            {
                Rigidbody2D rb = SelfRigidbody2D;
                rb.velocity =
                    new Vector2(Mathf.Sign(rb.velocity.x) *
                        Random.Range(0.5f, 1.5f) *
                        Random.Range(Global.BasketballSpeed.Value - 2, Global.BasketballSpeed.Value + 2),
                    rb.velocity.y);
                // Mathf.Sign() 只取正负符号

                SelfRigidbody2D.angularVelocity = Random.Range(-360, 360);
            }

            AudioKit.PlaySound(Sfx.BALL);
        }
    }
}
