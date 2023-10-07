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
            // 开始时生成一次守卫剑
            CreateSwords();

            // 监听守卫剑的数量变化
            Global.RotateSwordCount.Or(Global.AdditionalFlyThingCount)
                .Register(CreateSwords)
                .UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.RotateSwordRange.Register(range =>
            {
                UpdateCirclePos();

            }).UnRegisterWhenGameObjectDestroyed(gameObject);

            Global.SuperRotateSword.RegisterWithInitValue(unlocked =>
            {
                if (unlocked)
                    this.LocalScale(1.5f);
                else
                    this.LocalScale(1);

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void Update()
        {
            float degree = Time.frameCount * Global.RotateSwordSpeed.Value;

            this.LocalEulerAnglesZ(-degree);
        }

        private void CreateSwords()
        {
            int toAddCount = Global.RotateSwordCount.Value + Global.AdditionalFlyThingCount.Value - mBigSwords.Count;

            for (int i = 0; i < toAddCount; i++)
            {
                mBigSwords.Add(BigSword
                    .InstantiateWithParent(this)
                    .Self(self =>   // 拿到自身
                    {
                        // 添加碰撞事件
                        self.OnTriggerEnter2DEvent(collider =>
                        {
                            HitHurtBox hurtBox = collider.GetComponent<HitHurtBox>();
                            if (hurtBox)
                            {
                                if (hurtBox.Owner.CompareTag("Enemy"))
                                {
                                    int damageTimes = Global.SuperRotateSword.Value ? 2 : 1;

                                    if (Random.Range(0, 1.0f) < 0.5f)
                                    {
                                        // 击退效果
                                        collider.attachedRigidbody.AddForce(
                                            (collider.transform.position - transform.Position()).normalized * 500 +
                                            (collider.transform.position - Player.Default.Position()).normalized * 1000);
                                    }

                                    IEnemy e = hurtBox.Owner.GetComponent<IEnemy>();
                                    DamageSystem.CalculateDamage(Global.RotateSwordDamage.Value * damageTimes, e);
                                }
                            }

                        }).UnRegisterWhenGameObjectDestroyed(self);
                    })
                    .Show());
            }

            UpdateCirclePos();
        }

        private void UpdateCirclePos()
        {
            float rangeTimes = Global.SuperRotateSword.Value ? 1.2f : 1;

            float radius = Global.RotateSwordRange.Value * rangeTimes;
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
