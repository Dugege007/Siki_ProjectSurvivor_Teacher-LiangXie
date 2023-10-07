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

            // ����һ�����վ������Ʒ
            powerUpManager.GetAllExp.Instantiate()
                .Position(gameObject.Position())
                .Show();

            // ���� 1000 �� PowerUp
            for (int i = 0; i < 1000; i++)
            {

                for (int j = 0; j < 10; j++)
                {
                    // ���λ��
                    gameObject.Position(Random.Range(3, 20) * RandomUtility.Choose(-1, 1),
                        Random.Range(3, 20) * RandomUtility.Choose(-1, 1));

                    Global.GeneratePowerUp(gameObject, false);
                    mPowerUpCount++;
                }

                // Ϊ�˷�ֹ���٣���֡����ʱ�ȴ�
                // �൱��һ֡����һ��
                yield return new WaitForEndOfFrame();
            }
        }

        private void OnGUI()
        {
            // ���ͬʱ������ GameObject ��ʹ�� SetDesignResolution() ���������
            // �������� Matrix4x4 ����һ��
            Matrix4x4 cached = GUI.matrix;

            IMGUIHelper.SetDesignResolution(960, 540);
            GUILayout.Space(10);
            GUILayout.Label(mPowerUpCount.ToString());

            GUI.matrix = cached;
        }
    }
}
