using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    [SerializeField] Rigidbody2D player;
    private bool isGrounded;
    private bool isAllowedToDoubleJump;
    private int attackCount;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] PointerListener[] buttons;
    private enum Buttons{Left, Right, Attack, HAttack, Block, Jump};
    private Animator anim;
    private int jumpCount = 0;
    private SpriteRenderer render;
    // Start is called before the first frame update

    private float waitTime = 0.2f;
    [SerializeField] float attackWait = 1.26f;
    private float attackTime = 0.0f;
    private bool jumpButtonPressed = false;
    private bool highAttack = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
        attackCount = 0;
    }

    // Update is called once per frame
    public void Attack()
    {

    } 
    public void Print()
    {
        Debug.Log("Hello!\n");
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);
        if (isGrounded)
        {
            isAllowedToDoubleJump = true;
        }
        
        int direction = 0;
        if (buttons[(int)Buttons.Left].IsPressed)
            direction = -1;
        if (buttons[(int)Buttons.Right].IsPressed)
            direction = 1;
        if (buttons[(int)Buttons.Left].IsPressed && buttons[(int)Buttons.Right].IsPressed)
            direction = 0;
        player.velocity = new Vector2(moveSpeed * direction, player.velocity.y);
       
        if (!buttons[(int)Buttons.Jump].IsPressed)
            jumpButtonPressed = false;
       
        if (buttons[(int)Buttons.Jump].IsPressed)
        {   
            if (isGrounded)
            {
                player.velocity = new Vector2(player.velocity.x, jumpForce);
                jumpCount = 1;
            }
            else if (isAllowedToDoubleJump && !jumpButtonPressed)
            {
                Debug.Log("Sec");
                player.velocity = new Vector2(player.velocity.x, player.velocity.y + jumpForce / 2);
                isAllowedToDoubleJump = false;
                jumpCount = 0;
            }
            jumpButtonPressed = true;
        }
        if (buttons[(int)Buttons.Attack].IsPressed)
        {
            attackTime = Time.time;
            attackCount = 1;
        }
        if (!isGrounded && attackCount > 0)
        {
            highAttack = true;
        }
       
        if (Time.time - attackTime >= attackWait || (highAttack && isGrounded))
        {           
            attackCount = 0;
            highAttack = false;
        }

        if (player.velocity.x < 0)
            render.flipX = true;
        else if (player.velocity.x > 0)
            render.flipX = false;
        anim.SetBool("isGrounded", isGrounded);
        anim.SetInteger("attack", attackCount);
        anim.SetFloat("moveSpeed", Mathf.Abs(player.velocity.x));
    }
}
