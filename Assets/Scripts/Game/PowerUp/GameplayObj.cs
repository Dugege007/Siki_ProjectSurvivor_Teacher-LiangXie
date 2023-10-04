using QFramework;
using UnityEngine;

namespace ProjectSurvivor
{
    public abstract class GameplayObj : ViewController
    {
        protected abstract Collider2D Collider2D { get; }

        private void OnBecameVisible()
        {
            Collider2D.enabled = true;
        }

        private void OnBecameInvisible()
        {
            Collider2D.enabled = false;
        }
    }
}
