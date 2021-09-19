using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderAnimationEvent : MonoBehaviour
{
    //handle
    private Spider _spider;

    private void Start()
    {
        _spider = transform.parent.GetComponent<Spider>();
        if (_spider == null) { Debug.LogError(this.gameObject.name + ".spider is null"); }
    }

    public void Fire()
    {
        Debug.Log("Spider anim event: Fire");
        _spider.Attack();
    }
}
