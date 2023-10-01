using UnityEngine;
using UnityEngine.EventSystems;
using GLORY;

public class ItemUsageOnClick : MonoBehaviour, IPointerClickHandler
{
    public InventoryManager inventoryManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) // 檢查是否是滑鼠左鍵單擊
        {
            // 呼叫 InventoryManager 中的 UseSelectedItem 方法
            inventoryManager.UseSelectedItem();
        }
    }
}
