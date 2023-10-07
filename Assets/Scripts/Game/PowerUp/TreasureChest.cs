using UnityEngine;
using QFramework;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class TreasureChest : PowerUp
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
            // 播放音效
            AudioKit.PlaySound(Sfx.TREASUERCHEST);
            // 发送事件
            UIGamePanel.OpenTreasurePanel.Trigger();

            GetComponent<SpriteRenderer>().sortingOrder = 1;
            this.DestroyGameObjGracefully();
        }
    }
}
