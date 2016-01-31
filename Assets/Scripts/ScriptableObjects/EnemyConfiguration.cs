using UnityEngine;
using System.Collections;

public class EnemyConfiguration : ScriptableObject {

    [Header("Basic Parameters")]
    [Space(1f)]

    [SerializeField]
    float _healthPoints = 100f;
    public float HealthPoints {
      get { return _healthPoints; }
    }

    [SerializeField]
    int _damage = 0;
    public int Damage {
        get { return _damage; }
    }

    [SerializeField]
    float _moveSpeed = 0f;
    public float MoveSpeed {
        get { return _moveSpeed; }
    }
}
