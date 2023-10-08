using UnityEngine;
using UnityEngine.UI;

namespace GLORY
{
    public class EnemyHealth : MonoBehaviour
    {
        public int maxHealth = 100; // 最大生命值
        public int currentHealth; // 當前生命值

        public Slider healthSlider; // 生命條Slider
        public Animator anim; // 敵人的Animator
        public AudioSource hurtSound; // 敵人受到傷害音效的AudioSource組件
        public GameObject itemPrefab; // 道具的預製物
        public GameObject damageEffect;// 受傷特效的參考
        public Vector3 damageEffectOffset = new Vector3(0f, 1f, 0f); // 敵人受傷特效的上升偏移量

        private bool isDead = false; // 是否死亡
        private bool isTakingDamage = false; // 是否正在受到傷害

        private Rigidbody2D rb; // 敵人的Rigidbody2D
        private EnemyAI movementScript;

        private AchievementManager achievementManager; // Reference to the AchievementManager script
        private void Start()
        {
            currentHealth = maxHealth; // 初始化當前生命值為最大生命值
            movementScript = GetComponent<EnemyAI>();
            anim = GetComponent<Animator>();

            if (healthSlider != null)
            {
                healthSlider.maxValue = maxHealth; // 設定生命條的最大值
                healthSlider.value = currentHealth; // 設定生命條的當前值
            }

            achievementManager = FindObjectOfType<AchievementManager>();
        }

        public void TakeDamage(int damageAmount)
        {
            CinemachineShake.Instance.ShakeCamera(5f, 0.1f);

            if (isDead) return; // 如果已死亡，則不處理傷害

            currentHealth -= damageAmount;

            if (healthSlider != null)
            {
                healthSlider.value = currentHealth; // 更新生命條的當前值
            }

            // 只有在敵人還活著的情況下，才觸發受傷動畫
            if (anim != null && !isTakingDamage && currentHealth > 0)
            {
                anim.SetTrigger("Hurt");
                PlayHurtSound(); // 播放受傷音效
                isTakingDamage = true;

            }

            // 計算生成位置，包括上升偏移量
            Vector3 spawnPosition = transform.position + damageEffectOffset;

            if (currentHealth <= 0)
            {
                Die(); // 生命值小於等於0時，敵人死亡
            }

            // 啟動受傷特效
            if (damageEffect != null)
            {
                GameObject effect = Instantiate(damageEffect, spawnPosition, Quaternion.identity);
                Destroy(effect, 1.0f); // 1.0秒後銷毀特效，你可以根據需要調整時間
            }
        }

        private void Die()
        {
            isDead = true;
            // 敵人死亡的處理邏輯
            Debug.Log("Enemy died!");

            // 播放死亡動畫
            if (anim != null)
            {
                anim.SetTrigger("Die");
            }

            // 銷毀敵人物件
            Destroy(gameObject, 3f); // 3秒後銷毀物件

            // 延遲一秒後生成道具
            Invoke("SpawnItem", 1f);

            // 停止敵人的移動
            if (movementScript != null)
            {
                movementScript.enabled = false; // 停用敵人的移動腳本
            }
        }

        private void SpawnItem()
        {
            // 生成道具
            Instantiate(itemPrefab, transform.position, Quaternion.identity);

            // 可以在這裡添加其他生成後的處理邏輯
        }

        // 受傷音效的播放
        private void PlayHurtSound()
        {
            if (hurtSound != null && !hurtSound.isPlaying)
            {
                hurtSound.Play();
            }
        }


        // 受傷動畫結束時的回調函式
        public void OnHurtAnimationEnd()
        {
            isTakingDamage = false; // 受傷動畫結束後重置為 false
            anim.SetBool("isChasing", true);
        }
    }
}








