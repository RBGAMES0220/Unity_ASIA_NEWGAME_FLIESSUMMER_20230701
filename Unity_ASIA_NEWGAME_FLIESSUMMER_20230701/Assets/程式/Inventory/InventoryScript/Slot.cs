using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace GLORY
{
    public class Slot : MonoBehaviour
    {
        public Item slotItem;
        public Image slotImage;
        public Text slotNum;


        public void ItemOnClicked()
        {

            InventoryManager.UpdateItemInfo(slotItem.itemInfo);
        }
    }
}
