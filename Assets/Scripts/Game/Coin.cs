using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Coin : ViewController
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                AudioKit.PlaySound("Coin");
                // 经验增加
                Global.Coin.Value++;
                // 销毁自身
                this.DestroyGameObjGracefully();
            }
        }
    }
}
