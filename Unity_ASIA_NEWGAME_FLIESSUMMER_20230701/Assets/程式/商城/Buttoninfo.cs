using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Justin
{
    public class Buttoninfo : MonoBehaviour
    {
        public List<ShopItem> shopItems = new List<ShopItem>(); // 商城商品資訊清單
        public PlayerInventory playerInventory; // 玩家物品清單
        public TextMeshProUGUI coinsTxt; // 顯示玩家擁有的貨幣數量的文字
        private int currentItemIndex = 0; // 当前购买的物品索引

        [System.Serializable]
        public class ShopItem
        {
            public int itemID; // 商品ID
            public string itemName; // 商品名稱
            public int exchangeItemID; // 玩家需擁有的物品ID，用於交換該商城商品
            public int exchangeItemQuantity; // 需要的玩家物品數量
            public TextMeshProUGUI quantityText; // 顯示商城商品數量的 UI Text
        }

        [System.Serializable]
        public class PlayerItem
        {
            public int itemID; // 物品ID
            public string itemName; // 物品名稱
            public int quantity; // 物品數量
        }

        [System.Serializable]
        public class PlayerInventory
        {
            public List<PlayerItem> playerItems = new List<PlayerItem>(); // 玩家物品清單的列表
        }

        private void Start()
        {
            UpdateCoinsText(); // 更新顯示玩家擁有的貨幣數量的文字
            UpdateQuantityText(); // 更新顯示商城商品數量與玩家物品數量的文字
            foreach (ShopItem item in shopItems)
            {
                Debug.Log("商城物品：" + item.itemName + " 的 itemID 是：" + item.itemID);
            }

            foreach (PlayerItem playerItem in playerInventory.playerItems)
            {
                Debug.Log("玩家物品：" + playerItem.itemName + " 的 itemID 是：" + playerItem.itemID);
            }
        }

        /// <summary>
        /// 購買道具
        /// 1. 用一個酒瓶換生命藥水
        /// 2. 用三個肉換生命藥水
        /// 3. 用一個酒瓶換空瓶
        /// 
        /// </summary>
        /// <param name="nameItem">玩家要購買的道具</param>
        public void BuyCurrentItem(string nameItem)
        {
            print($"<color=#f69>玩家按下按鈕，要換的項目：{nameItem}</color>");

            if (nameItem == "用一個酒瓶換生命藥水")
            {

            }
            else if (nameItem == "用三個肉換生命藥水")
            {

            }
            else if (nameItem == "用一個酒瓶換空瓶")
            {

            }


            /*
            if (currentItemIndex >= 0 && currentItemIndex < shopItems.Count)
            {
                ShopItem item = shopItems[currentItemIndex];
                PlayerItem playerItem = playerInventory.playerItems.Find(x => x.itemID == item.exchangeItemID);

                if (playerItem != null && playerItem.quantity >= item.exchangeItemQuantity)
                {
                    // 玩家有足够的物品进行交换
                    playerItem.quantity -= item.exchangeItemQuantity;
                    UpdateCoinsText(); // 更新顯示玩家擁有的貨幣數量的文字
                    Debug.Log("玩家成功兌換了 " + item.itemName);

                    // 增加当前购买的物品索引，以便购买下一个物品
                    currentItemIndex++;

                    // 检查是否还有更多物品可以购买
                    if (currentItemIndex < shopItems.Count)
                    {
                        Debug.Log("可以购买下一个物品：" + shopItems[currentItemIndex].itemName);
                    }
                    else
                    {
                        Debug.Log("已购买所有物品。");
                    }
                }
                else
                {
                    // 玩家没有足够的物品进行交换
                    Debug.Log("玩家没有足够的物品进行交换。");
                }
            */
        }

        // 從玩家物品清單中取得指定物品的數量
        public int GetPlayerItemCount(int itemID)
        {
            PlayerItem playerItem = playerInventory.playerItems.Find(x => x.itemID == itemID);
            return playerItem != null ? playerItem.quantity : 0;
        }

        private void UpdateCoinsText()
        {
            // 更新顯示玩家擁有的貨幣數量的文字
            coinsTxt.text = "Coins: " + GetTotalCoins().ToString();
        }

        private void UpdateQuantityText()
        {
            // 更新顯示商城商品數量的文字
            foreach (ShopItem item in shopItems)
            {
                if (item.quantityText != null)
                {
                    // 在這裡不再顯示商城商品的庫存數量
                    item.quantityText.text = "";
                }
            }
        }

        // 取得玩家目前的總硬幣數量（這裡只是將所有物品數量相加，模擬使用硬幣）
        private int GetTotalCoins()
        {
            int totalCoins = 0;
            foreach (PlayerItem playerItem in playerInventory.playerItems)
            {
                totalCoins += playerItem.quantity;
            }
            return totalCoins;
        }
    }
}
