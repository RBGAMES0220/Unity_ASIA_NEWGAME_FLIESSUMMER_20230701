using UnityEngine;

namespace GLORY
{

    public class HealthPotion : MonoBehaviour
    {
        public int healthAmount = 20; // 補血的數量
        public AudioClip drinkSound; // 喝補血藥水的音效

        private void OnTriggerEnter(Collider other)
        {
            // 檢查碰撞的對象是否是玩家
            if (other.CompareTag("Player"))
            {
                // 玩家觸碰到藥水時，執行回復生命值的操作
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.Heal(healthAmount); // 呼叫玩家健康組件的回復生命值方法
                    AudioSource.PlayClipAtPoint(drinkSound, transform.position); // 播放音效
                    Destroy(gameObject); // 摧毀藥水遊戲物件
                }
            }
        }
    }
}