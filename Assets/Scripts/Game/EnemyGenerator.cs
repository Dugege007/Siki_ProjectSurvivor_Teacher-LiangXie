using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class EnemyGenerator : ViewController
    {
        private float mCurrentSeconds = 0;

        private void Update()
        {
            mCurrentSeconds += Time.deltaTime;

            if (mCurrentSeconds >= 1)
            {
                mCurrentSeconds = 0;

                Player player = Player.Default;
                // 随机角度
                float randomAngle = Random.Range(0, 360f);
                // 获取弧度
                float randomRadius = randomAngle * Mathf.Deg2Rad;
                // 计算方向
                Vector3 direction = new Vector3(Mathf.Cos(randomRadius), Mathf.Sin(randomRadius));
                //
                Vector3 generatePos = player.transform.position + direction * 10;

                // 生成敌人
                Enemy.Instantiate()
                    .Position(generatePos)
                    .Show();
            }
        }
    }
}
