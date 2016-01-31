using UnityEngine;
using System.Collections;

public class WeaponConfiguration : ScriptableObject {

    [Header("Basic Parameters")]
    [Space(1f)]

    [SerializeField]
    float _damage = 8f;
    public float Damage {
        get { return _damage; }
    }

    [SerializeField]
    float _attackRange = 3f;
    public float AttackRange {
        get { return _attackRange; }
    }
}
