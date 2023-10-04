using UnityEngine;
using QFramework;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class Exp : GameplayObj
    {
        protected override Collider2D Collider2D => SelfCollider2D;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                AudioKit.PlaySound(Sfx.EXP);

                Global.Exp.Value += 1 + Global.AdditionalExpRate.Value;

                this.DestroyGameObjGracefully();
            }
        }
    }
}
