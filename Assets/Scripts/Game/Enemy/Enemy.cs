using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Enemy : ViewController, IEnemy
    {
        public float HP = 3;
        public float MovementSpeed = 2f;
        private bool mIgnoreHurt = false;

        public Color DissolveColor = Color.yellow;

        private void Start()
        {
            EnemyGenerator.EnemyCount.Value++;
        }

        private void FixedUpdate()
        {
            if (Player.Default)
            {
                // 设置方向朝玩家
                Vector3 direction = (Player.Default.transform.position - transform.position).normalized;

                // 移动
                SelfRigidbody2D.velocity = direction * MovementSpeed;
            }
            else
            {
                SelfRigidbody2D.velocity = Vector3.zero;
            }
        }

        private void Update()
        {
            if (HP <= 0)
            {
                // 掉落道具
                Global.GeneratePowerUp(gameObject);
                FxController.Play(Sprite, DissolveColor);
                // 销毁自己
                this.DestroyGameObjGracefully();
            }
        }

        /// <summary>
        /// 敌人受到伤害
        /// </summary>
        /// <param name="hurtValue">伤害值</param>
        /// <param name="force">是否强制</param>
        public void Hurt(float hurtValue, bool force = false)
        {
            if (mIgnoreHurt && !force) return;

            // 忽略伤害
            mIgnoreHurt = true;
            // 变为红色
            Sprite.color = Color.red;
            // 伤害飘字
            FloatingTextController.Play(transform.position + Vector3.up * 0.4f, hurtValue.ToString());
            // 播放音效
            AudioKit.PlaySound("Hit");

            // 延时执行
            ActionKit.Delay(0.2f, () =>
            {
                // 减血
                HP -= hurtValue;
                // 变回白色
                Sprite.color = Color.white;
                // 在受伤期间不再受到伤害，避免冲突
                mIgnoreHurt = false;

            }).Start(this);   // 自身执行
        }

        public void SetSpeedScale(float speedScale)
        {
            MovementSpeed *= speedScale;
        }

        public void SetHPScale(float hpScale)
        {
            HP *= hpScale;
        }

        private void OnDestroy()
        {
            EnemyGenerator.EnemyCount.Value--;
        }
    }
}
