using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMove : MonoBehaviour
{
    public float maxSpeed;
    new Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    Animator animator;

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

        // Ű�Է� üũ
        //dir = Input.GetAxisRaw("Horizontal");
        if (true == Input.GetKey(KeyCode.LeftArrow))
        {
            dir = -1.0f;
        }
        else if (true == Input.GetKey(KeyCode.RightArrow))
        {
            dir = 1.0f;            
        }
        //Debug.Log("dir = " + dir);

        // Ű ���⿡ ���� �̹��� ����
        if (dir != 0.0f)
        {
            //transform.localScale = new Vector2(dir, 1);
            spriteRenderer.flipX = (dir == -1.0f ? true : false);
        }

        // �ӵ� ����
        rigidbody2D.AddForce(Vector2.right * dir, ForceMode2D.Impulse);
        //rigidbody2D.AddForce(Vector2.right * dir);
        //rigidbody2D.linearVelocityX = dir;

        // �ִ� �ӵ� ����
        if (Mathf.Abs(rigidbody2D.linearVelocityX) >= maxSpeed)
        {
            rigidbody2D.linearVelocityX = dir * maxSpeed;
        }

        //
        Debug.Log("rigidbody2D.linearVelocity.normalized.x = " + rigidbody2D.linearVelocity.normalized.x);
        Debug.Log("rigidbody2D.linearVelocityX = " + rigidbody2D.linearVelocityX);
        //if (rigidbody2D.linearVelocity.normalized.x == 0.0f)
        if (Mathf.Abs(rigidbody2D.linearVelocityX) <= 1.0f)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

        reStart();
    }

    void reStart()
    {
        if(transform.position.y < -10)
        {
            rigidbody2D.linearVelocity = Vector2.zero;
            transform.position = new Vector2(-4, 3);
        }
    }
}
