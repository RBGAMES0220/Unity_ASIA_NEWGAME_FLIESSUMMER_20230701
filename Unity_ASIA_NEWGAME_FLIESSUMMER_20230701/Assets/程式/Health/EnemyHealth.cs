using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // 最大生命值
    public int currentHealth; // 當前生命值

    public Slider healthSlider; // 生命條Slider
    public Animator animator; // 敵人的Animator

    private bool isDead = false; // 是否死亡
    private bool isTakingDamage = false; // 是否正在受到傷害

    private void Start()
    {
        currentHealth = maxHealth; // 初始化當前生命值為最大生命值

        if (healthSlider != null)
        {
            healthSlider.maxValue = maxHealth; // 設定生命條的最大值
            healthSlider.value = currentHealth; // 設定生命條的當前值
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return; // 如果已死亡，則不處理傷害

        currentHealth -= damageAmount;

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth; // 更新生命條的當前值
        }

        // 只有在敵人還活著的情況下，才觸發受傷動畫
        if (animator != null && !isTakingDamage && currentHealth > 0)
        {
            animator.SetTrigger("hurt");
            isTakingDamage = true;
        }

        if (currentHealth <= 0)
        {
            Die(); // 生命值小於等於0時，敵人死亡
        }
    }

    private void Die()
    {
        isDead = true;
        // 敵人死亡的處理邏輯
        Debug.Log("Enemy died!");

        // 播放死亡動畫
        if (animator != null)
        {
            animator.SetTrigger("die");
        }

        // 銷毀敵人物件
        Destroy(gameObject, 2f); // 2秒後銷毀物件

        // 停止敵人的移動或攻擊行為

        
    }
}



