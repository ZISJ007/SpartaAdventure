using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryMananager : MonoBehaviour
{
    public static InventoryMananager Instance;

    public Transform ItemSlotParent;


    private List<ItemSlot> slots = new List<ItemSlot>();


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    public void AddItem(ItemData newItem)
    {
        ItemSlot exitsSlot = null;

        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] != null && slots[i].HasItem(newItem) == true)
            {
                exitsSlot = slots[i];
                break;
            }
        }

        if (exitsSlot != null)

        {
            exitsSlot.AddCount(1);
        }
        else
        {
            ItemSlot itemslot = GetComponent<ItemSlot>();
            itemslot.SetUp(newItem);
            slots.Add(itemslot);
        }
    }
}