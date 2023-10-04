using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Exp : GameplayObj
    {
        protected override Collider2D Collider2D => selfCollider2D;

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
