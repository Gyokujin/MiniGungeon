using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    private Vector3 move;

    [Header("Component")]
    private SpriteRenderer sprite;
    private Animator animator;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        move = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            move += Vector3.left;
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            move += Vector3.right;
        }
        
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            move += Vector3.up;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            move += Vector3.down;
        }

        if (move.x < 0)
        {
            sprite.flipX = true;
        }
        else if (move.x > 0)
        {
            sprite.flipX = false;
        }

        if (move.magnitude > 0)
        {
            animator.SetTrigger("Move");
        }
        else
        {
            animator.SetTrigger("Stop");
        }
    }

    void FixedUpdate()
    {
        transform.Translate(move * speed * Time.fixedDeltaTime);
    }
}