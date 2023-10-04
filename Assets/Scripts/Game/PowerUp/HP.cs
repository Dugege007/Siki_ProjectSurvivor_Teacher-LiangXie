using UnityEngine;
using QFramework;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class HP : GameplayObj
    {
        protected override Collider2D Collider2D => SelfCollider2D;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                if (Global.HP.Value == Global.MaxHP.Value) return;

                AudioKit.PlaySound(Sfx.HP);
                // ����ֵ����
                Global.HP.Value++;
                // ��������
                this.DestroyGameObjGracefully();
            }
        }
    }
}
