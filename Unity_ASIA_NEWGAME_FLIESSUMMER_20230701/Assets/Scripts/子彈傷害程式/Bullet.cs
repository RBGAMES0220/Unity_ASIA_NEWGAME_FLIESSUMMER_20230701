using UnityEngine;

namespace GLORY
{
    public class Bullet : MonoBehaviour
    {
        public int damageAmount = 10; // �l�u�ˮ`��

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player")) // �˴��O�_�I�쪱�a
            {
                PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount); // �缾�a�y���ˮ`
                }

                Destroy(gameObject); // �P���l�u����
            }
        }
    }
}

