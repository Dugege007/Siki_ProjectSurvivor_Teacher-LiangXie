using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class GameStartController : ViewController
    {
        private void Start()
        {
            UIKit.OpenPanel<UIGameStartPanel>();
        }
    }
}
