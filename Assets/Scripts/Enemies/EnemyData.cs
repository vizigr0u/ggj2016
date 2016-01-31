using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyData : MonoBehaviour {

    public EnemyConfiguration config;
    public bool hasHealthBar = false;

    [HideInInspector]
    public float _actualHealth;

    private float _initialLife;

    void Awake() {
        _initialLife = _actualHealth;
    }

    void Start() {
        _actualHealth = config.HealthPoints;
    }

    void Update() {
        if (_actualHealth <= 0f && !GetComponentInChildren<ParticleSystem>().isPlaying) {
            if (transform.name == "Barrier01_Collider") {
                Destroy(transform.parent.gameObject);
            }

            Destroy(gameObject);
        }

        if (hasHealthBar) {
            transform.GetComponentInChildren<Scrollbar>().size = _actualHealth / _initialLife;
        }
    }

    public void UpdateLife(float _damage) {
        _actualHealth -= _damage;

        if (_actualHealth <= 0f) {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
