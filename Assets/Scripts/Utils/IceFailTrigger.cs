using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class IceFailTrigger : MonoBehaviour {

    public GameObject screen169;
    public GameObject lives;
    public RuntimeAnimatorController failedAnimator;

    private bool _isTouched = false;
    private float _resetPossibilities = 5f;
    private bool _countdownOn = false;

    GameObject _player;

    void Awake() {
        _player = GameObject.Find("Player");
    }

    void Update() {
        if (_isTouched && !_countdownOn) {
            Camera.main.GetComponent<SunShafts>().sunShaftIntensity += 0.03f;

            Camera.main.GetComponent<SunShafts>().sunShaftIntensity = Mathf.Clamp(Camera.main.GetComponent<SunShafts>().sunShaftIntensity, 1f, 4f);

            if (Camera.main.GetComponent<SunShafts>().sunShaftIntensity >= 3.5f) {
                _player.GetComponent<Animator>().SetBool("Fail", true);

                _countdownOn = true;
            }
        }

        if (_countdownOn) {
            _resetPossibilities -= Time.deltaTime;

            if (_resetPossibilities <= 0f) {
                _player.GetComponent<PlayerController>().CanMove = true;
                _player.GetComponent<PlayerController>().canJump = true;

                _player.GetComponent<Animator>().runtimeAnimatorController = failedAnimator;
                lives.SetActive(true);

                Camera.main.GetComponent<SunShafts>().sunShaftIntensity -= 0.03f;
                if (Camera.main.GetComponent<SunShafts>().sunShaftIntensity <= 1f) {
                    Camera.main.GetComponent<SunShafts>().enabled = false;
                }

                screen169.GetComponent<Animator>().SetBool("Activated", false);
            }
        }
    }

	void OnTriggerEnter2D(Collider2D _col) {
        if (_col.gameObject.tag.Equals("Player")) {
            _col.GetComponent<PlayerController>().CanMove = false;
            _col.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            screen169.GetComponent<Animator>().SetBool("Activated", true);
            Camera.main.GetComponent<SunShafts>().enabled = true;
            lives.SetActive(false);

            _isTouched = true;
        }
    }
}
