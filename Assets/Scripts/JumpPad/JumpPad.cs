using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [Header("점프 패드 재활성화 시간")]
    public float jumpDelay = 1.0f;
    [Header("점프 패드 파워")]
    public Vector3 jumpForce = new Vector3(0, 10, 0);

    private bool isReady = true;
    private Collider jumpPadCol;
    private void Awake()
    {
        jumpPadCol = GetComponent<Collider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        // 점프 패드가 재활성화 되지 않았거나 충돌한 오브젝트가 플레이어가 아닐 경우
        if (!isReady || !collision.gameObject.CompareTag("Player")) return; 

        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(jumpForce, ForceMode.Impulse);// 점프 패드에 충돌한 플레이어에게 힘을 가함
                StartCoroutine(JumpPadDelayCoroutine());
            }
        }
    }
    private IEnumerator JumpPadDelayCoroutine()
    {
        isReady = false; // 점프 패드 비활성화
        jumpPadCol.enabled = false; // 점프 패드의 콜라이더 비활성화
        yield return new WaitForSeconds(jumpDelay);
        jumpPadCol.enabled = true;
        isReady = true;
    }
}