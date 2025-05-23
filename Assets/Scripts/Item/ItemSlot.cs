// ItemSlot.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [Header("UI References")]
    public Image Icon;
    public TextMeshProUGUI CountText;
    public TextMeshProUGUI ItemName;

    [Header("Item Data (private)")]
    private ItemData itemData;
    private int itemCount = 0;

    [Header("Use Key")]
    public KeyCode useKey;

    // ������ ����ִ��� ���� (ItemData�� null�̸� �� ����)
    public bool IsEmpty => itemData == null;

    private void Update()
    {
        if (useKey != KeyCode.None && Input.GetKeyDown(useKey))
            OnUseItem();
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

    // �� ������ ����
    public void SetUp(ItemData newData, int startCount = 1)
    {
        itemData = newData;
        itemCount = startCount;
        UpdateUI();
    }

    public void AddCount(int amount)
    {
        itemCount += amount;
        UpdateUI();
    }

    public bool HasItem(ItemData check) => itemData == check;

    public void OnUseItem()
    {
        if (itemData == null || itemCount <= 0) return;

        // TODO: ���� ������ ��� ����

        itemCount--;
        UpdateUI();

        if (itemCount <= 0)
        {
            // ��� �� ���� �Ҹ� �� ���� �ʱ�ȭ
            itemData = null;
            itemCount = 0;
            UpdateUI();
        }
    }
}
