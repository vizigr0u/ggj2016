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
    public WeaponConfiguration weaponConfig;
    public bool canBeTouched = true;

    [HideInInspector]
    public int playerHP = 5;

    private float _invincibleCountdown = 3f;

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

    void Update() {
        if (!PlayerManager.Instance.canBeTouched) {
            _invincibleCountdown -= Time.deltaTime;

            if (_invincibleCountdown <= 0f) {

                _invincibleCountdown = 2f;
                PlayerManager.Instance.canBeTouched = true;
            }
        }
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
