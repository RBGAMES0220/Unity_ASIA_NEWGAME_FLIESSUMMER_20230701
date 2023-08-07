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

        [System.Serializable]
        public class ShopItem
        {
            public int itemID; // 商品ID
            public string itemName; // 商品名稱
            public int exchangeItemID; // 玩家需擁有的物品ID，用於交換該商城商品
            public int exchangeItemQuantity; // 需要的玩家物品數量
            public int itemQuantity; // 商城商品數量
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
        }

        public void Buy(ShopItem item)
        {
            PlayerItem playerItem = playerInventory.playerItems.Find(x => x.itemID == item.exchangeItemID);
            if (playerItem != null && playerItem.quantity >= item.exchangeItemQuantity && item.itemQuantity > 0)
            {
                // 玩家有足夠的物品進行交換，且商城商品數量大於 0
                playerItem.quantity -= item.exchangeItemQuantity;
                item.itemQuantity--; // 減少商城商品數量
                UpdateCoinsText(); // 更新顯示玩家擁有的貨幣數量的文字
                UpdateQuantityText(); // 更新顯示商城商品數量與玩家物品數量的文字
                Debug.Log("玩家成功兌換了 " + item.itemName);
            }
            else
            {
                // 玩家沒有足夠的物品進行交換或商城商品數量不足
                Debug.Log("玩家沒有足夠的物品進行交換或商城商品數量不足。");
            }
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
                    item.quantityText.text = "x" + item.itemQuantity.ToString();
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

        // 按鈕點擊事件處理
        public void OnButtonClick()
        {
            // 假設這個按鈕代表商城中的第一個
            if (shopItems.Count > 0)
            {
                Buy(shopItems[0]);
            }
        }
    }
}
