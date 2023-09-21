using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class PowerUpManager : ViewController
    {
        public static PowerUpManager Default;

        private void Awake()
        {
            Default = this;
        }

        private void OnDestroy()
        {
            Default = null;
        }
    }
}
