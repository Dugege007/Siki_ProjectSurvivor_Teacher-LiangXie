using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class GameStartController : ViewController
    {
        private void Awake()
        {
            // ��Ϸ��ʼʱ��Ҫ����������Դ������һ�γ�ʼ��
            ResKit.Init();
        }

        private void Start()
        {
            UIKit.OpenPanel<UIGameStartPanel>();
        }
    }
}
