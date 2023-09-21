using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Enemy : ViewController
    {
        public float HP = 3;
        public float MovementSpeed = 2f;
        private bool mIgnoreHurt = false;

        private void Start()
        {
            EnemyGenerator.EnemyCount.Value++;
        }

        private void Update()
        {
            if (Player.Default)
            {
                // 设置方向朝玩家
                Vector3 direction = (Player.Default.transform.position - transform.position).normalized;

                // 移动
                transform.Translate(direction * MovementSpeed * Time.deltaTime);
            }

            if (HP <= 0)
            {
                // 掉落道具
                Global.GeneratePowerUp(gameObject);
                // 销毁自己
                this.DestroyGameObjGracefully();
            }
        }

        public void Hurt(float value)
        {
            if (mIgnoreHurt) return;

            // 忽略伤害
            mIgnoreHurt = true;
            // 变为红色
            Sprite.color = Color.red;

            // 延时执行
            ActionKit.Delay(0.2f, () =>
            {
                // 减血
                HP -= value;
                // 变回白色
                Sprite.color = Color.white;
                // 在受伤期间不再受到伤害，避免冲突
                mIgnoreHurt = false;

            }).Start(this);   // 自身执行
        }

        private void OnDestroy()
        {
            EnemyGenerator.EnemyCount.Value--;
        }
    }
}
