using UnityEngine;
using QFramework;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class Bomb : ViewController
    {
        public static void Excute()
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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                Excute();

                // ��������
                this.DestroyGameObjGracefully();
            }
        }
    }
}
