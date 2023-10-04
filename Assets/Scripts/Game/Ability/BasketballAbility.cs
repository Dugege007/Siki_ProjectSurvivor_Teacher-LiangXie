using UnityEngine;
using QFramework;
using System.Collections.Generic;

namespace ProjectSurvivor
{
    public partial class BasketballAbility : ViewController
    {
        private List<Basketball> mBasketballs = new List<Basketball>();

        private void Start()
        {
            // ��ʼʱ����һ����
            CreateBalls();

            // ������������仯
            Global.BasketballCount.Or(Global.AdditionalFlyThingCount)
                .Register(CreateBalls)
                .UnRegisterWhenGameObjectDestroyed(gameObject);
        }

        private void CreateBalls()
        {
            int ballCount2Create = Global.BasketballCount.Value + Global.AdditionalFlyThingCount.Value - mBasketballs.Count;

            for (int i = 0; i < ballCount2Create; i++)
            {
                mBasketballs.Add(Basketball.Instantiate()
                    .SyncPosition2DFrom(this)
                    .Show());
            }
        }
    }
}
