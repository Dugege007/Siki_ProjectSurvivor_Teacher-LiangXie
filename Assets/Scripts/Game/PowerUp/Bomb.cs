using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Bomb : ViewController
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                foreach (var enemyObj in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    Enemy enemy = enemyObj.GetComponent<Enemy>();

                    if (enemy && enemy.gameObject.activeSelf)
                    {
                        enemy.Hurt(Global.SimpleBombDamage.Value);
                    }
                }

                AudioKit.PlaySound("Bomb");
                // ����һ�������¼�
                UIGamePanel.FlashScreen.Trigger();
                CameraController.Shake();

                // ��������
                this.DestroyGameObjGracefully();
            }
        }
    }
}
