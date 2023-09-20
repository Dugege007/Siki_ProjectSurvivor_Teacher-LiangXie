using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Enemy : ViewController
    {
        public float MovementSpeed = 2f;

        private void Update()
        {
            if (Player.Default)
            {
                // 设置方向朝玩家
                Vector3 direction = (Player.Default.transform.position - transform.position).normalized;

                // 移动
                transform.Translate(direction * MovementSpeed * Time.deltaTime);
            }
        }
    }
}
