using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour {

    private bool _needToStart = true;

    void Awake() {
        if (_needToStart) {
            _needToStart = false;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}