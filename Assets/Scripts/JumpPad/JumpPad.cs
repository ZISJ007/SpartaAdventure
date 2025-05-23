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
        if (!isReady || !collision.gameObject.CompareTag("Player")) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(jumpForce, ForceMode.Impulse);
                StartCoroutine(JumpPadDelayCoroutine());
            }
        }
    }
    private IEnumerator JumpPadDelayCoroutine()
    {
        isReady = false;
        jumpPadCol.enabled = false;
        yield return new WaitForSeconds(jumpDelay);
        jumpPadCol.enabled = true;
        isReady = true;
    }
}