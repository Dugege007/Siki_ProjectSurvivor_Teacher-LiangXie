using UnityEngine;
using QFramework;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class Bomb : PowerUp
    {
        protected override Collider2D Collider2D => SelfCollider2D;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                FlyingToPlayer = true;

                GetComponent<SpriteRenderer>().sortingOrder = 1;
                // ��������
                this.DestroyGameObjGracefully();
            }
        }

        protected override void Excute()
        {
            foreach (var enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Enemy enemy = enemyObj.GetComponent<Enemy>();

                if (enemy && enemy.gameObject.activeSelf)
                {
                    DamageSystem.CalculateDamage(Global.SimpleBombDamage.Value, enemy);
                }
            }

            AudioKit.PlaySound(Sfx.BOMB);
            // ����һ�������¼�
            UIGamePanel.FlashScreen.Trigger();
            CameraController.Shake();
        }

        // �� Excute() ����һ�����ṩ���ⲿʹ��
        public static void GetExcute()
        {
            foreach (var enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Enemy enemy = enemyObj.GetComponent<Enemy>();

                if (enemy && enemy.gameObject.activeSelf)
                {
                    DamageSystem.CalculateDamage(Global.SimpleBombDamage.Value, enemy);
                }
            }

            AudioKit.PlaySound(Sfx.BOMB);
            // ����һ�������¼�
            UIGamePanel.FlashScreen.Trigger();
            CameraController.Shake();
        }
    }
}
