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
                // �������
                Global.Coin.Value++;
                // ��������
                this.DestroyGameObjGracefully();
            }
        }
    }
}
