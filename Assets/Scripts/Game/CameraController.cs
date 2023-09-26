using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class CameraController : ViewController
    {
        private static CameraController mDefault = null;

        private Vector2 mTargetPosition = Vector2.zero;
        private Vector3 mCurrentCameraPos;
        private bool mShake = false;
        private int mShakeFrame = 0;
        private float mShakeAmplitude;

        private void Awake()
        {
            mDefault = this;
        }

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

                mCurrentCameraPos.x =
                    (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                    .Lerp(transform.position.x, mTargetPosition.x);

                mCurrentCameraPos.y =
                    (1.0f - Mathf.Exp(-Time.deltaTime * 20))
                    .Lerp(transform.position.y, mTargetPosition.y);

                mCurrentCameraPos.z = transform.position.z;

                if (mShake)
                {
                    mShakeFrame--;
                    if (mShakeFrame % 2 == 0)
                    {
                        // 振幅
                        float shakeA = Mathf.Lerp(mShakeAmplitude, 0f, mShakeFrame / 30);

                        transform.position = new Vector3(
                            mCurrentCameraPos.x + Random.Range(-shakeA, shakeA),
                            mCurrentCameraPos.y + Random.Range(-shakeA, shakeA),
                            mCurrentCameraPos.z);
                    }

                    if (mShakeFrame <= 0)
                    {
                        mShake = false;
                    }
                }
                else
                {
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

        public static void Shake()
        {
            mDefault.mShake = true;
            mDefault.mShakeFrame = 30;
            mDefault.mShakeAmplitude = 0.25f;
        }

        private void OnDestroy()
        {
            mDefault = null;
        }
    }
}
