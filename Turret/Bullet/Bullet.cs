using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Bullet : MonoBehaviour
{
    private Action<Bullet> _killAction;
    private float _damage;
    private string _targetTag;
    private WaitForSeconds _delay = new WaitForSeconds(1f);
    public void Init(Action<Bullet> killAction, string targetTag, float dmg)
    {
        _killAction = killAction;
        _targetTag = targetTag;
        _damage = dmg;
        StartCoroutine(SelfDestruct());
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag(_targetTag))
        {
            other.GetComponentInParent<Car>().GetHit(_damage);
            _killAction(this);
        }
    }
    IEnumerator SelfDestruct()
    {
        yield return _delay;
        _killAction(this);
    }
    public float Damage { get { return _damage; } set { _damage = value; } }
}
