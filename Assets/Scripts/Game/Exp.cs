using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Exp : ViewController
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>() != null)
            {
                Global.Exp.Value++;
                this.DestroySelfGracefully();
            }
        }
    }
}
