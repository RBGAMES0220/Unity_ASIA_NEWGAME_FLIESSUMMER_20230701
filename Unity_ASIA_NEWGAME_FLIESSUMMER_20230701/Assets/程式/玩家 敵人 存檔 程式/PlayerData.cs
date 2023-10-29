using UnityEngine;

public class PlayerData : MonoBehaviour
{
    // 單例模式
    public static PlayerData Instance { get; private set; }

    // 玩家的位置
    public Vector3 playerPosition = Vector3.zero;

    // 玩家的預置物件
    public GameObject playerPrefab;

    private void Awake()
    {
        // 確保只有一個 PlayerData 實例存在
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
