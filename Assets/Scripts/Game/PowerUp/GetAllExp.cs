using UnityEngine;
using QFramework;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class GetAllExp : PowerUp
    {
        protected override Collider2D Collider2D => SelfCollider2D;

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

            foreach (var exp in FindObjectsByType<Exp>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
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

            foreach (var coin in FindObjectsByType<Coin>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
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

            AudioKit.PlaySound(Sfx.GETALLEXP);

            GetComponent<SpriteRenderer>().sortingOrder = 1;
            // 销毁自身
            this.DestroyGameObjGracefully();
        }
    }
}
