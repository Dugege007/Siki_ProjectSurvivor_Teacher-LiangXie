using UnityEngine;
using QFramework;
using QAssetBundle;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace ProjectSurvivor
{
    public partial class GetAllExp : PowerUp
    {
        protected override Collider2D Collider2D => SelfCollider2D;

        static IEnumerator FlyToPlayerStart()
        {
            // 找到所有的 PowerUp 包括 Exp 和 Coin
            IEnumerable<PowerUp> exps = FindObjectsByType<Exp>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            IEnumerable<PowerUp> coins = FindObjectsByType<Coin>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

            int count = 0;
            // 将他们拼接，使两种物品能自由顺序飞向玩家，而不是先飞完一种再飞另一种
            foreach (var powerUp in exps.Concat(coins)
                .OrderByDescending(e => e.InScreen)
                .ThenBy(e => e.Distance2D(Player.Default)))
            {
                if (powerUp.InScreen)
                {
                    if (count > 8)
                    {
                        count = 0;
                        yield return new WaitForEndOfFrame();
                    }
                }
                else
                {
                    if (count > 2)
                    {
                        count = 0;
                        yield return new WaitForEndOfFrame();
                    }
                }

                count++;
                ActionKit.OnUpdate.Register(() =>
                {
                    Player player = Player.Default;
                    if (player)
                    {
                        // 让 Exp 飞向玩家
                        Vector3 direction = (player.Position() - powerUp.Position()).normalized;
                        powerUp.transform.Translate(direction * 12f * Time.deltaTime);
                    }

                }).UnRegisterWhenGameObjectDestroyed(powerUp);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                FlyingToPlayer = true;
            }
        }

        protected override void Excute()
        {
            Global.Exp.Value++;

            PowerUpManager.Default.StartCoroutine(FlyToPlayerStart());

            AudioKit.PlaySound(Sfx.GETALLEXP);

            GetComponent<SpriteRenderer>().sortingOrder = 1;
            // 销毁自身
            this.DestroyGameObjGracefully();
        }
    }
}
