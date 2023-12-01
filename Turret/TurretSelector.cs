using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Turret/new turret")]
public class TurretSelector : ScriptableObject
{
    [SerializeField]
    public List<Vector3> turretTransforms;
    [SerializeField]
    public int turretCount;
}
