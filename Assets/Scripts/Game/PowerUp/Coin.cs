using UnityEngine;
using QFramework;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class Coin : PowerUp
    {
        protected override Collider2D Collider2D => SelfCollider2D;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                FlyingToPlayer = true;
            }
        }

        protected override void Excute()
        {
            AudioKit.PlaySound(Sfx.COIN);
            // �������
            Global.Coin.Value++;

            GetComponent<SpriteRenderer>().sortingOrder = 1;
            // ��������
            this.DestroyGameObjGracefully();
        }
    }
}
