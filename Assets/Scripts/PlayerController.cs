using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float speed = 3;
    private Vector3 move;

    [Header("Action")]
    [SerializeField]
    private float flashTime = 0.5f;
    [SerializeField]
    private float dieTime = 0.875f;

    [SerializeField]
    private Material defaultMaterial;
    [SerializeField]
    private Material flashMaterial;

    [Header("Component")]
    private SpriteRenderer sprite;
    private Animator animator;
    private Character character;
    private ObjectPool bulletPool;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        character = GetComponent<Character>();
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

    void Flash()
    {
        sprite.material = flashMaterial;
        Invoke("AfterFlash", flashTime);
    }

    void AfterFlash()
    {
        sprite.material = defaultMaterial;
    }

    void Die()
    {
        animator.SetTrigger("Die");
        Invoke("AfterDying", dieTime);
    }

    void AfterDying()
    {
        SceneManager.LoadScene("GameOver");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (character.Hit(1))
            {
                Flash();
            }
            else
            {
                Die();
            }
        }
    }
}