using UnityEngine;

namespace GLORY
{
    public class PlayerHealth : MonoBehaviour
    {
        public int maxHealth = 100; // 玩家的最大生命值
        public int currentHealth; // 玩家的當前生命值
        public Animator anim; // 玩家的Animator組件
        public static bool isDead = false; // 是否死亡
        public AudioSource hurtAudioSource; // 受傷音效的AudioSource組件
        public HealthBar healthBar;

        private PlayerController playerController; // 玩家的PlayerController腳本

        private void Start()
        {
            currentHealth = maxHealth; // 將當前生命值初始化為最大生命值
            healthBar.SetMaxHealth(maxHealth);
            anim = GetComponent<Animator>();
            playerController = GetComponent<PlayerController>(); // 取得玩家的PlayerController腳本
            isDead = false;
        }

        public void TakeDamage(int damageAmount)
        {
            // 如果已死亡，則不處理傷害
            if (isDead)
                return;

            currentHealth -= damageAmount; // 扣除傷害值

            healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                Die(); // 如果生命值小於等於0，則觸發死亡事件
            }
            else
            {
                // 在受傷時播放受傷動畫
                if (anim != null)
                {
                    anim.SetTrigger("hurt");
                }
                // 在受傷時播放音效
                if (hurtAudioSource != null)
                {
                    hurtAudioSource.Play();
                }
            }

            // 如果生命值大於 0，將動畫設置回 idle 狀態
            if (currentHealth > 0)
            {
                anim.SetBool("idle", true);
            }
        }

        private void Die()
        {
            // 玩家死亡的處理邏輯
            Debug.Log("Player died!"); // 在控制台輸出死亡消息
            isDead = true; // 設置為死亡狀態，避免重複觸發死亡事件

            // 播放死亡動畫
            if (anim != null)
            {
                anim.SetTrigger("Die");
            }

            // 停止玩家的移動、攻擊和跳躍
            if (playerController != null)
            {
                playerController.enabled = false; // 停用PlayerController腳本
            }

            // 停止播放音效
            AudioSource[] audioSources = GetComponents<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.Stop();
            }

            // 銷毀玩家物件
            Destroy(gameObject, 2f); // 2秒後銷毀物件
        }

        private void OnTriggerEnter(Collider other)
        {
            // 檢查碰撞的對象是否是補血藥水
            if (other.CompareTag("HealthPotion"))
            {
                // 獲取補血藥水腳本
                HealthPotion healthPotion = other.GetComponent<HealthPotion>();

                if (healthPotion != null)
                {
                    // 使用補血藥水回復生命值
                    Heal(healthPotion.healthAmount);

                    // 播放補血藥水音效
                    if (healthPotion.drinkSound != null)
                    {
                        AudioSource.PlayClipAtPoint(healthPotion.drinkSound, transform.position);
                    }

                    // 銷毀補血藥水遊戲物件
                    Destroy(other.gameObject);
                }
            }
        }

        public void Heal(int amount)
        {
            // 增加生命值
            currentHealth += amount;

            // 確保不超過最大生命值
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }

            // 在這裡您可以添加其他處理回復生命值的邏輯，例如更新UI等

            Debug.Log("玩家回復了 " + amount + " 點生命值，當前生命值：" + currentHealth);
        }
    }
}