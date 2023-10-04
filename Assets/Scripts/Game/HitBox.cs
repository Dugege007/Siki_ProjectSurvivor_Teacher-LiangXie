using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class HitBox : GameplayObj
    {
        public GameObject Owner;
        private Collider2D mCollider2D;
        protected override Collider2D Collider2D => mCollider2D;

        private void Awake()
        {
            mCollider2D = GetComponent<Collider2D>();
        }

        private void Start()
        {
            if (!Owner)
            {
                Owner = transform.parent.gameObject;
            }
        }
    }
}
