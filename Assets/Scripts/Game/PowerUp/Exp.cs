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

                Global.Exp.Value += 1 + Global.AdditionalExpRate.Value;

                this.DestroyGameObjGracefully();
            }
        }
    }
}
