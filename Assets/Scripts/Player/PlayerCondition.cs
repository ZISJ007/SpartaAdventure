using System;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    private PlayerController playerController;
    Condition health { get { return uiCondition.health; } }

    public event Action onTakeDamage;
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
        health.Add(amount); // 체력 회복
    }
    public void Damaged(float amount)
    {
        health.Subtract(amount); // 체력 감소
    }

    public void Die()
    {
        playerController.IsDie();
    }
}