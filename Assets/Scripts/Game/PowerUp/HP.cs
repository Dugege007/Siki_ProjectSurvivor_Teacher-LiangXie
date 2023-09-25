using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class HP : ViewController
	{
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                if (Global.HP.Value == Global.MaxHP.Value) return;

                AudioKit.PlaySound("Coin");
                // 生命值增加
                Global.HP.Value++;
                // 销毁自身
                this.DestroyGameObjGracefully();
            }
        }
    }
}
