﻿using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace GLORY
{
    public class ItemOnWorld : MonoBehaviour
    {
        public Item thisItem;
        public Inventory playerInventory;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                AddNewItem();
                Destroy(gameObject);
            }
        }

        public void AddNewItem()
        {
            if (!playerInventory.itemList.Contains(thisItem))
            {
                // playerInventory.itemList.Add(thisItem);
                // InventoryManager.CreateNewItem(thisItem);
                for (int i = 0; i < playerInventory.itemList.Count; i++)
                {
                  if (playerInventory.itemList[i] == null)
                    {
                        playerInventory.itemList[i] = thisItem;
                        break;
                    }
                }
                
                   
                     
                    
                 
             }
                 
            
            else
            {
                thisItem.itemHeld += 1;
            }

           
            InventoryManager.RefreshItem(); 
        }
    }
}
