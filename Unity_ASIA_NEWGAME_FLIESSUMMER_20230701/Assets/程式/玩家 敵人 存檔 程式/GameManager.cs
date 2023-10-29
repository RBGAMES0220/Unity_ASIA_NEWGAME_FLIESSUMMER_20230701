using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        if (PlayerData.Instance.playerPrefab != null)
        {
            // 生成玩家
            GameObject player = Instantiate(PlayerData.Instance.playerPrefab, PlayerData.Instance.playerPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Player prefab is not set in PlayerData.");
        }
    }
}

