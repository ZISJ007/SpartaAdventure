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

    // 슬롯이 비어있는지 여부 (ItemData가 null이면 빈 슬롯)
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

    // 새 아이템 세팅
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

        var player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            switch (itemData.abilityType)
            {
                case ItemData.itemAbilityType.SpeedUp:
                    StartCoroutine(SpeedBuff(player));
                    break;
                case ItemData.itemAbilityType.JumpUp:
                    StartCoroutine(JumpBuff(player));
                    break;
            }
        }
            // TODO: 실제 아이템 사용 로직
            itemCount--;
        UpdateUI();

        if (itemCount <= 0)
        {
            // 사용 후 완전 소모 → 슬롯 초기화
            itemData = null;
            itemCount = 0;
            UpdateUI();
        }
    }
    private IEnumerator SpeedBuff(PlayerController player)
    {
        float originalSpeed = player.moveSpeed;
        player.moveSpeed += originalSpeed + itemData.plusValue; // 예: 속도 50% 증가
        yield return new WaitForSeconds(3);
        player.moveSpeed = originalSpeed;
    }

    private IEnumerator JumpBuff(PlayerController player)
    {
        float originalJump = player.jumpPower; // 플레이어의 원래 점프 파워
        player.jumpPower += originalJump + itemData.plusValue;
        yield return new WaitForSeconds(3);
        player.jumpPower = originalJump;
    }
}
