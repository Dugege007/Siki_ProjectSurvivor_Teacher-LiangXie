using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
	public partial class HurtBox : ViewController
	{
        public GameObject Owner;

        private void Start()
        {
            if (!Owner)
            {
                Owner = transform.parent.gameObject;
            }
        }
    }
}
