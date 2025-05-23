using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// 아이템을 슬롯에 반영해준다 
public class ItemSlot : MonoBehaviour
{
    [Header("UI References")]
    public Image Icon;
    public TMPro.TextMeshProUGUI CountText;
    public TMPro.TextMeshProUGUI ItemName;

    [Header("Item Data")]
    private ItemData itemData;
    private int itemCount = 1;

    [Header("Use Key")]
    [Tooltip("이 슬롯을 사용할 키를 지정하세요. (예: Alpha1, Alpha2, Alpha3)")]
    public KeyCode useKey = KeyCode.None;

    private void Update()
    {
        // 지정된 키를 누르면 슬롯 아이템 사용
        if (useKey != KeyCode.None && Input.GetKeyDown(useKey))
        {
            OnclickItem();
        }
    }

    private void UpdateUI()
    {
        if (itemData != null)
        {
            Icon.sprite = itemData.Icon;
            ItemName.text = itemData.itemName;
            CountText.text = itemCount > 1 ? itemCount.ToString() : "";
        }
        else
        {
            Icon.sprite = null;
            ItemName.text = "";
            CountText.text = "";
        }
    }

    public void SetUp(ItemData setItemData)
    {
        itemData = setItemData;
        itemCount = 1;
        UpdateUI();
    }

    // 동일한 아이템이면 개수만 추가
    public void AddCount(int amount)
    {
        itemCount += amount;
        UpdateUI();
    }

    public bool HasItem(ItemData checkItem)
    {
        return itemData == checkItem;
    }

    // 슬롯 클릭 혹은 키를 눌렀을 때 호출
    public void OnclickItem()
    {
        if (itemData == null || itemCount <= 0)
            return;

        // TODO: 실제 아이템 사용 로직

        // 사용 후 카운트 차감
        itemCount--;
        UpdateUI();

        if (itemCount <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
