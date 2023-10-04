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
            // 触发一下闪屏事件
            UIGamePanel.FlashScreen.Trigger();
            CameraController.Shake();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                Excute();

                // 销毁自身
                this.DestroyGameObjGracefully();
            }
        }
    }
}
