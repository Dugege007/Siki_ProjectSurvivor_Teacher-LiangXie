using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class SimpleAbility : ViewController
    {
        private float mCurrentSeconds = 0;

        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;

            if (mCurrentSeconds >= 1.5f)
            {
                mCurrentSeconds = 0;

                Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

                foreach (Enemy enemy in enemies)
                {
                    float distance = Vector2.Distance(Player.Default.transform.position, enemy.transform.position);

                    if (distance <= 5)
                    {
                        // 变为红色
                        enemy.Sprite.color = Color.red;
                        // 缓存 enemy 的引用
                        Enemy enemyRefCache = enemy;

                        // 延时执行
                        ActionKit.Delay(0.3f, () =>
                        {
                            // 减血
                            enemyRefCache.HP--;
                            // 变回白色
                            enemy.Sprite.color = Color.white;

                        }).StartGlobal();   // 全局执行
                    }
                }
            }
        }
    }
}
