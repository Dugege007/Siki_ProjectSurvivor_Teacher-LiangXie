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
                                        IEnemy e = hurtBox.Owner.GetComponent<IEnemy>();
                                        DamageSystem.CalculateDamage(Global.RotateSwordDamage.Value, e);

                                        if (Random.Range(0, 1.0f) < 0.5f)
                                        {
                                            // 击退效果
                                            collider.attachedRigidbody.velocity
                                            = (collider.transform.position - transform.Position()).normalized * 5
                                            + (collider.transform.position-Player.Default.Position()).normalized * 10;
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
