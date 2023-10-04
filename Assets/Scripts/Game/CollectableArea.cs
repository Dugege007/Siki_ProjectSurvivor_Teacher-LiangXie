using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class CollectableArea : ViewController
    {
        private void Start()
        {
            Global.CollectableAreaRange.RegisterWithInitValue(range =>
            {
                GetComponent<CircleCollider2D>().radius = range;

            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}
