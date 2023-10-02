using UnityEngine;
using QFramework;
using System.Collections.Generic;

namespace ProjectSurvivor
{
    public partial class RotateSword : ViewController
    {
        private List<Collider2D> mBigSwords = new List<Collider2D>();

        private void Start()
        {
            // 为 BigSword 注册一个 OnTriggerEnter2D 事件
            BigSword.OnTriggerEnter2DEvent(collider =>
            {
                HurtBox hurtBox = collider.GetComponent<HurtBox>();
                if (hurtBox)
                {
                    if (hurtBox.Owner.CompareTag("Enemy"))
                    {
                        hurtBox.Owner.GetComponent<Enemy>().Hurt(Global.RotateSwordDamage.Value);
                    }
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            BigSword.Hide();

            Global.RotateSwordCount.RegisterWithInitValue(count =>
            {
                int toAddCount = mBigSwords.Count;

                for (int i = 0; i < toAddCount; i++)
                {
                    mBigSwords.Add(BigSword.InstantiateWithParent(this)
                         .Show());
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            UpdateCirclePos();
        }

        private void Update()
        {
            float degree = Time.frameCount;

            this.LocalEulerAnglesZ(-degree);
        }

        private void UpdateCirclePos()
        {
            float radius = 3f;
            float durationDegrees = 360 / mBigSwords.Count;

            for (int i = 0; i < mBigSwords.Count; i++)
            {
                Vector2 circleLocalPos = new Vector2(Mathf.Cos(durationDegrees * i * Mathf.Deg2Rad), Mathf.Sin(durationDegrees * i * Mathf.Deg2Rad)) * radius;

                mBigSwords[i].LocalPosition(circleLocalPos.x, circleLocalPos.y)
                    .LocalEulerAnglesZ(durationDegrees * i - 90);
            }
        }
    }
}
