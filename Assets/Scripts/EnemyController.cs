using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum State
    {
        Spawning,
        Moving,
        Dying
    }

    private State state;

    [Header("Move")]
    [SerializeField]
    private float speed = 2;

    [Header("Action")]
    [SerializeField]
    private float dieTime = 1.4f;
    private GameObject target;
    [SerializeField]
    private Material defaultMaterial;
    [SerializeField]
    private Material flashMaterial;
    [SerializeField]
    private float flashTime = 0.5f;

    [Header("Component")]
    private SpriteRenderer sprite;
    private Animator animator;
    private Character character;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        character = GetComponent<Character>();
    }

    void Start()
    {
        target = GameObject.Find("Player");
        state = State.Moving;
    }

    void FixedUpdate()
    {
        if (state == State.Spawning)
        {
            animator.SetTrigger("Stop");
        }
        else if (state == State.Moving)
        {
            Vector2 direction = target.transform.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.fixedDeltaTime);
            animator.SetTrigger("Move");

            if (direction.x < 0)
            {
                sprite.flipX = true;
            }
            else if (direction.x > 0)
            {
                sprite.flipX = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            float damage = collision.gameObject.GetComponent<Bullet>().damage;

            if (character.Hit(damage))
            {
                Flash();
            }
            else
            {
                Die();
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
        state = State.Dying;
        animator.SetTrigger("Die");
        Invoke("AfterDying", dieTime);
    }

    void AfterDying()
    {
        Destroy(gameObject);
    }
}