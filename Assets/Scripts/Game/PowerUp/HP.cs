using UnityEngine;
using QFramework;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class HP : PowerUp
    {
        protected override Collider2D Collider2D => SelfCollider2D;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                if (Global.HP.Value == Global.MaxHP.Value) return;

                FlyingToPlayer = true;
            }
        }

        protected override void Excute()
        {
            AudioKit.PlaySound(Sfx.HP);
            // ����ֵ����
            Global.HP.Value++;

            GetComponent<SpriteRenderer>().sortingOrder = 1;
            // ��������
            this.DestroyGameObjGracefully();
        }
    }
}
