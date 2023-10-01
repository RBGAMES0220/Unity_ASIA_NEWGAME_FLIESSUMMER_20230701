using GLORY;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GLORY
{
    public class InventoryManager : MonoBehaviour
    {
        static InventoryManager instance;

        public Inventory myBag;
        public GameObject slotGrid;
        // public Slot slotPrefab;
        public GameObject emptySlot;
        public Text itemInformation;
        public PlayerHealth playerHealth;

        public List<GameObject> slots = new List<GameObject>();



        void Awake()
        {
            if (instance != null)
                Destroy(this);
            instance = this;
        }



        private void OnEnable()
        {
            RefreshItem();
            instance.itemInformation.text = "";
        }

        public void UseSelectedItem()
        {
            if (myBag.itemList.Count > 0)
            {
                // 假設選擇的物品是背包中的第一個物品
                Item selectedItem = myBag.itemList[0];

                if (selectedItem != null)
                {
                    // 檢查物品是否可用
                    if (selectedItem.isUsable)
                    {
                        // 在這裡執行使用物品的操作，例如增加玩家的生命值
                        // 假設 playerHealth 是玩家的生命值管理器
                        playerHealth.Heal(selectedItem.value);

                        // 從背包中刪除該物品
                        myBag.itemList.Remove(selectedItem);

                        // 更新 UI 以反映背包中的變化
                        RefreshItem();
                    }
                }
            }
        }
        public static void UpdateItemInfo(string itemDescription)
        {
            instance.itemInformation.text = itemDescription;
        }


        /* public static void CreateNewItem(Item item)
         {
             Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
             newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
             newItem.slotItem = item;
             newItem.slotImage.sprite = item.itemImage;
             newItem.slotNum.text = item.itemHeld.ToString();
         }*/

        public static void RefreshItem()
        {
            for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
            {
                if (instance.slotGrid.transform.childCount == 0)
                    break;
                Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
                instance.slots.Clear();
            }

            for (int i = 0; i < instance.myBag.itemList.Count; i++)
            {
                //CreateNewItem(instance.myBag.itemList[i]);
                instance.slots.Add(Instantiate(instance.emptySlot));
                instance.slots[i].transform.SetParent(instance.slotGrid.transform);
                instance.slots[i].GetComponent<Slot>().slotID = i;
                instance.slots[i].GetComponent<Slot>().SetupSlot(instance.myBag.itemList[i]);
            }

        }
    }
}





