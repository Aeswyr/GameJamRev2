using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private InputHandler input;

    [Header("Movement Variables")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float airdashSpeed;
    [SerializeField] private float startAirDashTime;
    [SerializeField] private float gravity;
    
    [SerializeField] private int jumps = 2;
    [SerializeField] private int airdashes = 1;

    private float airDashTime;

    private float speed;

    private float jumpLockout;
    private float jumpLock = 0.2f;

    private int airdashingDir;

    private bool grounded = false;


    private void Start()
    {
        speed = walkSpeed;
    }

    void FixedUpdate() {
        CheckGrounded();
        AirDashMod();
        if (airDashTime <= 0)
        {
            Move();
            Jump();
            Action();
            GroundDashMod();
        }
    }

    private void Move() {
        Vector2 velocity = new Vector2(input.dir.x * speed, rbody.velocity.y);
        if (grounded)
        {
            rbody.velocity = velocity;
        }
        else
        {
            if((Mathf.Sign(rbody.velocity.x) != Mathf.Sign(velocity.x)) 
                || (velocity.x > rbody.velocity.x))
            {
                rbody.velocity = velocity;
            }
        }
            bool running = input.dir.x != 0;

            if (input.dir.x > 0)
                sprite.flipX = false;
            if (input.dir.x < 0)
                sprite.flipX = true;

            animator.SetBool("running", running);
    }

    private void GroundDashMod()
    {
       if (input.dashmod.pressed)
        {
            speed = dashSpeed;
        }
       else if (input.dashmod.released)
        {
            speed = walkSpeed;
        }

    }

    private void AirDashMod()
    {
        if (!grounded)
        {
            if (airdashingDir == 0)
            {
                if (input.airdashmod.pressed && input.dir.x != 0 && airdashes != 0)
                {
                    airdashes--;
                    rbody.velocity = Vector2.right * rbody.velocity.x;
                    airDashTime = startAirDashTime;
                    airdashingDir = (int)input.dir.x;
                    rbody.gravityScale = 0;
                }
            }
            else
            {
                if (airDashTime <= 0)
                {
                   EndAirDash();
                }
                else
                {
                    airDashTime -= Time.deltaTime;
                    rbody.velocity = Vector2.right * airdashSpeed * airdashingDir;
                }
            }
        }
        else if (airdashingDir != 0)
        {
            EndAirDash();
        }
    }

    private void EndAirDash()
    {
        airdashingDir = 0;
        airDashTime = 0;
        rbody.velocity = Vector2.zero;
        rbody.gravityScale = gravity;
    }

    private void Jump() {
        if (input.jump.pressed && (grounded || jumps > 0)) {
            rbody.velocity = new Vector2(rbody.velocity.x, 30);
            jumps--;
            jumpLockout = Time.time + jumpLock;
            animator.SetBool("grounded", grounded && Time.time > jumpLockout);
            animator.SetTrigger("jump");
        }
    }

    private void Action() {
        if (input.primary.pressed) {
            var obj = Instantiate(starPrefab, transform.position, Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().flipX = sprite.flipX;

        }
    }

    private void CheckGrounded() {
        bool wasGrounded = grounded;
        grounded = Physics2D.Raycast((Vector2)transform.position - new Vector2(0, 1.5f), Vector2.down, 0.1f, LayerMask.GetMask("World"));
        if (grounded)
        {
            jumps = 2;
            airdashes = 1;
        }
        if (grounded && !wasGrounded)
            animator.SetTrigger("land");
        animator.SetBool("grounded", grounded && Time.time > jumpLockout);
    }
}
