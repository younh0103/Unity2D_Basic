using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    // Negative left, a : -1
    // Positive right, d : 1
    // Non : 0
    // private float moveSpeed = 5.0f;                 // 이동 속도
    // // Negative down, s : -1
    // // Positive up, w : 1
    // // Non : 0
    // // private Vector3 moveDirection = Vector3.zero;   // 이동 방향
    // private Rigidbody2D rigid2D;

    // private void Awake()
    // {
    //     rigid2D = GetComponent<Rigidbody2D>();
    // }

    // private void Update()
    // {
    //     float X = Input.GetAxisRaw("Horizontal");   // 좌우 이동
    //     float Y = Input.GetAxisRaw("Vertical");     // 위아래 이동

    //     // 이동방향 설정
    //     // moveDirection = new Vector3(X, Y, 0);

    //     // 새로운 위치 = 현재 위치 + (방향 * 속도)
    //     // transform.position += moveDirection * moveSpeed * Time.deltaTime;

    //     // Rigidbody2D 컴포넌트에 있는 속력(velocity) 변수 설정
    //     rigid2D.velocity = new Vector3(X, Y, 0) * moveSpeed;
    // }

    // 실습
    // private float moveSpeed = 5.0f;
    // private Vector3 moveDirection;

    // public void Setup(Vector3 direction)
    // {
    //     moveDirection = direction;
    // }

    // private void Update()
    // {
    //     // 새로운 위치 = 현재 위치 + (방향 * 속도)
    //     transform.position += moveDirection * moveSpeed * Time.deltaTime;
    // }

    [SerializeField]
    private float speed = 5.0f; // 이동속도
    [SerializeField]
    private float jumpForce = 8.0f; // 점프 힘 (클수록 높게 점프)
    private Rigidbody2D rigid2D;
    [HideInInspector]
    public bool isLongJump = false; // 낮은 점프, 높은 점프 체크

    [SerializeField]
    private LayerMask groundLayer;  // 바닥 체크를 위한 충돌 레이어
    private CapsuleCollider2D capsuleCollider2D;  // 오브젝트의 충돌 범위 컴포넌트
    private bool isGrounded;    // 바닥체크(바닥에 닿아있을 때 true
    private Vector3 footPosition;   // 발의 위치

    [SerializeField]
    private int maxJumpCount = 2;   // 땅을 밟기 전까지 할 수 있는 최대 점프 횟수
    private int currentJumpCount = 0;   // 현재 가능한 점프 횟수
 
    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void FixedUpdate()
    {
        // 플레이어 오브젝트의 Collider2D min, center, max 위치정보
        Bounds bounds = capsuleCollider2D.bounds;
        // 플레이어의 발 위치 설정
        footPosition = new Vector2(bounds.center.x, bounds.min.y);
        // 플레이어의 발 위치에 원을 생성하고, 원이 바닥과 닿아았으면 isGrounded =true
        isGrounded = Physics2D.OverlapCircle(footPosition, 0.1f, groundLayer);

        // 플레이어의 발이 땋에 닿아 있고, y축 속도가 0이하이면 점프 횟수 초기화
        // velocity.y <= 0 을 추가하지 않으면 점프키를 누르는 순간에도 초기화가 되어 
        // 최대 점프 횟수를 2로 설정하면 2번까지 점프가 가능하게 됨.
        if(isGrounded == true && rigid2D.velocity.y <= 0)
        {
            currentJumpCount = maxJumpCount;
        }

        // 낮은 점프, 높은 점프 구현을 위한 중력 계수(gravityScale) 조절 (Jump Up 일 때만 적용)
        // 중력 계수가 낮은 if 문은 높은 점프가 되고, 중력 계수가 높은 else 문은 낮은 점프가 됨.
        if(isLongJump && rigid2D.velocity.y > 0)
        {
            rigid2D.gravityScale = 1.0f;
        }
        else
        {
            rigid2D.gravityScale = 2.5f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(footPosition, 0.1f);
    }

    public void Move(float x)
    {
        // x축 이동은 x * speed로, y축 이동은 기존의 속력 값 (현재는 중력)
        rigid2D.velocity = new Vector2(x * speed, rigid2D.velocity.y);
    }

    public void Jump()
    {
        // if(isGrounded == true)
        if(currentJumpCount > 0)
        {
            // jumpForce의 크기만큼, 윗쪽 방향으로 속력 설정
            rigid2D.velocity = Vector2.up * jumpForce;
            // 점프 횟수 1 감소
            currentJumpCount--;
        }
    }

}
