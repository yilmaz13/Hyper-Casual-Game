using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public InputManager inputManager;
    public CharacterController controller;
    public Animator anim;
    public GameObject bloodEffect;

    public float speed;
    public float gravity;
    Vector3 moveDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!GameManager.Instance.IsGameActive)
            return;

        Vector2 direction = InputManager.Instance.direction;

        if (controller.isGrounded)
        {
            moveDirection = new Vector3(direction.x, 0, direction.y);

            Quaternion targetRotation = moveDirection != Vector3.zero
                ? Quaternion.LookRotation(moveDirection)
                : transform.rotation;
            transform.rotation = targetRotation;

            moveDirection = moveDirection * speed;
        }

        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

        if (anim.GetBool("Moving") != (direction != Vector2.zero))
            anim.SetBool("Moving", direction != Vector2.zero);
    }

    public void Die()
    {
        LevelManager.Instance.GameOver();
        Instantiate(bloodEffect, transform.position + Vector3.up * 1.5f, transform.rotation);
        anim.SetBool("Dead", true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeathArea"))
        {
            Die();
        } 
        if (other.gameObject.CompareTag("Bullet"))
        {
            Die();
        } 
        if (other.gameObject.CompareTag("Finish"))
        {
            GameManager.Instance.IsGameActive = false;
            UIManager.Instance.ActivateGameWonUI();
        }
    }
}