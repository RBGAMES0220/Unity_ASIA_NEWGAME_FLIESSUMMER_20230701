using UnityEngine;
using UnityEngine.Events;

namespace Justin
{
    /// <summary>
    /// 互動系統
    /// </summary>
    public class InteractableSystem : MonoBehaviour
    {
        [SerializeField, Header("啟動道具")]
        private GameObject propActive;
        [SerializeField, Header("啟動後的事件")]
        private UnityEvent onInteract;

        private string nameTarget = "PlayerCapsule";

        private void OnTriggerEnter(Collider other)
        {
            if (other.name.Contains(nameTarget))
            {
                print(other.name);

                if (propActive == null || propActive.activeInHierarchy)
                {
                    // 如果 propActive 為 null 或啟用狀態，直接觸發事件
                    onInteract.Invoke();
                }
            }
        }

        // 碰撞持續進入事件
        private void OnTriggerStay(Collider other)
        {
            // 可在此實現持續觸發的相關邏輯
        }

        // 碰撞離開事件
        private void OnTriggerExit(Collider other)
        {
            // 可在此實現碰撞離開時的相關邏輯
        }

        // 隱藏物件的方法
        public void HiddenObject()
        {
            // 將此 GameObject 設置為非啟用狀態，隱藏物件
            gameObject.SetActive(false);
        }
    }
}
