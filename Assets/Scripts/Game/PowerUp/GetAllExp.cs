using UnityEngine;
using QFramework;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class GetAllExp : GameplayObj
    {
        protected override Collider2D Collider2D => SelfCollider2D;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
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
                            exp.transform.Translate(direction * 8f * Time.deltaTime);
                        }

                    }).UnRegisterWhenGameObjectDestroyed(exp);
                }

                AudioKit.PlaySound(Sfx.GETALLEXP);

                // 销毁自身
                this.DestroyGameObjGracefully();
            }
        }
    }
}
