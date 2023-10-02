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

            BigSword.Hide();

            Global.RotateSwordCount.RegisterWithInitValue(count =>
            {
                int toAddCount = count - mBigSwords.Count;

                for (int i = 0; i < toAddCount; i++)
                {
                    mBigSwords.Add(BigSword
                        .InstantiateWithParent(this)
                        .Self(self =>   // 拿到自身
                        {
                            // 添加碰撞事件
                            self.OnTriggerEnter2DEvent(collider =>
                            {
                                HurtBox hurtBox = collider.GetComponent<HurtBox>();
                                if (hurtBox)
                                {
                                    if (hurtBox.Owner.CompareTag("Enemy"))
                                    {
                                        hurtBox.Owner.GetComponent<Enemy>().Hurt(Global.RotateSwordDamage.Value);

                                        if (Random.Range(0, 1.0f) < 0.5f)
                                        {
                                            collider.attachedRigidbody.velocity
                                            = collider.NormalizedDirection2DFrom(self) * 5
                                            + collider.NormalizedDirection2DFrom(Player.Default) * 10;
                                        }
                                    }
                                }

                            }).UnRegisterWhenGameObjectDestroyed(self);
                        })
                        .Show());
                }

                UpdateCirclePos();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.RotateSwordRange.Register(range =>
            {
                UpdateCirclePos();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            float degree = Time.frameCount * Global.RotateSwordSpeed.Value;

            this.LocalEulerAnglesZ(-degree);
        }

        private void UpdateCirclePos()
        {
            float radius = Global.RotateSwordRange.Value;
            float durationDegrees = 1;
            if (mBigSwords.Count > 0)
                durationDegrees = 360 / mBigSwords.Count;

            for (int i = 0; i < mBigSwords.Count; i++)
            {
                Vector2 circleLocalPos = new Vector2(Mathf.Cos(durationDegrees * i * Mathf.Deg2Rad), Mathf.Sin(durationDegrees * i * Mathf.Deg2Rad)) * radius;

                mBigSwords[i].LocalPosition(circleLocalPos.x, circleLocalPos.y)
                    .LocalEulerAnglesZ(durationDegrees * i - 90);
            }
        }
    }
}
