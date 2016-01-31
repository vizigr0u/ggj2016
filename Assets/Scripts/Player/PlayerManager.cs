using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour {

    //singleton
    private static PlayerManager _instance;
    public static PlayerManager Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<PlayerManager>();
            }
            return PlayerManager._instance;
        }
    }

    public List<GameObject> hpSprites;

    [HideInInspector]
    public int playerHP = 3;

    void Awake() {
        if (_instance == null) _instance = this;
        else {
            DestroyImmediate(this);
            return;
        }
    }

    void Start () {
        ActualizeLife();
    }

    public void UpdateLife(int _damage) {
        playerHP -= _damage;

        ActualizeLife();
    }

    void ActualizeLife() {
        playerHP = Mathf.Clamp(playerHP, 0, 5);

        for (int i = 0; i < hpSprites.Count; i++) {
            hpSprites[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Heart_sprite_empty");
        }

        for (int i = 0; i < playerHP; i++) {
            hpSprites[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Heart_sprite");
        }
    }
}
