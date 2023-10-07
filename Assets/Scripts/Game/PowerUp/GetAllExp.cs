using UnityEngine;
using QFramework;
using QAssetBundle;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace ProjectSurvivor
{
    public partial class GetAllExp : PowerUp
    {
        protected override Collider2D Collider2D => SelfCollider2D;

        static IEnumerator FlyToPlayerStart()
        {
            // �ҵ����е� PowerUp ���� Exp �� Coin
            IEnumerable<PowerUp> exps = FindObjectsByType<Exp>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            IEnumerable<PowerUp> coins = FindObjectsByType<Coin>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            // ������ƴ�ӣ�ʹ������Ʒ������˳�������ң��������ȷ���һ���ٷ���һ��
            foreach (var powerUp in exps.Concat(coins).OrderByDescending(e => e.InScreen))
            {
                
            }

            // Ŀǰ�Ĵ��뻹������
            // �����ռ�ȫ�� Exp
            foreach (var exp in FindObjectsByType<Exp>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                .OrderByDescending(e => e.InScreen))
            {
                if (exp.InScreen)
                {
                    // �� exp ע��һ�� Update �¼����൱�ڸ�����һ������
                    ActionKit.OnUpdate.Register(() =>
                    {
                        Player player = Player.Default;
                        if (player)
                        {
                            // �� Exp �������
                            Vector3 direction = (player.Position() - exp.Position()).normalized;
                            exp.transform.Translate(direction * 12f * Time.deltaTime);
                        }

                    }).UnRegisterWhenGameObjectDestroyed(exp);
                }
                else
                {
                    yield return new WaitForEndOfFrame();
                }
            }

            // ���ռ� Coin
            foreach (var coin in FindObjectsByType<Coin>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                .OrderByDescending(e => e.InScreen))
            {
                if (coin.InScreen)
                {
                    // �� Coin ע��һ�� Update �¼����൱�ڸ�����һ������
                    ActionKit.OnUpdate.Register(() =>
                    {
                        Player player = Player.Default;
                        if (player)
                        {
                            // �� Coin �������
                            Vector3 direction = (player.Position() - coin.Position()).normalized;
                            coin.transform.Translate(direction * 8f * Time.deltaTime);
                        }

                    }).UnRegisterWhenGameObjectDestroyed(coin);
                }
                else
                {
                    yield return new WaitForEndOfFrame();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<CollectableArea>())
            {
                FlyingToPlayer = true;
            }
        }

        protected override void Excute()
        {
            Global.Exp.Value++;

            PowerUpManager.Default.StartCoroutine(FlyToPlayerStart());

            AudioKit.PlaySound(Sfx.GETALLEXP);

            GetComponent<SpriteRenderer>().sortingOrder = 1;
            // ��������
            this.DestroyGameObjGracefully();
        }
    }
}
