using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GLORY
{
    [System.Serializable]
    public class CraftingRecipe
    {
        public Item ingredient1; // 第一個原材料
        public Item ingredient2; // 第二個原材料
        public Item result;     // 合成結果
    }
}
