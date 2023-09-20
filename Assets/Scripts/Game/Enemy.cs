using UnityEngine;
using QFramework;

namespace ProjectSurvivor
{
    public partial class Enemy : ViewController
    {
        public float MovementSpeed = 2f;

        private void Update()
        {
            Player player = FindObjectOfType<Player>();

            Vector3 direction = (player.transform.position - transform.position).normalized;

            transform.Translate(direction * MovementSpeed * Time.deltaTime);
        }
    }
}
