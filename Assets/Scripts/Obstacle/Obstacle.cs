using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private PlayerCondition playerCondition;
    private void Awake()
    {
        playerCondition = FindObjectOfType<PlayerCondition>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // �÷��̾ ��ֹ��� �浹���� �� �÷��̾��� ü���� ���ҽ�Ŵ
            playerCondition.Damaged(30f);
            Debug.Log("�÷��̾ ��ֹ��� ��Ҵ�.");
        }
    }
}
