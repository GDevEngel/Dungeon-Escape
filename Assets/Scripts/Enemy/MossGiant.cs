using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    //config
    private float _damageCD = 0.5f;

    //global var
    private bool _canDamage = true;

    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    public void Damage()
    {
        if (_canDamage == true)
        {
            _canDamage = false;
            Debug.Log(this.gameObject.name+" Damage()");
            Health--;
            animator.SetTrigger("Hit");
            animator.SetBool("InCombat", true);
            if (Health < 1)
            {
                animator.SetTrigger("Death");
                isDead = true;
                //Destroy(this.gameObject, 1.5f);
            }
            else
            {
                StartCoroutine(ResetCanDamage());
            }
        }
    }

    IEnumerator ResetCanDamage()
    {
        Debug.Log("reset dmg coroutine started");
        yield return new WaitForSeconds(_damageCD);
        _canDamage = true;
        Debug.Log("reset dmg coroutine ended");
    }
}
