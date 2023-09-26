using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class CameraController : ViewController
    {
        private Vector2 mTargetPosition = Vector2.zero;

        private void Start()
        {
            Application.targetFrameRate = 60;
        }

        private void Update()
        {
            if (Player.Default)
            {
                // 相机跟随玩家
                mTargetPosition = Player.Default.transform.position;
                transform.PositionX(
                    (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                    .Lerp(transform.position.x, mTargetPosition.x));    // 1.0f - Mathf.Exp(-Time.deltaTime * 20) 平滑处理
                                                                        // 此处 .Lerp() 是 QFramework 中的一个功能

                transform.PositionY(
                    (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                    .Lerp(transform.position.y, mTargetPosition.y));
            }
        }
    }
}
