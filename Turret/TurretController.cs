using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField]
    Turret turretPrefab;
    [SerializeField]
    List<Turret> turrets;
    [SerializeField]
    Vector3[] turretPositions;
    int _initTurretCount;
    /// <summary>
    /// Initialise the turret for the first time if not initialised. Use three parameters after initialised.
    /// </summary>
    /// <param name="damage">Damage in float.</param>
    /// <param name="attackSpeed">Attack Speed :: Increases by 1 on every update if not attacking.</param>
    /// <param name="range">Range in float.</param>
    /// <param name="target">Target tag or layer name.</param>
    /// <param name="sprite">Sprite of the turret.</param>
    private void Awake()
    {
        _initTurretCount = 0;
    }
    public void AddTurret(float damage, float attackSpeed, float critChance, float critDamage, float range, string target, Sprite sprite)
    {
        Turret newTurret = Instantiate(turretPrefab, transform.position + turretPositions[_initTurretCount], Quaternion.identity, transform);
        newTurret.tag = this.tag;
        newTurret.Damage = damage;
        newTurret.AttackSpeed = attackSpeed;
        newTurret.Range = range;
        newTurret.CritChance = critChance;
        newTurret.CritDamage = critDamage;
        newTurret.TargetTag = target;
        newTurret.Sprite = sprite;
        turrets.Add(newTurret);
        _initTurretCount++;
    }
    public void UpgradeStat(StatType type, float value)
    {
        foreach (var turret in turrets)
        {
            switch (type)
            {
                case StatType.damage:
                    turret.Damage += value;
                    break;
                case StatType.attackSpeed:
                    turret.AttackSpeed += value;
                    break;
                case StatType.range:
                    turret.Range += value;
                    break;
                case StatType.critChance:
                    turret.CritChance += value;
                    break;
                case StatType.damagedRate:
                    turret.CritDamage += value;
                    break;
            }
        }
    }
}