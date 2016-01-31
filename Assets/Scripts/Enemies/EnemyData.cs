using UnityEngine;
using System.Collections;

public class EnemyData : MonoBehaviour {

    public EnemyConfiguration config;

    float _actualHealth;

    void Start() {
        _actualHealth = config.HealthPoints;
    }

    void Update() {
        if (_actualHealth <= 0f && !GetComponentInChildren<ParticleSystem>().isPlaying) {
            Destroy(gameObject);
        }
    }

    public void UpdateLife(float _damage) {
        _actualHealth -= _damage;

        if (_actualHealth <= 0f) {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
