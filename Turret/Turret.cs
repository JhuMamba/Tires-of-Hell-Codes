using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEngine.GraphicsBuffer;
using System.Data;

public class Turret : MonoBehaviour
{
    float _damage;
    float _attackSpeed;
    float _attackCoolDown;
    float _range;
    float _critChance;
    float _critDamage;
    string _targetTag;
    Sprite _sprite;

    public float Damage { get { return _damage; } set {  _damage = value; } }
    public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
    public float AttackCoolDown { get { return _attackCoolDown; } set { _attackCoolDown = value; } }
    public float Range { get { return _range; } set { _range = value; } }
    public float CritChance { get { return _critChance; } set { _critChance = value; } }
    public float CritDamage { get { return _critDamage; } set { _critDamage = value; } }
    public string TargetTag { get { return _targetTag; } set { _targetTag = value; } }
    public Sprite Sprite { get { return _sprite; } set { _sprite = value; } }

    //TurredData _turretData;

    SpriteRenderer _spriteRenderer;
    GameObject _currentTarget;
    BulletSpawner _bulletSpawner;

    private void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _bulletSpawner = GetComponent<BulletSpawner>();
        _attackCoolDown = 0f;
    }
    private void Start()
    {
        _bulletSpawner.TargetTag = _targetTag;
        _spriteRenderer.sprite = _sprite;
    }
    private void Update()
    {
        if (_currentTarget == null || (Vector2.Distance(transform.position, _currentTarget.transform.position) > _range)) SearchForTarget();
        else
        {
            transform.up = _currentTarget.transform.position - transform.position;
            if (_attackCoolDown >= _attackSpeed) Shoot();
            else _attackCoolDown++;
        }
    }
    void SearchForTarget()
    {
        //add layer mask
        Collider2D[] target = Physics2D.OverlapCircleAll((Vector2)transform.position, _range, 1 << LayerMask.NameToLayer(_targetTag));
        if (target.Length < 1) return;
        Collider2D[] orderedByProximity = target.OrderBy(c => (transform.position - c.transform.position).sqrMagnitude).ToArray();
        _currentTarget = orderedByProximity[0].gameObject;
    }
    void Shoot()
    {
        _attackCoolDown = 0f;
        _bulletSpawner.FirePoint = transform;
        float damageResult = (Random.Range(0f,100f) <= CritChance) ? _damage * _critDamage : _damage;
        _bulletSpawner.Spawn(damageResult, 20f);
    }
}