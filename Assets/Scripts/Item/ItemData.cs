using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public enum itemAbilityType
    {
        SpeedUp,
        JumpUp,
    }
    public string itemName;
    public string itemDescription; // 아이템 설명
    public Sprite Icon;
    public int plusValue; // 아이템 효과 수치
    internal readonly itemAbilityType abilityType; // 아이템 효과 종류
}