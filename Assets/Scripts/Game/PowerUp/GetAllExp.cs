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
            // 将他们拼接，使两种物品能自由顺序飞向玩家，而不是先飞完一种再飞另一种
            foreach (var powerUp in exps.Concat(coins).OrderByDescending(e => e.InScreen))
            {
                
            }

            // 目前的代码还有问题
            // 会先收集全部 Exp
            foreach (var exp in FindObjectsByType<Exp>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                .OrderByDescending(e => e.InScreen))
            {
                if (exp.InScreen)
                {
                    // 给 exp 注册一个 Update 事件，相当于给它挂一个任务
                    ActionKit.OnUpdate.Register(() =>
                    {
                        Player player = Player.Default;
                        if (player)
                        {
                            // 让 Exp 飞向玩家
                            Vector3 direction = (player.Position() - exp.Position()).normalized;
                            exp.transform.Translate(direction * 12f * Time.deltaTime);
                        }

                    }).UnRegisterWhenGameObjectDestroyed(exp);
                }
                else
                {
                    yield return new WaitForEndOfFrame();
                }
            }

            // 再收集 Coin
            foreach (var coin in FindObjectsByType<Coin>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                .OrderByDescending(e => e.InScreen))
            {
                if (coin.InScreen)
                {
                    // 给 Coin 注册一个 Update 事件，相当于给它挂一个任务
                    ActionKit.OnUpdate.Register(() =>
                    {
                        Player player = Player.Default;
                        if (player)
                        {
                            // 让 Coin 飞向玩家
                            Vector3 direction = (player.Position() - coin.Position()).normalized;
                            coin.transform.Translate(direction * 8f * Time.deltaTime);
                        }

                    }).UnRegisterWhenGameObjectDestroyed(coin);
                }
                else
                {
                    yield return new WaitForEndOfFrame();
                }
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
