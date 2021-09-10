using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //handle
    private Animator _animator;
    private Animator _swordAnimator;
    private SpriteRenderer _swordEffect;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        if (_animator == null) { Debug.Log(this.gameObject.name + " animator is null"); }

        _swordAnimator = GameObject.Find("Sword Arc").GetComponent<Animator>();
        _swordEffect = GameObject.Find("Sword Arc").GetComponent<SpriteRenderer>();
    }

    public void Move(float speed)
    {
        _animator.SetFloat("Speed", Mathf.Abs(speed));
    }

    public void Jumping(bool isJumping)
    {
        _animator.SetBool("Jumping", isJumping);
    }

    public void Attacking(bool FaceBack)
    {
        _animator.SetTrigger("Attack");
        _swordAnimator.SetTrigger("Attack");

        if (FaceBack)
        {
            _swordEffect.transform.localPosition = new Vector2(-0.29f, 0);
        }
        else
        {
            _swordEffect.transform.localPosition = new Vector2(0.29f, 0);
        }

        //_swordEffect.flipX = FaceBack;
        _swordEffect.flipY = FaceBack;
    }
}
