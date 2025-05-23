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
        if (useKey != KeyCode.None && Input.GetKeyDown(useKey)) // 키 입력이 있을 때
            OnUseItem();
    }

    private void UpdateUI()
    {
        if (itemData != null) // 슬롯에 아이템이 있을 때
        {
            Icon.sprite = itemData.Icon;
            ItemName.text = itemData.itemName;
            CountText.text = itemCount > 1 ? itemCount.ToString() : "";// 아이템 개수 표시
        }
        else// 슬롯이 비어있을 때
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

    public void AddCount(int amount) //아이템 개수 추가
    {
        itemCount += amount;
        UpdateUI();
    }

    public bool HasItem(ItemData check) => itemData == check; // 슬롯에 있는 아이템과 비교

    public void OnUseItem()
    {
        if (itemData == null || itemCount <= 0) return; // 슬롯이 비어있거나 아이템 개수가 0일 때 return

        var player = FindObjectOfType<PlayerController>();
        if (player != null)
        {
            switch (itemData.abilityType) // 아이템의 능력 타입에 따라
            {
                case ItemData.itemAbilityType.SpeedUp:// 이동 속도 증가
                    StartCoroutine(SpeedBuff(player));
                    break;
                case ItemData.itemAbilityType.JumpUp:// 점프력 증가
                    StartCoroutine(JumpBuff(player));
                    break;
            }
        }

            itemCount--;
        UpdateUI();

        if (itemCount <= 0)
        {
            // 사용 후 완전 소모 > 슬롯 초기화
            itemData = null;
            itemCount = 0;
            UpdateUI();
        }
    }
    private IEnumerator SpeedBuff(PlayerController player)
    {
        float originalSpeed = player.moveSpeed; // 플레이어의 원래 이동 속도
        player.moveSpeed += originalSpeed + itemData.plusValue; 
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
