using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected int gems;
    [SerializeField] protected Transform pointA, pointB;

    protected Transform target;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    public virtual void Init()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null) { Debug.LogError(this.gameObject.name + ".animator is null"); }
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer == null) { Debug.LogError(this.gameObject.name + ".renderer is null"); }

        target = pointB;
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            return;
        }
        Movement();
    }

    public virtual void Movement()
    {
        if (target == pointA)
        {
            spriteRenderer.flipX = false;
        }
        else if (target == pointB)
        {
            spriteRenderer.flipX = true;
        }

        spriteRenderer.flipX = !spriteRenderer.flipX;
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            animator.SetTrigger("Idle");
            //switch target
            if (target == pointA)
            {
                target = pointB;

            }
            else if (target == pointB)
            {
                target = pointA;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    public void Attack()
    {
        Debug.Log("My name is: " + this.gameObject.name);
    }
}
