using System;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;  // 현재 입력 값
    public float jumpPower;
   

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;  // 최소 시야각
    public float maxXLook;  // 최대 시야각
    private float camCurXRot;
    public float lookSensitivity; // 카메라 민감도

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPoint;   // 땅 체크 위치(발 등)
    [SerializeField] private float groundCheckRadius = 0.2f; // 반경
    [SerializeField] private LayerMask groundLayerMask;     // 땅 레이어

    // 이 속도보다 더 빠르게 아래로 움직일 때만 falling

    private Vector2 mouseDelta;  // 마우스 변화값

    [HideInInspector]
    public bool canLook = true;

    private Rigidbody rigid;
    private Animator animator;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // 물리 연산
    private void FixedUpdate()
    {
        Move();
        IsFalling();
    }

    // 카메라 연산 -> 모든 연산이 끝나고 카메라 움직임
    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    // 입력값 처리
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    // 입력값 처리
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
            animator.SetBool("IsMove",true);  
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
            animator.SetBool("IsMove", false);
        }
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && IsGrounded())
        {
            animator.SetTrigger("IsJump");
            rigid.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }

    private void Move()
    {
        // 현재 입력의 y 값은 z 축(forward, 앞뒤)에 곱한다.
        // 현재 입력의 x 값은 x 축(right, 좌우)에 곱한다.
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;  // 방향에 속력을 곱해준다.
        dir.y = rigid.velocity.y;  // y값은 velocity(변화량)의 y 값을 넣어준다.
        rigid.velocity = dir;  // 연산된 속도를 velocity(변화량)에 넣어준다.
    }

    void CameraLook()
    {
        // 마우스 움직임의 변화량(mouseDelta)중 y(위 아래)값에 민감도를 곱한다.
        // 카메라가 위 아래로 회전하려면 rotation의 x 값에 넣어준다. -> 실습으로 확인
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // 마우스 움직임의 변화량(mouseDelta)중 x(좌우)값에 민감도를 곱한다.
        // 카메라가 좌우로 회전하려면 rotation의 y 값에 넣어준다. -> 실습으로 확인
        // 좌우 회전은 플레이어(transform)를 회전시켜준다.
        // Why? 회전시킨 방향을 기준으로 앞뒤좌우 움직여야하니까.
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    bool IsGrounded()
    {
        if(Physics.CheckSphere(groundCheckPoint.position, groundCheckRadius, groundLayerMask)) return true;
        return false;
    }
    private void IsFalling()
    {
        if(IsGrounded())
        {
            animator.SetBool("IsFalling", false);
            return;
        }
        bool isFalling = rigid.velocity.y < -0.05f;
        animator.SetBool("IsFalling", true);
    }
    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
    public void IsDie()
    {
        animator.SetTrigger("IsDie");
        this.enabled = false; // 스크립트를 비활성화 한다.
    }
}