using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Exp : ViewController
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                AudioKit.PlaySound("Exp");
                // 经验增加
                Global.Exp.Value++;
                // 销毁自身
                this.DestroyGameObjGracefully();
            }
        }
    }
}
