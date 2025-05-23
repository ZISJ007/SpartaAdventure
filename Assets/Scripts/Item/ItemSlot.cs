using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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
        if (useKey != KeyCode.None && Input.GetKeyDown(useKey)) // Ű �Է��� ���� ��
            OnUseItem();
    }

    private void UpdateUI()
    {
        if (itemData != null) // ���Կ� �������� ���� ��
        {
            Icon.sprite = itemData.Icon;
            ItemName.text = itemData.itemName;
            CountText.text = itemCount > 1 ? itemCount.ToString() : "";// ������ ���� ǥ��
        }
        else// ������ ������� ��
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

    public void AddCount(int amount) //������ ���� �߰�
    {
        itemCount += amount;
        UpdateUI();
    }

    public bool HasItem(ItemData check) => itemData == check; // ���Կ� �ִ� �����۰� ��

    public void OnUseItem()
    {
        if (itemData == null || itemCount <= 0) return; // ������ ����ְų� ������ ������ 0�� �� return

        var player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            switch (itemData.abilityType) // �������� �ɷ� Ÿ�Կ� ����
            {
                case ItemData.itemAbilityType.SpeedUp:// �̵� �ӵ� ����
                    StartCoroutine(SpeedBuff(player));
                    break;
                case ItemData.itemAbilityType.JumpUp:// ������ ����
                    StartCoroutine(JumpBuff(player));
                    break;
            }
        }

            itemCount--;
        UpdateUI();

        if (itemCount <= 0)
        {
            // ��� �� ���� �Ҹ� > ���� �ʱ�ȭ
            itemData = null;
            itemCount = 0;
            UpdateUI();
        }
    }
    private IEnumerator SpeedBuff(PlayerController player)
    {
        float originalSpeed = player.moveSpeed; // �÷��̾��� ���� �̵� �ӵ�
        player.moveSpeed += originalSpeed + itemData.plusValue; 
        yield return new WaitForSeconds(3);
        player.moveSpeed = originalSpeed;
    }

    private IEnumerator JumpBuff(PlayerController player)
    {
        float originalJump = player.jumpPower; // �÷��̾��� ���� ���� �Ŀ�
        player.jumpPower += originalJump + itemData.plusValue;
        yield return new WaitForSeconds(3);
        player.jumpPower = originalJump;
    }
}
