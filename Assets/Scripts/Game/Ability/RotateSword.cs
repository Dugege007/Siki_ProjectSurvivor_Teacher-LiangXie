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
            // ��ʼʱ����һ��������
            CreateSwords();

            // �����������������仯
            Global.RotateSwordCount.Or(Global.AdditionalFlyThingCount)
                .Register(CreateSwords)
                .UnRegisterWhenGameObjectDestroyed(gameObject);

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

        private void CreateSwords()
        {
            int toAddCount = Global.RotateSwordCount.Value + Global.AdditionalFlyThingCount.Value - mBigSwords.Count;

            for (int i = 0; i < toAddCount; i++)
            {
                mBigSwords.Add(BigSword
                    .InstantiateWithParent(this)
                    .Self(self =>   // �õ�����
                    {
                        // �����ײ�¼�
                        self.OnTriggerEnter2DEvent(collider =>
                        {
                            HurtBox hurtBox = collider.GetComponent<HurtBox>();
                            if (hurtBox)
                            {
                                if (hurtBox.Owner.CompareTag("Enemy"))
                                {
                                    if (Random.Range(0, 1.0f) < 0.5f)
                                    {
                                        // ����Ч��
                                        collider.attachedRigidbody.velocity =
                                            (collider.transform.position - transform.Position()).normalized * 5 +
                                            (collider.transform.position - Player.Default.Position()).normalized * 10;
                                    }

                                    IEnemy e = hurtBox.Owner.GetComponent<IEnemy>();
                                    DamageSystem.CalculateDamage(Global.RotateSwordDamage.Value, e);
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
