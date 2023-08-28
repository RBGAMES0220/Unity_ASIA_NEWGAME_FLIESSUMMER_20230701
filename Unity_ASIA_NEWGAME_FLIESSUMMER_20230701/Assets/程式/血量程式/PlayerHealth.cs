using UnityEngine;

namespace GLORY
{
    public class PlayerHealth : MonoBehaviour
    {
        public int maxHealth = 100; // 玩家的最大生命值
        public int currentHealth; // 玩家的當前生命值
        public Animator anim; // 玩家的Animator組件

        private PlayerController playerController; // 玩家的PlayerController腳本
        public static bool isDead = false; // 是否死亡

        private void Start()
        {
            currentHealth = maxHealth; // 將當前生命值初始化為最大生命值
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

            // 停止播放音效：這裡假設你的音效是由AudioSource組件播放的，如果是其他方式播放音效，相應地進行停止處理
            AudioSource[] audioSources = GetComponents<AudioSource>();
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.Stop();
            }

            // 停止所有動畫：這裡假設你的動畫是由Animator組件控制的，如果是其他方式播放動畫，相應地進行停止處理
            //if (anim != null)
            {
               // anim.enabled = false; // 停用Animator組件
            }

            // 停止與敵人的碰撞檢測：這裡假設你的碰撞檢測是由Collider2D組件控制的，如果是其他方式進行碰撞檢測，相應地進行停止處理
            //Collider2D collider = GetComponent<Collider2D>();
            //if (collider != null)
            {
                //collider.enabled = false; // 停用Collider2D組件
            }

            // 銷毀玩家物件：在完成所有處理後，最後銷毀玩家物件
            //Destroy(gameObject, 2f); // 0.5秒後銷毀物件
        }
    }
}
