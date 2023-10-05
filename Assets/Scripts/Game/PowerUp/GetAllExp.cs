using UnityEngine;
using QFramework;
using QAssetBundle;

namespace ProjectSurvivor
{
    public partial class GetAllExp : GameplayObj
    {
        protected override Collider2D Collider2D => SelfCollider2D;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                Global.Exp.Value++;

                foreach (var exp in FindObjectsByType<Exp>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
                {
                    // �� exp ע��һ�� Update �¼����൱�ڸ�����һ������
                    ActionKit.OnUpdate.Register(() =>
                    {
                        Player player = Player.Default;
                        if (player)
                        {
                            // �� Exp �������
                            Vector3 direction = (player.Position() - exp.Position()).normalized;
                            exp.transform.Translate(direction * 8f * Time.deltaTime);
                        }

                    }).UnRegisterWhenGameObjectDestroyed(exp);
                }

                AudioKit.PlaySound(Sfx.GETALLEXP);

                // ��������
                this.DestroyGameObjGracefully();
            }
        }
    }
}
