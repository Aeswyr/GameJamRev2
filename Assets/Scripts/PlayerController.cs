using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject starPrefab;
    [SerializeField] private InputHandler input;
    float jumpLockout;
    float jumpLock = 0.2f;
    [SerializeField] private float speed;

    private bool grounded = false;
    int jumps = 2;
    void FixedUpdate() {
        CheckGrounded();
        Move();
        Jump();
        Action();
    }

    private void Move() {
        rbody.velocity = new Vector2(input.dir.x * speed, rbody.velocity.y);

        bool running = input.dir.x != 0;
        
        if (input.dir.x > 0)
            sprite.flipX = false;
        if (input.dir.x < 0)
            sprite.flipX = true;

        animator.SetBool("running", running);
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
            jumps = 2;
        if (grounded && !wasGrounded)
            animator.SetTrigger("land");
        animator.SetBool("grounded", grounded && Time.time > jumpLockout);
    }
}
