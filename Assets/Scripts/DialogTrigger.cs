using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

struct DialogConfig {
    string textToSay;
    float dialogLength;
    AudioClip audio;
}

enum DialogType {
    FirstEncounter, NotTheRightWeapon
};

public class DialogTrigger : MonoBehaviour {

    Dictionary<DialogType, DialogConfig[]> dialogs = new Dictionary<DialogType, DialogConfig[]>();

    public List<AudioClip> audioSources;

    //DEBUG//
    //public AudioClip test;

    private bool _countdownON = false;
    private float _globalDialogLength;

    GameObject _grandFather;
    GameObject dialogText;

    void Start () {
        _grandFather = GameObject.Find("GrandFather");
        dialogText = GameObject.Find("Text : DialogText");

       //dialogs.Add(DialogType.FirstEncounter, new DialogConfig("lol", 2f, audioSources[0]));
	}

	void Update () {
        //DEBUG//
        /*if (Input.GetKeyDown(KeyCode.E)) {
            Display("Tu ne vas pas au bon endroit !", 3f);

            _grandFather.GetComponent<AudioSource>().PlayOneShot(test);
        }*/

        //countdown telling when to removes the displaying
        if (_countdownON) {
            _globalDialogLength -= Time.deltaTime;
            if (_globalDialogLength <= 0f) {
                dialogText.GetComponent<Animator>().SetBool("FadeIn", false);
                dialogText.GetComponent<Animator>().SetBool("FadeOut", true);
                _grandFather.GetComponent<MeshRenderer>().enabled = false;

                _countdownON = false;
            }
        }
	}

    void OnTriggerEnter(Collider _col) {
        if (_col.gameObject.tag.Equals("Player")) {
            //reset the fadeout bool previously changed to true
            dialogText.GetComponent<Animator>().SetBool("FadeOut", false);

            //PlayDialog(dialogs[0]);
        }
    }

    /*void PlayDialog(DialogConfig _config) {
        Display(_config.textToSay, _config.dialogLength);

        _grandFather.GetComponent<AudioSource>().PlayOneShot(_config.audio);
    }*/

    void Display(string _textToSay, float _dialogLength) {
        _grandFather.GetComponent<MeshRenderer>().enabled = true;

        //sets the text's string
        dialogText.GetComponent<Text>().text = _textToSay;

        //launches the animation
        dialogText.GetComponent<Animator>().SetBool("FadeIn", true);

        //sets the timer and launches it
        _globalDialogLength = _dialogLength;
        _countdownON = true;
    }
}