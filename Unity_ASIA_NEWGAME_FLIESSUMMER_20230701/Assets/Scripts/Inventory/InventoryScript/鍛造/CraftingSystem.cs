using GLORY;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public Inventory playerInventory; // 玩家的背包
    public List<CraftingRecipe> craftingRecipes;
}// 可用的合成规则

   /* public void CraftItem(Item item1, Item item2, Item item3)
    {
        CraftingRecipe matchingRecipe = GetMatchingRecipe(item1, item2, item3);

        if (matchingRecipe != null)
        {
            if (CheckInventoryForMaterials(matchingRecipe, item1, item2, item3))
            {
                RemoveMaterialsFromInventory(item1, item2, item3);

                PerformCrafting(matchingRecipe);

                UpdateUI();
            }
            else
            {
                Debug.Log("玩家没有足够的原材料。");
            }
        }
        else
        {
            Debug.Log("无法合成这些物品。");
        }
    }

    private CraftingRecipe GetMatchingRecipe(Item item1, Item item2, Item item3)
    {
        foreach (CraftingRecipe recipe in craftingRecipes)
        {
            // 检查是否有匹配的合成规则
            if (recipe.ingredient1 == item1 && recipe.ingredient2 == item2 && recipe.ingredient3 == item3)
            {
                return recipe;
            }
            else if (recipe.ingredient1 == item2 && recipe.ingredient2 == item1 && recipe.ingredient3 == item3)
            {
                return recipe;
            }
            // 添加其他可能的匹配条件
        }

        return null; // 如果没有找到匹配的合成规则
    }
}

   /* private bool CheckInventoryForMaterials(CraftingRecipe recipe, Item item1, Item item2, Item item3)
    {
        // 检查玩家是否拥有足够的原材料
        if (item3 == null)
        {
            return playerInventory.Contains(item1) && playerInventory.Contains(item2);
        }
        else
        {
            return playerInventory.Contains(item1) && playerInventory.Contains(item2) && playerInventory.Contains(item3);
        }
    }

    private void RemoveMaterialsFromInventory(Item item1, Item item2, Item item3)
    {
        // 从背包中移除原材料
        playerInventory.RemoveItem(item1);
        playerInventory.RemoveItem(item2);

        if (item3 != null)
        {
            playerInventory.RemoveItem(item3);
        }
    }

    private void PerformCrafting(CraftingRecipe recipe)
    {
        // 根据合成结果的类型执行不同的操作
        switch (recipe.result)
        {
            case HealthPotion _:
                playerInventory.AddItem(new HealthPotion()); // 假设 HealthPotion 是一个合成的结果类型
                break;
            case Firebomb _:
                playerInventory.AddItem(new Firebomb());
                break;
            case GelBomb _:
                playerInventory.AddItem(new GelBomb());
                break;
                // 添加其他合成结果的处理
        }
    }

    private void UpdateUI()
    {
        // 在这里更新你的UI，例如刷新玩家背包的显示
        // 你可以使用Unity的UI系统或其他UI框架来实现这一点
    }
}*/