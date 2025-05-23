using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// �������� ���Կ� �ݿ����ش� 
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
    [Tooltip("�� ������ ����� Ű�� �����ϼ���. (��: Alpha1, Alpha2, Alpha3)")]
    public KeyCode useKey = KeyCode.None;

    private void Update()
    {
        // ������ Ű�� ������ ���� ������ ���
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

    // ������ �������̸� ������ �߰�
    public void AddCount(int amount)
    {
        itemCount += amount;
        UpdateUI();
    }

    public bool HasItem(ItemData checkItem)
    {
        return itemData == checkItem;
    }

    // ���� Ŭ�� Ȥ�� Ű�� ������ �� ȣ��
    public void OnclickItem()
    {
        if (itemData == null || itemCount <= 0)
            return;

        // TODO: ���� ������ ��� ����

        // ��� �� ī��Ʈ ����
        itemCount--;
        UpdateUI();

        if (itemCount <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
