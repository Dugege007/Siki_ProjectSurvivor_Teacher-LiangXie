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
            Global.BasketballCount.RegisterWithInitValue(count =>
            {
                if (mBasketballs.Count < count)
                {
                    mBasketballs.Add(Basketball.Instantiate()
                        .SyncPosition2DFrom(this)
                        .Show());
                }

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}
