using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected int gems;
    [SerializeField] protected Transform pointA, pointB;
    [SerializeField] protected float alertDistance;

    //protected bool isHit = false;

    protected Transform target;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    protected Transform player;
    protected bool isDead = false;

    public virtual void Init()
    {
        animator = GetComponentInChildren<Animator>();
        if (animator == null) { Debug.LogError(this.gameObject.name + ".animator is null"); }
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer == null) { Debug.LogError(this.gameObject.name + ".renderer is null"); }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (player == null) { Debug.LogError(this.gameObject.name+".player is null"); }

        target = pointB;
    }

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (isDead == true)
        {
            return;
        }

        //face player if in combat mode
        if (animator.GetBool("InCombat") == true)
        {
            Vector2 direction = transform.position - player.position;
            if (direction.x > 0)
            {
                spriteRenderer.flipX = true;
                //transform.Rotate(0, 180f, 0);
            }
            else if (direction.x < 0)
            {
                spriteRenderer.flipX = false;
            }

            //if player moves away exit combat mode
            //Debug.Log(this.gameObject.name + " to player distance: " + Vector2.Distance(transform.position, player.position));
            if (Vector2.Distance(transform.position, player.position) > alertDistance)
            {
                //isHit = false;
                animator.SetBool("InCombat", false);
            }
        }
        else if (Vector2.Distance(transform.position, player.position) < alertDistance)
        {
            animator.SetTrigger("Idle");
            animator.SetBool("InCombat", true);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") || animator.GetBool("InCombat") == true)
        {
            return;
        }
        Movement();
    }

    public virtual void Movement()
    {
        if (target == pointA)
        {
            spriteRenderer.flipX = true;
        }
        else if (target == pointB)
        {
            spriteRenderer.flipX = false;
        }

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
        if (animator.GetBool("InCombat") == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }

    public virtual void Attack()
    {
        Debug.Log("My name is: " + this.gameObject.name);
    }
}
