using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour {

    //singleton
    private static CharacterManager _instance;
    public static CharacterManager Instance {
        get {
            if (_instance == null) {
                _instance = GameObject.FindObjectOfType<CharacterManager>();
            }
            return CharacterManager._instance;
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
            hpSprites[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("2D/Heart_sprite_empty");
        }

        for (int i = 0; i < playerHP; i++) {
            hpSprites[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("2D/Heart_sprite");
        }
    }
}
