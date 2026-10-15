using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // 1. Añadido para usar el nuevo Input System

namespace ClearSky
{
    public class DemoCollegeStudentController : MonoBehaviour
    {
        public float movePower = 10f;
        public float KickBoardMovePower = 15f;
        public float jumpPower = 20f;

        private Rigidbody2D rb;
        private Animator anim;
        private int direction = 1;
        bool isJumping = false;
        private bool alive = true;
        private bool isKickboard = false;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            Restart();
            if (alive)
            {
                Hurt();
                Die();
                Attack();
                Jump();
                KickBoard();
                Run();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            anim.SetBool("isJump", false);
        }

        void KickBoard()
        {
            if (Keyboard.current == null) return;

            if (Keyboard.current.digit4Key.wasPressedThisFrame && isKickboard)
            {
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
            }
            else if (Keyboard.current.digit4Key.wasPressedThisFrame && !isKickboard)
            {
                isKickboard = true;
                anim.SetBool("isKickBoard", true);
            }
        }

        void Run()
        {
            float horizontalInput = 0f;
            if (Keyboard.current != null)
            {
                if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                    horizontalInput = -1f;
                if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                    horizontalInput = 1f;
            }

            float currentPower = isKickboard ? KickBoardMovePower : movePower;
            Vector3 moveVelocity = Vector3.zero;

            if (!isKickboard)
            {
                anim.SetBool("isRun", false);
            }

            if (horizontalInput < 0)
            {
                direction = -1;
                moveVelocity = Vector3.left;
                transform.localScale = new Vector3(direction, 1, 1);
                if (!isKickboard && !anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);
            }
            else if (horizontalInput > 0)
            {
                direction = 1;
                moveVelocity = Vector3.right;
                transform.localScale = new Vector3(direction, 1, 1);
                if (!isKickboard && !anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);
            }

            transform.position += moveVelocity * currentPower * Time.deltaTime;
        }

        void Jump()
        {
            if (Keyboard.current == null) return;

            bool jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame ||
                               Keyboard.current.wKey.wasPressedThisFrame ||
                               Keyboard.current.upArrowKey.wasPressedThisFrame;

            if (jumpPressed && !anim.GetBool("isJump"))
            {
                isJumping = true;
                anim.SetBool("isJump", true);
            }

            if (!isJumping) return;

            rb.linearVelocity = Vector2.zero;
            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
            isJumping = false;
        }

        void Attack()
        {
            if (Keyboard.current != null && Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                anim.SetTrigger("attack");
            }
        }

        void Hurt()
        {
            if (Keyboard.current != null && Keyboard.current.digit2Key.wasPressedThisFrame)
            {
                anim.SetTrigger("hurt");
                if (direction == 1)
                    rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
                else
                    rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
            }
        }

        void Die()
        {
            if (Keyboard.current != null && Keyboard.current.digit3Key.wasPressedThisFrame)
            {
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
                anim.SetTrigger("die");
                alive = false;
            }
        }

        void Restart()
        {
            if (Keyboard.current != null && Keyboard.current.digit0Key.wasPressedThisFrame)
            {
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
                anim.SetTrigger("idle");
                alive = true;
            }
        }
    }
}