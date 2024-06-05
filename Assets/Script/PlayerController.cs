using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // [SerializeField]
    // private KeyCode keyCodeFire = KeyCode.Space;
    // [SerializeField]
    // private GameObject bulletPrefab;
    // private float moveSpeed = 3.0f;
    // private Vector3 lastMoveDirection = Vector3.right;

    // private void Update()
    // {
    //     // 플레이어 오브젝트 이동
    //     float x = Input.GetAxisRaw("Horizontal");
    //     float y = Input.GetAxisRaw("Vertical");

    //     transform.position += new Vector3(x, y, 0) * moveSpeed * Time.deltaTime;

    //     // 마지막에 입력된 방향키의 방향을 총알의 발사 방향으로 활용
    //     if(x != 0 || y != 0)
    //     {
    //         lastMoveDirection = new Vector3(x, y, 0);
    //     }

    //     // 플레이어 오브젝트 총알 발사
    //     if(Input.GetKeyDown(keyCodeFire))
    //     {
    //         GameObject clone = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

    //         clone.name = "Bullet";
    //         clone.transform.localScale = Vector3.one * 0.5f;
    //         clone.GetComponent<SpriteRenderer>().color = Color.red;

    //         clone.GetComponent<Movement2D>().Setup(lastMoveDirection);
    //     }
    // }

    // private Movement2D movement2D;

    // private void Awake()
    // {
    //     movement2D = GetComponent<Movement2D>();
    // }

    // private void Update()
    // {
    //     // left or a = -1 / right or d = 1
    //     float x = Input.GetAxisRaw("Horizontal");
    //     // 좌우 이동 방향 제어
    //     movement2D.Move(x);

    //     // 플레이어 점프 (스페이스 키를 누르면 점프)
    //     if(Input.GetKeyDown(KeyCode.Space))
    //     {
    //         movement2D.Jump();
    //     }

    //     // 스페이스 키를 누르고 있으면 isLongJump = true
    //     if(Input.GetKey(KeyCode.Space))
    //     {
    //         movement2D.isLongJump = true;
    //     }
    //     // 스페이스 키를 떼면 isLongJump = false;
    //     else if(Input.GetKeyUp(KeyCode.Space))
    //     {
    //         movement2D.isLongJump = false;
    //     }

    // }

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("IsDie");
        }
    }

    public void OnDieEvent()
    {
        Debug.Log("End of Die Animation");
    }
}
