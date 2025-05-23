using System;
using UnityEngine;

// UI를 참조할 수 있는 PlayerCondition
// 외부에서 능력치 변경 기능은 이곳을 통해서 호출. 내부적으로 UI 업데이트 수행.
public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    private PlayerController playerController;
    Condition health { get { return uiCondition.health; } }

    public event Action onTakeDamage;   // Damage 받을 때 호출할 Action (6강 데미지 효과 때 사용)
    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        if (health.curValue == 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }
    public void Damaged(float amount)
    {
        health.Subtract(amount);
    }

    public void Die()
    {
        playerController.IsDie();
    }
}