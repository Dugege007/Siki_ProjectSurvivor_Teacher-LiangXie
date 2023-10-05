using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public abstract class PowerUp : GameplayObj
    {
        public bool FlyingToPlayer { get; set; }

        private int FlyingToPlayerFrameCount = 0;

        private void Update()
        {
            if (FlyingToPlayer)
            {
                if (FlyingToPlayerFrameCount == 0)
                    GetComponent<SpriteRenderer>().sortingOrder = 5;

                FlyingToPlayerFrameCount++;

                if (Player.Default)
                {
                    Vector2 direction = Player.Default.Direction2DFrom(this);
                    float distance = Vector2.Distance(transform.position, Player.Default.Position());

                    if (FlyingToPlayerFrameCount <= 15)
                        transform.Translate(direction.normalized * -2 * Time.deltaTime);
                    else
                        transform.Translate(direction.normalized * 7.5f * Time.deltaTime);

                    if (distance < 0.5f)
                        Excute();
                }
            }
        }

        protected abstract void Excute();
    }
}
