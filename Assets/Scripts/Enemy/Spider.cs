using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    //handle
    [SerializeField] private GameObject AcidPrefab;

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
            Health--;
            _canDamage = false;
            //animator.SetBool("InCombat", true);
            if (Health < 1)
            {
                animator.SetTrigger("Death");
                isDead = true;
                //Destroy(this.gameObject, 1.5f);
            }
        }
    }

    public override void Movement()
    {
        //sit your ass down
    }

    public override void Attack()
    {
        Instantiate(AcidPrefab, transform.position, Quaternion.identity);
    }
}