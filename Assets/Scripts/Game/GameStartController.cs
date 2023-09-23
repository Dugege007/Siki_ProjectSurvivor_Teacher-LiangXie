using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class GameStartController : ViewController
    {
        private void Awake()
        {
            // 游戏开始时需要加载所有资源，进行一次初始化
            ResKit.Init();
        }

        private void Start()
        {
            UIKit.OpenPanel<UIGameStartPanel>();
        }
    }
}
