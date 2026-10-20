using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ClearSky
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movimiento y Salto")]
        public float movePower = 10f;
        public float jumpPower = 20f;

        private Rigidbody2D rb;
        private Animator anim;
        private int direction = 1;
        private bool isJumping = false;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            Run();
            Jump();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            // Restablece la animación de salto al tocar el suelo
            if (anim != null)
            {
                anim.SetBool("isJump", false);
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

            Vector3 moveVelocity = Vector3.zero;

            // Restablece la animación de correr por defecto
            if (anim != null)
            {
                anim.SetBool("isRun", false);
            }

            if (horizontalInput < 0)
            {
                direction = -1;
                moveVelocity = Vector3.left;
                transform.localScale = new Vector3(direction, 1, 1);

                if (anim != null && !anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);
            }
            else if (horizontalInput > 0)
            {
                direction = 1;
                moveVelocity = Vector3.right;
                transform.localScale = new Vector3(direction, 1, 1);

                if (anim != null && !anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);
            }

            // Movimiento horizontal
            transform.position += moveVelocity * movePower * Time.deltaTime;
        }

        void Jump()
        {
            if (Keyboard.current == null) return;

            bool jumpPressed = Keyboard.current.spaceKey.wasPressedThisFrame ||
                               Keyboard.current.wKey.wasPressedThisFrame ||
                               Keyboard.current.upArrowKey.wasPressedThisFrame;

            if (jumpPressed && (anim == null || !anim.GetBool("isJump")))
            {
                isJumping = true;
                if (anim != null)
                {
                    anim.SetBool("isJump", true);
                }
            }

            if (!isJumping) return;

            // Aplica la fuerza de salto
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // Limpia la velocidad vertical previa
            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
            
            isJumping = false;
        }
    }
}