using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GLORY
{
    public class CraftingSystem : MonoBehaviour
    {
        // 物品列表，包含所有可能的物品
        public List<Items> items = new List<Items>();

        // 可製作物品的列表
        public List<Items> craftableItems = new List<Items>();

        // 是否正在進行合成
        public bool isCrafting;

        // 當前合成的 ID
        public string currentCraftID;

        // 合成所需的輸入欄位
        public List<InputField> craftInputs = new List<InputField>();

        // 顯示合成物品的圖片
        public List<Image> cratImages = new List<Image>();

        // 顯示最終合成結果的圖片
        public Image resultImage;

        // 空物品槽的圖片
        public Sprite emptySlot;

        // 根據 ID 取得物品的方法
        Items FetchItemByID(int _id)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == _id)
                {
                    return items[i];
                }
            }
            return null;
        }

        // 根據合成 ID 取得可合成的物品的方法
        Items FetchCraftItem(string _id)
        {
            for (int i = 0; i < craftableItems.Count; i++)
            {
                for (int j = 0; j < craftableItems[i].craftID.Count; j++)
                {
                    if (craftableItems[i].craftID[j] == _id)
                    {
                        return craftableItems[i];
                    }
                }
            }
            return null;
        }

        // 建立可合成物品列表的方法
        void ConstructCraftItems()
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].craftable)
                {
                    craftableItems.Add(items[i]);
                }
            }
        }

        // 遊戲啟動時執行的方法
        void Start()
        {
            ConstructCraftItems();
            Debug.Log(FetchItemByID(1).name);
            Debug.Log(FetchCraftItem("123").name);
        }

        // 遊戲每一幀更新時執行的方法
        void Update()
        {
            if (isCrafting)
            {
                Craft();
            }
        }

        // 執行合成的方法
        void Craft()
        {
            currentCraftID = "";

            // 使用迴圈處理所有的 craftInputs
            for (int i = 0; i < craftInputs.Count; i++)
            {
                // 檢查 cratImages 和 craftInputs 的索引是否有效
                if (i < cratImages.Count && i < craftInputs.Count)
                {
                    if (craftInputs[i].text != "")
                    {
                        currentCraftID += craftInputs[i].text;

                        // 檢查 cratImages 列表的索引是否在範圍內
                        if (i < cratImages.Count)
                        {
                            cratImages[i].sprite = FetchItemByID(int.Parse(craftInputs[i].text)).sprite;
                        }
                        else
                        {
                            Debug.LogError("Index out of range for cratImages: " + i);
                        }
                    }
                    else
                    {
                        currentCraftID += "";
                        cratImages[i].sprite = emptySlot;
                    }
                }
            }

            Items result = FetchCraftItem(currentCraftID);
            if (result != null)
            {
                resultImage.sprite = result.sprite;
            }
            else
            {
                resultImage.sprite = emptySlot;
            }
        }
    }

    // 物品類別，包含名稱、ID、圖片、是否可合成、合成所需的 ID
    [System.Serializable]
    public class Items
    {
        public string name;
        public int ID;
        public Sprite sprite;
        public bool craftable;
        public List<string> craftID; // 修正這裡的 typo
    }
}
