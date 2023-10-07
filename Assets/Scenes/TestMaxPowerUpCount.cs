using UnityEngine;
using QFramework;
using System.Collections;

namespace ProjectSurvivor
{
    public partial class TestMaxPowerUpCount : ViewController
    {
        private int mPowerUpCount = 0;

        IEnumerator Start()
        {
            PowerUpManager powerUpManager = FindObjectOfType<PowerUpManager>();

            // 生成一个吸收经验的物品
            powerUpManager.GetAllExp.Instantiate()
                .Position(gameObject.Position())
                .Show();

            // 生成 1000 个 PowerUp
            for (int i = 0; i < 1000; i++)
            {

                for (int j = 0; j < 10; j++)
                {
                    // 随机位置
                    gameObject.Position(Random.Range(3, 20) * RandomUtility.Choose(-1, 1),
                        Random.Range(3, 20) * RandomUtility.Choose(-1, 1));

                    Global.GeneratePowerUp(gameObject, false);
                    mPowerUpCount++;
                }

                // 为了防止卡顿，在帧结束时等待
                // 相当于一帧生成一个
                yield return new WaitForEndOfFrame();
            }
        }

        private void OnGUI()
        {
            // 如果同时在两个 GameObject 中使用 SetDesignResolution() 会出现问题
            // 所以先用 Matrix4x4 缓存一下
            Matrix4x4 cached = GUI.matrix;

            IMGUIHelper.SetDesignResolution(960, 540);
            GUILayout.Space(10);
            GUILayout.Label(mPowerUpCount.ToString());

            GUI.matrix = cached;
        }
    }
}
