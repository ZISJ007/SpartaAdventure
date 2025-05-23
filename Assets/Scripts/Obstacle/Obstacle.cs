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
            // 플레이어가 장애물에 충돌했을 때 플레이어의 체력을 감소시킴
            playerCondition.Damaged(30f);
            Debug.Log("플레이어가 장애물에 닿았다.");
        }
    }
}
