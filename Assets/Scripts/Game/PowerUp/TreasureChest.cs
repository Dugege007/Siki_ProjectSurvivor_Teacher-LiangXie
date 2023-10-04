using UnityEngine;
using QFramework;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class TreasureChest : GameplayObj
    {
        protected override Collider2D Collider2D => SelfCollider2D;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                // ������Ч
                AudioKit.PlaySound(Sfx.TREASUERCHEST);
                // �����¼�
                UIGamePanel.OpenTreasurePanel.Trigger();

                this.DestroyGameObjGracefully();
            }
        }
    }
}
