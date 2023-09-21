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

            if (mCurrentSeconds >= Global.SimpleAbilityDuration.Value)
            {
                mCurrentSeconds = 0;

                Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

                foreach (Enemy enemy in enemies)
                {
                    float distance = Vector2.Distance(Player.Default.transform.position, enemy.transform.position);

                    if (distance <= 5)
                    {
                        enemy.Hurt(Global.SimpleAbilityDamage.Value);
                    }
                }
            }
        }
    }
}
