using UnityEngine;
using QFramework;
using System.Linq;

namespace ProjectSurvivor
{
    public partial class SimpleKnife : ViewController
    {
        private float mCurrentSeconds = 0;

        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;

            if (mCurrentSeconds >= 1.0f)
            {
                mCurrentSeconds = 0;

                // ��ȡ��ǰ���ڵĵ����б�
                Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
                // ��ȡ����ĵ���
                Enemy enemy = enemies.OrderBy(e => Vector3.Distance(Player.Default.transform.position, e.transform.position)).FirstOrDefault();

                if (enemy)
                {
                    Knife.Instantiate()
                        .Position(this.Position())
                        .Show()
                        .Self(self =>
                        {
                            // �����˶�����
                            Rigidbody2D rigidbody2D = self.GetComponent<Rigidbody2D>();
                            Vector3 direction = (enemy.Position() - Player.Default.Position()).normalized;
                            rigidbody2D.velocity = direction * 10;

                            // �����ײ�¼�
                            self.OnTriggerEnter2DEvent(collider =>
                            {
                                HurtBox hurtBox = collider.GetComponent<HurtBox>();
                                if (hurtBox)
                                {
                                    // �������˾Ͷ�������˺�
                                    if (hurtBox.Owner.CompareTag("Enemy"))
                                    {
                                        hurtBox.Owner.GetComponent<Enemy>().Hurt(5);
                                        self.DestroyGameObjGracefully();
                                    }
                                }

                            }).UnRegisterWhenGameObjectDestroyed(self);

                            // ע������Զ���������¼�
                            ActionKit.OnUpdate.Register(() =>
                            {
                                if (Player.Default)
                                {
                                    if (Vector3.Distance(Player.Default.Position(), self.Position()) > 20)
                                    {
                                        self.DestroyGameObjGracefully();
                                    }
                                }

                            }).UnRegisterWhenGameObjectDestroyed(self);
                        });
                }
            }
        }
    }
}
