using UnityEngine;
using UnityEngine.SceneManagement;

namespace GLORY
{
    public class PlayerFinal : MonoBehaviour
    {
        private void OnDestroy()
        {
            FinalManager.instance.GameOver("感謝遊玩");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("通關"))
            {
                FinalManager.instance.GameOver("遊戲通關");

                Destroy(gameObject);
            }
        }
    }
}

