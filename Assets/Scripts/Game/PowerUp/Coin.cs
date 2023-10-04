using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Coin : GameplayObj
    {
        protected override Collider2D Collider2D => selfCollider2D;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                AudioKit.PlaySound("Coin");
                // 金币增加
                Global.Coin.Value++;
                // 销毁自身
                this.DestroyGameObjGracefully();
            }
        }
    }
}
