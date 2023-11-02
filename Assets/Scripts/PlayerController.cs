using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float speed = 3;
    private Vector3 move;

    [Header("Component")]
    private SpriteRenderer sprite;
    private Animator animator;
    private ObjectPool bulletPool;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        bulletPool = GetComponent<ObjectPool>();
    }

    void Update()
    {
        MoveInput();
        Shoot();
    }

    void FixedUpdate()
    {
        Move();
    }

    void MoveInput()
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

    void Move()
    {
        transform.Translate(move * speed * Time.fixedDeltaTime);
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;
            Vector3 direction = worldPos - transform.position;
            GameObject newBullet = bulletPool.Get();

            if (newBullet != null)
            {
                newBullet.transform.position = transform.position + (Vector3.down * 0.5f);
                newBullet.GetComponent<Bullet>().Direction = direction;
            }
        }
    }
}