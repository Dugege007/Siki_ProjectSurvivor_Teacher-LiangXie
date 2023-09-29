using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class SimpleCircle : ViewController
    {
        private void Start()
        {
            // Ϊ Circle ע��һ�� OnTriggerEnter2D �¼�
            Circle.OnTriggerEnter2DEvent(collider =>
            {
                HurtBox hurtBox = collider.GetComponent<HurtBox>();
                if (hurtBox)
                {
                    if (hurtBox.Owner.CompareTag("Enemy"))
                    {
                        hurtBox.Owner.GetComponent<Enemy>().Hurt(2);
                    }

                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            float radius = 3f;

            float degree = Time.frameCount;

            Vector2 circleLocalPos = new Vector2(-Mathf.Cos(degree * Mathf.Deg2Rad), Mathf.Sin(degree * Mathf.Deg2Rad)) * radius;

            Circle.LocalPosition(circleLocalPos.x, circleLocalPos.y);
        }
    }
}
