using System;
using UnityEngine;
using UnityEngine.Accessibility;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;  // ���� �Է� ��
    public float jumpPower;
   

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;  // �ּ� �þ߰�
    public float maxXLook;  // �ִ� �þ߰�
    private float camCurXRot;
    public float lookSensitivity; // ī�޶� �ΰ���

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheckPoint;   // �� üũ ��ġ(�� ��)
    [SerializeField] private float groundCheckRadius = 0.2f; // �ݰ�
    [SerializeField] private LayerMask groundLayerMask;     // �� ���̾�

    // �� �ӵ����� �� ������ �Ʒ��� ������ ���� falling

    private Vector2 mouseDelta;  // ���콺 ��ȭ��

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

    // ���� ����
    private void FixedUpdate()
    {
        Move();
        IsFalling();
    }

    // ī�޶� ���� -> ��� ������ ������ ī�޶� ������
    private void LateUpdate()
    {
        if (canLook)
        {
            CameraLook();
        }
    }

    // �Է°� ó��
    public void OnLookInput(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    // �Է°� ó��
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
        // ���� �Է��� y ���� z ��(forward, �յ�)�� ���Ѵ�.
        // ���� �Է��� x ���� x ��(right, �¿�)�� ���Ѵ�.
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;  // ���⿡ �ӷ��� �����ش�.
        dir.y = rigid.velocity.y;  // y���� velocity(��ȭ��)�� y ���� �־��ش�.
        rigid.velocity = dir;  // ����� �ӵ��� velocity(��ȭ��)�� �־��ش�.
    }

    void CameraLook()
    {
        // ���콺 �������� ��ȭ��(mouseDelta)�� y(�� �Ʒ�)���� �ΰ����� ���Ѵ�.
        // ī�޶� �� �Ʒ��� ȸ���Ϸ��� rotation�� x ���� �־��ش�. -> �ǽ����� Ȯ��
        camCurXRot += mouseDelta.y * lookSensitivity;
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);

        // ���콺 �������� ��ȭ��(mouseDelta)�� x(�¿�)���� �ΰ����� ���Ѵ�.
        // ī�޶� �¿�� ȸ���Ϸ��� rotation�� y ���� �־��ش�. -> �ǽ����� Ȯ��
        // �¿� ȸ���� �÷��̾�(transform)�� ȸ�������ش�.
        // Why? ȸ����Ų ������ �������� �յ��¿� ���������ϴϱ�.
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
        this.enabled = false; // ��ũ��Ʈ�� ��Ȱ��ȭ �Ѵ�.
    }
}