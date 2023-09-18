using UnityEngine;

namespace GLORY
{
    public class BulletController : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // 檢查碰撞是否是牆壁（你可以根據你的遊戲設計使用不同的判斷方法）
            if (collision.gameObject.CompareTag("Map"))
            {
                // 如果碰撞到牆壁，銷毀子彈物體
                Destroy(gameObject);
            }
        }
    }
}

