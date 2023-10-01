using UnityEngine;
using UnityEngine.EventSystems;
using GLORY;

public class ItemUsageOnClick : MonoBehaviour, IPointerClickHandler
{
    public InventoryManager inventoryManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) // �ˬd�O�_�O�ƹ��������
        {
            // �I�s InventoryManager ���� UseSelectedItem ��k
            inventoryManager.UseSelectedItem();
        }
    }
}
