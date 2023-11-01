
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorToNextLevel : MonoBehaviour
{
    public List<GameObject> dontDestroyObjects;
    public ConfinerManager confinerManager;
    public Transform playerSpawnPoint; // 引用玩家生成点

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other is CapsuleCollider2D)
        {
            // 获取下一个场景的索引
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            // 切换Confiner
            confinerManager.SwitchConfiner(nextSceneIndex);

            foreach (var obj in dontDestroyObjects)
            {
                DontDestroyOnLoad(obj);
            }

            // 获取玩家对象
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                // 设置玩家位置和方向为生成点的位置和方向
                player.transform.position = playerSpawnPoint.position;
                player.transform.rotation = playerSpawnPoint.rotation;
            }

            // 切换到下一个场景
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}


