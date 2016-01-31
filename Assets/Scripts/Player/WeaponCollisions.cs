using UnityEngine;
using System.Collections;

public class WeaponCollisions : MonoBehaviour {

    private Vector3 _raycastDirection;

    GameObject _player;

    void Start() {
        _player = GameObject.Find("Player");
    }

    void Update() {
        if (_player.GetComponent<PlayerController>()._facingRight) _raycastDirection = Vector3.right;
        else _raycastDirection = Vector3.left;

        Debug.DrawLine(transform.position, transform.position + (_raycastDirection * PlayerManager.Instance.weaponConfig.AttackRange), Color.red, 0.1f);
        RaycastHit2D _hitInfo = Physics2D.Raycast(transform.position, _raycastDirection, PlayerManager.Instance.weaponConfig.AttackRange);

        if (_hitInfo.collider != null) {
            if (_player.GetComponent<PlayerController>()._allowHit) {
                if (_hitInfo.collider.tag.Equals("Bush")) {
                    _hitInfo.collider.GetComponent<EnemyData>().UpdateLife(_player.GetComponent<PlayerManager>().weaponConfig.Damage);
                    _hitInfo.collider.GetComponentInChildren<ParticleSystem>().Play();
                    _hitInfo.collider.transform.localScale *= 0.8f;
                }

                if (_hitInfo.collider.tag.Equals("Barrier")) {
                    _hitInfo.collider.GetComponent<EnemyData>().UpdateLife(_player.GetComponent<PlayerManager>().weaponConfig.Damage);
                    _hitInfo.collider.GetComponentInChildren<ParticleSystem>().Play();
                }
            }
        }

        _player.GetComponent<PlayerController>()._allowHit = false;
    }

	/*void OnTriggerStay2D(Collider2D _col) {
        Debug.Log("lol");

        if (_player.GetComponent<PlayerController>()._allowHit) {
            if (_col.gameObject.tag.Equals("Bush")) {
                _col.GetComponent<EnemyData>().UpdateLife(_player.GetComponent<PlayerManager>().weaponConfig.Damage);
            }
        }

        _player.GetComponent<PlayerController>()._allowHit = false;
    }*/
}
