using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    public Car _car;
    [SerializeField]
    Vehicle _vehicle;
    [SerializeField]
    TurretController _turretController;
    [SerializeField]
    Sprite _turretSprite;

    private void Awake()
    {
        if (_car == null) _car = GetComponentInChildren<Car>();
        if (_turretController == null) _turretController = GetComponentInChildren<TurretController>();
    }
    private void Start()
    {
        _car.InitCar(10f, 10f, 10f, 2f, 1, _vehicle.icon);
        _turretController.AddTurret(10f, 100f, 10f, 2f, 10f, "Player", _turretSprite);
        _turretController.AddTurret(10f, 100f, 10f, 2f, 10f, "Player", _turretSprite);
    }
    public void UpgradeCar(StatType statType, float value)
    {
        _car.UpgradeStat(statType, value);
    }
    public void UpgradeTurret(StatType statType, float value)
    {
        _turretController.UpgradeStat(statType, value);
    }
}
