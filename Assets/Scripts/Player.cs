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
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] PointerListener[] buttons;
    private enum Buttons{Left, Right, Attack, HAttack, Block, Jump};
    private Animator anim;
    private SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame

    public void Print()
    {
        Debug.Log("Hello!\n");
    }
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround);
        if (isGrounded)
            isAllowedToDoubleJump = true;
        
        int direction = 0;
        if (buttons[(int)Buttons.Left].IsPressed)
            direction = -1;
        if (buttons[(int)Buttons.Right].IsPressed)
            direction = 1;
        if ( buttons[(int)Buttons.Left].IsPressed && buttons[(int)Buttons.Right].IsPressed)
            direction = 0;
        player.velocity = new Vector2(moveSpeed * direction, player.velocity.y);
        if (buttons[(int)Buttons.Jump].IsPressed)
        {   
            if (isGrounded)
                player.velocity = new Vector2(player.velocity.x, jumpForce);
            else if (isAllowedToDoubleJump)
            {
                player.velocity = new Vector2(player.velocity.x, jumpForce);
                isAllowedToDoubleJump = false;
            }
        }
        if (player.velocity.x < 0)
            render.flipX = true;
        else if (player.velocity.x > 0)
            render.flipX = false;
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("moveSpeed", Mathf.Abs(player.velocity.x));
    }
}
