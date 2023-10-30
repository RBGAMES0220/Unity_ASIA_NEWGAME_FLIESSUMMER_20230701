using UnityEngine;

namespace GLORY
{
    public class Bullet : MonoBehaviour
    {
        public int damageAmount = 10; // 子彈傷害值

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) // 檢測是否碰到玩家
            {
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount); // 對玩家造成傷害
                }

                Destroy(gameObject); // 銷毀子彈物件
            }
        }
    }
}

