using Spine;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // 玩家的最大生命值
    public int currentHealth; // 玩家的當前生命值
    public Animator anim; // 玩家的Animator組件

    private void Start()
    {
        currentHealth = maxHealth; // 將當前生命值初始化為最大生命值
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount)
    {
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
       
    }
}
