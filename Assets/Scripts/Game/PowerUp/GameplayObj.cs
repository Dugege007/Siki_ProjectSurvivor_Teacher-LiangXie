using QFramework;
using UnityEngine;

namespace ProjectSurvivor
{
    public abstract class GameplayObj : ViewController
    {
        protected abstract Collider2D Collider2D { get; }

        private void OnBecameVisible()
        {
            Debug.Log("打开碰撞器");
            Collider2D.enabled = true;
        }

        private void OnBecameInvisible()
        {
            Debug.Log("关闭碰撞器");
            Collider2D.enabled = false;
        }
    }
}
