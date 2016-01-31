using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundsManager : MonoBehaviour {

    //singleton
    private static SoundsManager _instance;
    public static SoundsManager Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<SoundsManager>();
            }
            return SoundsManager._instance;
        }
    }

    public List<AudioClip> sounds;

    GameObject _player;

    void Awake() {
        if (_instance == null) _instance = this;
        else {
            DestroyImmediate(this);
            return;
        }

        _player = GameObject.Find("Player");
    }

    public void PlaySound(AudioClip _clip) {
        _player.GetComponent<AudioSource>().PlayOneShot(_clip);
    }
}
