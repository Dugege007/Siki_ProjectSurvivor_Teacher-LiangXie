using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class SimpleAxe : ViewController
    {
        private float mCurrentSeconds = 0;

        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;

            if (mCurrentSeconds >= 1.0f)
            {
                Axe.Instantiate()
                    .Show()
                    .Position(this.Position())
                    .Self(self =>
                    {
                        // ��ͷ�ĳ��ٶ�
                        Rigidbody2D rigidbody2D = self.GetComponent<Rigidbody2D>();
                        float randomX = RandomUtility.Choose(-5, -2, 2, 5);
                        float randomY = RandomUtility.Choose(2, 5);
                        rigidbody2D.velocity = new Vector2(randomX, randomY);

                        // Ϊ��ͷ�����ײ�¼�
                        self.OnTriggerEnter2DEvent(collider =>
                        {
                            HitHurtBox hurtBox = collider.GetComponent<HitHurtBox>();
                            if (hurtBox)
                            {
                                if (hurtBox.Owner.CompareTag("Enemy"))
                                {
                                    hurtBox.Owner.GetComponent<Enemy>().Hurt(2);
                                }
                            }

                        }).UnRegisterWhenGameObjectDestroyed(self);

                        // ��ͷ����һ���������������
                        ActionKit.OnUpdate.Register(() =>
                        {
                            if (Player.Default)
                            {
                                if (Player.Default.Position().y - self.PositionY() > Screen.height)
                                {
                                    self.DestroyGameObjGracefully();
                                }
                            }

                        }).UnRegisterWhenGameObjectDestroyed(self);
                    });

                mCurrentSeconds = 0;
            }
        }
    }
}
