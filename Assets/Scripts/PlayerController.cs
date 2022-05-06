using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Rigidbody2D rbody;
    [SerializeField] private Animator animator;
    float jumpLockout;
    float jumpLock = 0.2f;
    [SerializeField] private float speed;

    private bool grounded = false;
    int jumps = 1;
    void FixedUpdate() {
        CheckGrounded();
        Move();
    }

    void Update() {
        Jump();
    }

    private void Move() {
        rbody.velocity = rbody.velocity.y * Vector2.up;

        bool running = false;
        if (Input.GetKey(KeyCode.D)) {
            rbody.velocity = new Vector2(speed, rbody.velocity.y);
            sprite.flipX = false;
            running = true;
        }
        if (Input.GetKey(KeyCode.A)) {
            rbody.velocity = new Vector2(-speed, rbody.velocity.y); 
            sprite.flipX = true;
            running = true;
        }

        animator.SetBool("running", running);
    }

    private void Jump() {
        if (Input.GetKeyDown(KeyCode.Space) && (grounded || jumps > 0)) {
            rbody.velocity = new Vector2(rbody.velocity.x, 30);
            jumps--;
            jumpLockout = Time.time + jumpLock;
            animator.SetBool("grounded", grounded && Time.time > jumpLockout);
            animator.SetTrigger("jump");
        }
    }

    private void CheckGrounded() {
        bool wasGrounded = grounded;
        grounded = Physics2D.Raycast((Vector2)transform.position - new Vector2(0, 1.5f), Vector2.down, 0.1f, LayerMask.GetMask("World"));
        if (grounded)
            jumps = 1;
        if (grounded && !wasGrounded)
            animator.SetTrigger("land");
        animator.SetBool("grounded", grounded && Time.time > jumpLockout);
    }
}
