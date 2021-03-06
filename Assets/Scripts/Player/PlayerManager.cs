﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    public List<WeaponConfiguration> weaponConfigs;

    public int playerHP = 5;

    private float _invincibleCountdown = 3f;

    void Awake() {
        if (_instance == null) _instance = this;
        else {
            DestroyImmediate(this);
            return;
        }

        if (SceneManager.GetActiveScene().name == "Introduction") {
            PlayerManager.Instance.weaponConfig = PlayerManager.Instance.weaponConfigs[0];
        }

        if (SceneManager.GetActiveScene().name == "level1") {
            PlayerManager.Instance.weaponConfig = PlayerManager.Instance.weaponConfigs[1];
            GetComponent<PlayerController>().canJump = false;
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
        if (playerHP <= 0) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        playerHP = Mathf.Clamp(playerHP, 0, 5);

        for (int i = 0; i < hpSprites.Count; i++) {
            hpSprites[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Heart_sprite_empty");
        }

        for (int i = 0; i < playerHP; i++) {
            hpSprites[i].GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Heart_sprite");
        }
    }
}
