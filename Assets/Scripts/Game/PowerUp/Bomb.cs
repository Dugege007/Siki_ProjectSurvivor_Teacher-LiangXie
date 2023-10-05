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
                // 销毁自身
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
            // 触发一下闪屏事件
            UIGamePanel.FlashScreen.Trigger();
            CameraController.Shake();
        }

        // 和 Excute() 代码一样，提供给外部使用
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
            // 触发一下闪屏事件
            UIGamePanel.FlashScreen.Trigger();
            CameraController.Shake();
        }
    }
}
