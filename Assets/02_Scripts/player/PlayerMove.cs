using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    new Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    Animator animator;
    RaycastHit2D raycastHit2D;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float dir = 0.0f;

        // 키입력 체크
        //dir = Input.GetAxisRaw("Horizontal");
        if (true == Input.GetKey(KeyCode.LeftArrow))
        {
            dir = -1.0f;
            rigidbody2D.AddForce(Vector2.right * dir, ForceMode2D.Impulse);
        }
        if (true == Input.GetKey(KeyCode.RightArrow))
        {
            dir = 1.0f;
            rigidbody2D.AddForce(Vector2.right * dir, ForceMode2D.Impulse);
        }
        if((true == Input.GetMouseButtonDown(0) || true == Input.GetKey(KeyCode.UpArrow)) && false == animator.GetBool("isJumping"))
        {
            //Debug.Log("dir = " + dir);
            rigidbody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }
        //Debug.Log("dir = " + dir);

        // 키 방향에 따른 이미지 반전
        if (dir != 0.0f)
        {
            //transform.localScale = new Vector2(dir, 1);
            spriteRenderer.flipX = (dir == -1.0f ? true : false);
        }

        // 속도 적용
        //rigidbody2D.AddForce(Vector2.right * dir, ForceMode2D.Impulse);
        //rigidbody2D.AddForce(Vector2.right * dir);
        //rigidbody2D.linearVelocityX = dir;

        // 좌우 최대 속도 제어
        if (Mathf.Abs(rigidbody2D.linearVelocityX) >= maxSpeed)
        {
            rigidbody2D.linearVelocityX = dir * maxSpeed;
        }

        //
        //Debug.Log("rigidbody2D.linearVelocity.normalized.x = " + rigidbody2D.linearVelocity.normalized.x);
        Debug.Log("rigidbody2D.linearVelocityX = " + rigidbody2D.linearVelocityX);
        Debug.Log("rigidbody2D.linearVelocityY = " + rigidbody2D.linearVelocityY);

        // 애니메이션
        //if (rigidbody2D.linearVelocity.normalized.x == 0.0f)
        if (Mathf.Abs(rigidbody2D.linearVelocityX) <= 1.0f)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

        // ray, 한번만 체크 됨
        if (rigidbody2D.linearVelocityY <= 0.0f)
        {
            Debug.DrawRay(rigidbody2D.position, Vector3.down, new Color(0, 1, 0));
            raycastHit2D = Physics2D.Raycast(rigidbody2D.position, Vector3.down, 1, LayerMask.GetMask("block"));
            if (raycastHit2D.collider != null)
            {
                if (raycastHit2D.distance <= 0.5f)
                {
                    animator.SetBool("isJumping", false);
                    //Debug.Log("ray name = " + raycastHit2D.collider.name);
                }
            }
        }


        reStart();
    }

    void reStart()
    {
        // 테스트 코드
        if(transform.position.y < -10)
        {
            rigidbody2D.linearVelocity = Vector2.zero;
            transform.position = new Vector2(-4, 3);
        }
    }

    
}
