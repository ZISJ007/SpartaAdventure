using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKey(KeyCode.F))
        {
            InventoryManager.Instance.AddItem(itemData); // 인벤토리에 아이템 추가
            Destroy(this.gameObject);
        }
    }
}