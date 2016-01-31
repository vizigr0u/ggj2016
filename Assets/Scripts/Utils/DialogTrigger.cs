using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DialogTrigger : MonoBehaviour {
    struct DialogConfig
    {
        public string text;
        public float duration;
        public string audio;

        public DialogConfig(string text, float duration, string audio)
        {
            this.text = text;
            this.duration = duration;
            this.audio = audio;
        }
    }

    public enum DialogType
    {
        FirstEncounter, NotTheRightWeapon
    };

    static private Dictionary<DialogType, DialogConfig[]> Dialogs = new Dictionary<DialogType, DialogConfig[]> {
        { DialogType.FirstEncounter, new DialogConfig[] { new DialogConfig("Salut c'est moi papy", 4, "JeTeLavaisDit") } }
    };
    
    public DialogType Dialog;

    //DEBUG//
    //public AudioClip test;

    private bool _countdownON = false;
    private float _globalDialogLength;

    GameObject _grandFather;
    GameObject dialogText;

    void Start () {
        _grandFather = GameObject.Find("GrandFather");
        dialogText = GameObject.Find("Text : DialogText");
	}

	void Update () {
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

    void OnTriggerEnter2D(Collider2D _col) {
        if (_col.gameObject.tag.Equals("Player")) {
            //reset the fadeout bool previously changed to true
            dialogText.GetComponent<Animator>().SetBool("FadeOut", false);
            var configs = Dialogs[Dialog];
            var randConfig = configs[Random.Range(0, configs.Length)];
            PlayDialog(randConfig);
        }
    }

    void PlayDialog(DialogConfig config) {
        Display(config);

        var audio = (AudioClip)Resources.Load(config.audio, typeof(AudioClip));
        _grandFather.GetComponent<AudioSource>().PlayOneShot(audio);
    }

    void Display(DialogConfig config) {
        _grandFather.GetComponent<MeshRenderer>().enabled = true;

        //sets the text's string
        dialogText.GetComponent<Text>().text = config.text;

        //launches the animation
        dialogText.GetComponent<Animator>().SetBool("FadeIn", true);

        //sets the timer and launches it
        _globalDialogLength = config.duration;
        _countdownON = true;
    }
}