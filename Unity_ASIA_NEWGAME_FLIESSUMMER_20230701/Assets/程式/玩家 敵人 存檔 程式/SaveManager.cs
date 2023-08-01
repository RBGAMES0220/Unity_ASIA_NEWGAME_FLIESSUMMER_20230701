using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{
    // 存檔資料
    [System.Serializable]
    public class SaveData
    {
        // 定義您需要存儲的遊戲數據，例如玩家位置、生命值等
        public Vector3 playerPosition;
        public int playerHealth;
        // 其他需要存儲的遊戲數據...
    }

    // 存檔檔案的路徑
    private string saveFilePath;

    private void Awake()
    {
        saveFilePath = Application.persistentDataPath + "/save.dat";
    }

    // 存檔
    public void SaveGame()
    {
        SaveData data = new SaveData();
        // 將遊戲數據儲存到SaveData物件中

        // 使用BinaryFormatter將SaveData物件序列化並寫入檔案
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(saveFilePath);
        formatter.Serialize(file, data);
        file.Close();

        Debug.Log("遊戲已存檔");
    }

    // 讀取存檔
    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(saveFilePath, FileMode.Open);
            SaveData data = (SaveData)formatter.Deserialize(file);
            file.Close();

            // 從SaveData物件中讀取數據，並應用到遊戲中
            // 例如：player.transform.position = data.playerPosition;
            // player.GetComponent<Health>().SetHealth(data.playerHealth);

            Debug.Log("存檔讀取完成");
        }
        else
        {
            Debug.LogWarning("找不到存檔檔案");
        }
    }
}
