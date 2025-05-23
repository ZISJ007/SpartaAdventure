using System;
using UnityEngine;

// UI�� ������ �� �ִ� PlayerCondition
// �ܺο��� �ɷ�ġ ���� ����� �̰��� ���ؼ� ȣ��. ���������� UI ������Ʈ ����.
public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    private PlayerController playerController;
    Condition health { get { return uiCondition.health; } }

    public event Action onTakeDamage;   // Damage ���� �� ȣ���� Action (6�� ������ ȿ�� �� ���)
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