using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    //handle
    private Rigidbody2D _rigidbody;
    [SerializeField] LayerMask _groundLayer;
    private PlayerAnimation _playerAnimation;
    private SpriteRenderer _playerRenderer;

    //config
    private float _speed = 2.5f;
    private float _jumpForce = 250f;
    private WaitForSeconds _jumpCDTime = new WaitForSeconds(0.3f);
    private float _distance = 0.8f; //from player to ground

    //global var
    private float _move;
    private bool _faceBack = false;
    private bool _jumpOffCD = true;

    public int Health { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody == null) { Debug.Log(gameObject.name+".rigidbody is null"); }

        _playerAnimation = GetComponent<PlayerAnimation>();
        if (_playerAnimation == null) { Debug.Log(gameObject.name + " PlayerAnimation is null"); }

        _playerRenderer = GetComponentInChildren<SpriteRenderer>();
        if (_playerRenderer == null) { Debug.Log(this.gameObject.name + " SpriteRenderer is null"); }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    private void CalculateMovement()
    {
        _move = (Input.GetAxisRaw("Horizontal"));

        Debug.Log("IsGrounded: " + IsGrounded());
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(new Vector2(0, _jumpForce));
            //anim jumping
            _playerAnimation.Jumping(true);
            //jump cooldown check to prevent instant trigger of ground check raycast to end the jump anim
            StartCoroutine(JumpCD());
        }

        _rigidbody.velocity = new Vector2(_move * _speed, _rigidbody.velocity.y);

        _playerAnimation.Move(_move);
        Flip();
    }

    private void Flip()
    {
        if (_move > 0)
        {
            _playerRenderer.flipX = false;
            _faceBack = false;
        }
        else if (_move < 0)
        {
            _playerRenderer.flipX = true;
            _faceBack = true;
        }
    }

    private void Attack()
    {
        _playerAnimation.Attacking(_faceBack);
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _distance, _groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * _distance, Color.green);
        if (hit)
        {
            if (_jumpOffCD == true)
            {
                //anim stop jumping
                _playerAnimation.Jumping(false);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator JumpCD()
    {
        _jumpOffCD = false;
        yield return _jumpCDTime;
        _jumpOffCD = true;
    }

    public void Damage()
    {
        Debug.Log(this.gameObject.name + " damage called");
        //TODO cd system to prevent multiple hits
        Health--;
        if (Health < 1)
        {
            //TODO death anim
        }
    }
}
