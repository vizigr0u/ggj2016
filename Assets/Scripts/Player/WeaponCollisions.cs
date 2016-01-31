using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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

        if (_hitInfo.collider == null && _player.GetComponent<PlayerController>()._checkWindSound && !_player.GetComponent<PlayerController>()._isAttacking) {
            SoundsManager.Instance.PlaySound(SoundsManager.Instance.sounds[22]);

            _player.GetComponent<PlayerController>()._checkWindSound = false;
        }

        if (_hitInfo.collider != null) {
            if (_player.GetComponent<PlayerController>()._allowHit) {
                if (_hitInfo.collider.tag.Equals("Bush")) {
                    _hitInfo.collider.GetComponent<EnemyData>().UpdateLife(_player.GetComponent<PlayerManager>().weaponConfig.Damage);
                    _hitInfo.collider.GetComponentInChildren<ParticleSystem>().Play();
                    _hitInfo.collider.transform.localScale *= 0.8f;

                    int _random = Random.Range(15, 20);
                    SoundsManager.Instance.PlaySound(SoundsManager.Instance.sounds[_random]);
                }

                if (_hitInfo.collider.tag.Equals("Barrier")) {
                    _hitInfo.collider.GetComponent<EnemyData>().UpdateLife(_player.GetComponent<PlayerManager>().weaponConfig.Damage);
                    _hitInfo.collider.GetComponentInChildren<ParticleSystem>().Play();

                    int _random = Random.Range(7, 9);
                    SoundsManager.Instance.PlaySound(SoundsManager.Instance.sounds[_random]);
                }

                if (_hitInfo.collider.tag.Equals("Rabbit")) {
                    _hitInfo.collider.GetComponent<EnemyData>().UpdateLife(_player.GetComponent<PlayerManager>().weaponConfig.Damage);
                    _hitInfo.collider.GetComponentInChildren<ParticleSystem>().Play();

                    int _random = Random.Range(20, 22);
                    SoundsManager.Instance.PlaySound(SoundsManager.Instance.sounds[_random]);
                }

                if (SceneManager.GetActiveScene().name == "Introduction") {
                    if (_hitInfo.collider.tag.Equals("GrandFather")) {
                        if (PapyFirstDialogManager.Instance.canBeAttacked) {
                            PapyFirstDialogManager.Instance.PlaySound();
                            PapyFirstDialogManager.Instance.Display();

                            int _random = Random.Range(7, 9);
                            SoundsManager.Instance.PlaySound(SoundsManager.Instance.sounds[_random]);

                            PapyFirstDialogManager.Instance.attackCount++;
                            PapyFirstDialogManager.Instance.canBeAttacked = false;
                        }
                    }
                }
            }
        }

        _player.GetComponent<PlayerController>()._allowHit = false;
    }
}
