using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public GameObject canvas169;

    private float _resetCountdown = 3f;

    void Awake() {
        if (_instance == null) _instance = this;
        else {
            DestroyImmediate(this);
            return;
        }
    }

    void Update() {
        if (!canBeAttacked) {
            _resetCountdown -= Time.deltaTime;

            if (_resetCountdown <= 0f) {
                canBeAttacked = true;
                _resetCountdown = 3f;
            }
        }

        if (attackCount == 4) {
            canvas169.GetComponent<Animator>().SetBool("Activated", true);
        }
    }

    public void PlaySound() {
        GetComponent<AudioSource>().PlayOneShot(localDialogs[attackCount]);
    }
}
