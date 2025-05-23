
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public Transform itemSlotParent;

    // 씬에 붙어 있는 ItemSlot 컴포넌트 3개
    private List<ItemSlot> slots;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        slots = new List<ItemSlot>(itemSlotParent.GetComponentsInChildren<ItemSlot>());

    }

    public void AddItem(ItemData newItem)
    {
        // 이미 같은 아이템이 있는 슬롯 찾기
        var exitSlot = slots.Find(s => !s.IsEmpty && s.HasItem(newItem));
        if (exitSlot != null)
        {
            exitSlot.AddCount(1);
            return;
        }

        // 빈 슬롯 찾기
        var empty = slots.Find(s => s.IsEmpty);
        if (empty != null)
        {
            empty.SetUp(newItem, 1);
            return;
        }

        //빈 슬롯이 없으면 경고
        Debug.LogWarning("인벤토리 슬롯이 가득 찼습니다!");
    }
}
