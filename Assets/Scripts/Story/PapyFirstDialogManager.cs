using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;
using UnityEngine.SceneManagement;

public class PapyFirstDialogManager : MonoBehaviour {

    //singleton
    private static PapyFirstDialogManager _instance;
    public static PapyFirstDialogManager Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<PapyFirstDialogManager>();
            }
            return PapyFirstDialogManager._instance;
        }
    }

    public int attackCount = 0;
    public bool canBeAttacked = true;
    public List<AudioClip> localDialogs;
    public List<string> dialogsStrings;
    public List<float> dialogsDurations;
    public List<Sprite> weaponChoices;
    public GameObject canvas169;
    public GameObject musicManager;
    public GameObject endChoiceImage;

    private float _resetCountdown = 3f;
    private bool _countdownON = false;
    private bool _endState = false;
    private float _globalDialogLength;
    private int _actualWeaponChoice = 0;

    //end state
    private float _switchCountdown = 1f;
    private bool _canSwitch = true;

    GameObject _player;
    GameObject _dialogText;

    void Awake() {
        if (_instance == null) _instance = this;
        else {
            DestroyImmediate(this);
            return;
        }

        _player = GameObject.Find("Player");
        _dialogText = GameObject.Find("Text : DialogTextPapy");

        _resetCountdown = dialogsDurations[0];
    }

    void Update() {
        if (!canBeAttacked && attackCount < 7) {
            _resetCountdown -= Time.deltaTime;

            if (attackCount == 4) {
                if (_resetCountdown >= 11.70f && _resetCountdown <= 11.9f) {
                    transform.FindChild("Particle").gameObject.SetActive(true);
                }

                if (_resetCountdown <= 5.5f) {
                    _dialogText.GetComponent<Text>().text = "Ugh, ugh, ugh...";
                }

                if (_resetCountdown <= 2.5f) {
                    _dialogText.GetComponent<Text>().text = "Je suis trop vieux pour ça.";
                }
            }

            if (_resetCountdown <= 0f) {
                if (attackCount < 4) {
                    canBeAttacked = true;
                }

                if (attackCount == 4) {
                    PlaySound();

                    _resetCountdown = dialogsDurations[attackCount];

                    attackCount++;
                    _dialogText.SetActive(false);

                    return;
                }

                if (attackCount == 5) {
                    musicManager.GetComponent<AudioSource>().Play();

                    PlaySound();
                    attackCount++;

                    return;
                }

                if (attackCount == 6) {
                    _endState = true;
                }

                if (attackCount < 6) {
                    _resetCountdown = dialogsDurations[attackCount];
                }
            }
        }

        if (attackCount == 4) {
            canvas169.GetComponent<Animator>().SetBool("Activated", true);

            _player.GetComponent<PlayerController>().CanMove = false;
            _player.GetComponent<Animator>().SetBool("Attacking", false);
        }

        //countdown telling when to removes the displaying
        if (_countdownON) {
            _globalDialogLength -= Time.deltaTime;
            if (_globalDialogLength <= 0f) {
                _dialogText.GetComponent<Animator>().SetBool("FadeIn", false);
                _dialogText.GetComponent<Animator>().SetBool("FadeOut", true);

                _countdownON = false;
            }
        }
        
        if (_endState) {
            endChoiceImage.SetActive(true);

            Camera.main.GetComponent<Blur>().enabled = true;

            if (Input.GetAxis("6thAxis") <= -0.5f && _canSwitch) {
                _actualWeaponChoice--;
                _canSwitch = false;
            }

            if (Input.GetAxis("6thAxis") >= 0.5f && _canSwitch) {
                _actualWeaponChoice++;
                _canSwitch = false;
            }

            if (Input.GetAxis("6thAxis") >= -0.2f && Input.GetAxis("6thAxis") <= 0.2f) {
                _canSwitch = true;
            }

            if (_actualWeaponChoice == -1) {
                _actualWeaponChoice = 2;
            }

            if (_actualWeaponChoice == 3) {
                _actualWeaponChoice = 0;
            }

            _switchCountdown -= Time.deltaTime;

            if (_switchCountdown <= 0f) {
                _canSwitch = true;
                _switchCountdown = 1f;
            }

            ActualizeEndChoice();

            if (Input.GetButtonDown("Jump") && _actualWeaponChoice == 0f) {
                SceneFadeInOut.Instance.StartFadeOut("level1");
            }
        }
    }

    void ActualizeEndChoice() {
        endChoiceImage.GetComponent<Image>().sprite = weaponChoices[_actualWeaponChoice];
    }

    public void Display() {
        _dialogText.GetComponent<Text>().text = dialogsStrings[attackCount];
        _dialogText.GetComponent<Animator>().SetBool("FadeIn", true);

        //sets the timer and launches it
        _globalDialogLength = dialogsDurations[attackCount];
        _countdownON = true;
    }

    public void PlaySound() {
        GetComponent<AudioSource>().PlayOneShot(localDialogs[attackCount]);
    }
}
