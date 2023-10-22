using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Buttoninfo : MonoBehaviour
{
    public List<ShopItem> shopItems = new List<ShopItem>();
    public PlayerInventory playerInventory;
    public TextMeshProUGUI coinsTxt;

    private int currentItemIndex = 0;

    [System.Serializable]
    public class ShopItem
    {
        public int itemID;
        public string itemName;
        public int exchangeItemID;
        public int exchangeItemQuantity;
    }

    [System.Serializable]
    public class PlayerItem
    {
        public int itemID;
        public string itemName;
        public int quantity;
    }

    [System.Serializable]
    public class PlayerInventory
    {
        public List<PlayerItem> playerItems = new List<PlayerItem>();
    }

    private void Start()
    {
        UpdateCoinsText();
        UpdateQuantityText();

        // 在這裡設置初始物品清單
        playerInventory.playerItems.Add(new PlayerItem { itemID = 1, itemName = "酒瓶", quantity = 6 });
        playerInventory.playerItems.Add(new PlayerItem { itemID = 2, itemName = "肉", quantity = 6 });
        playerInventory.playerItems.Add(new PlayerItem { itemID = 3, itemName = "牙", quantity = 6 });
        playerInventory.playerItems.Add(new PlayerItem { itemID = 4, itemName = "凝膠", quantity = 6 });
        playerInventory.playerItems.Add(new PlayerItem { itemID = 5, itemName = "骨頭", quantity = 6 });
        

        // 添加更多初始物品...

        foreach (ShopItem item in shopItems)
        {
            Debug.Log("商城物品：" + item.itemName + " 的 itemID 是：" + item.itemID);
        }

        foreach (PlayerItem playerItem in playerInventory.playerItems)
        {
            Debug.Log("玩家物品：" + playerItem.itemName + " 的 itemID 是：" + playerItem.itemID);
        }
    }

    public void BuyCurrentItem(string nameItem)
    {
        print($"<color=#f69>玩家按下按鈕，要換的項目：{nameItem}</color>");

        // 確保有足夠的物品進行交換
        bool canExchange = false;
        int exchangeItemID = -1;
        int exchangeQuantity = 0;

        // 根據選擇的名稱來設定交換物品ID和數量
        switch (nameItem)
        {
            case "用一個酒瓶換生命藥水":
                exchangeItemID = 1; // 酒瓶的物品ID
                exchangeQuantity = 1; // 交換1個
                break;

            case "用三個肉換生命藥水":
                exchangeItemID = 2; // 肉的物品ID
                exchangeQuantity = 3; // 交換3個
                break;
            case "用一個酒瓶換空瓶":
                exchangeItemID = 1; // 酒瓶的物品ID
                exchangeQuantity = 1; // 交換1個
                break;

            case "用六個牙換蘑菇彈":
                exchangeItemID = 3; // 牙的物品ID（假設3是牙的物品ID）
                exchangeQuantity = 6; // 交換6個
                break;

            case "用六個凝膠換凝膠彈":
                exchangeItemID = 4; // 凝膠的物品ID（假設4是凝膠的物品ID）
                exchangeQuantity = 6; // 交換6個
                break;

            case "用六個骨頭換燃燒彈":
                exchangeItemID = 5; // 骨頭的物品ID（假設5是骨頭的物品ID）
                exchangeQuantity = 6; // 交換6個
                break;

            // 添加更多物品交換設定...

            default:
                break;
        }

        if (exchangeItemID != -1 && exchangeQuantity > 0)
        {
            // 檢查玩家是否有足夠的物品進行交換
            int playerItemIndex = playerInventory.playerItems.FindIndex(x => x.itemID == exchangeItemID);
            if (playerItemIndex != -1 && playerInventory.playerItems[playerItemIndex].quantity >= exchangeQuantity)
            {
                // 玩家有足夠的物品進行交換
                playerInventory.playerItems[playerItemIndex].quantity -= exchangeQuantity;
                canExchange = true;
            }
        }

        if (canExchange)
        {
            // 完成交換
            UpdateCoinsText();
            Debug.Log($"玩家成功兌換了 {nameItem}");

            currentItemIndex++;

            if (currentItemIndex < shopItems.Count)
            {
                Debug.Log("可以購買下一个物品：" + shopItems[currentItemIndex].itemName);
            }
            else
            {
                Debug.Log("已購買所有物品。");
            }
        }
        else
        {
            Debug.Log("玩家没有足夠的物品進行交换。");
        }
    }

    public int GetPlayerItemCount(int itemID)
    {
        PlayerItem playerItem = playerInventory.playerItems.Find(x => x.itemID == itemID);
        return playerItem != null ? playerItem.quantity : 0;
    }

    private void UpdateCoinsText()
    {
        int totalCoins = GetTotalCoins();
        coinsTxt.text = "Coins: " + totalCoins.ToString();
    }

    private void UpdateQuantityText()
    {
        // 更新商城商品的庫存數量（這裡只是範例，你可以設定實際的庫存數量）
        foreach (ShopItem item in shopItems)
        {
            Debug.Log("商城商品 " + item.itemName + " 的庫存數量：" + GetPlayerItemCount(item.exchangeItemID));
        }
    }

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
