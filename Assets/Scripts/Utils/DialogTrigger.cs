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
        IceFail, JumpChest, Fuis, Inoffensif, Monte, BonChemin
    };

    static private Dictionary<DialogType, DialogConfig[]> Dialogs = new Dictionary<DialogType, DialogConfig[]> {
        { DialogType.IceFail, new DialogConfig[] { new DialogConfig("Oh ! J'aurai dû te prévenir, à l'époque de Bork la glace, il faisait bien plus froid.. Bonne chance !", 7.5f, "JAuraisDuTePrevenir...Bork") } },
        { DialogType.JumpChest, new DialogConfig[] { new DialogConfig("Tu devrais sauter. Tu pourrais trouver un trésor !", 3f, "TuDevraisSauter....") } },
        { DialogType.Fuis, new DialogConfig[] { new DialogConfig("Fuis ! Tu ne peux pas le vaincre !", 3f, "FuisPeuxPasVaincre") } },
        { DialogType.Inoffensif, new DialogConfig[] { new DialogConfig("Celui-là est inoffensif.", 2f, "CeluiLaInnoffensif") } },
        { DialogType.Monte, new DialogConfig[] { new DialogConfig("Il y a peut-être quelque chose d'intéressant en haut... Oui, les souvenirs me reviennent, grimpe.", 5f, "Peut-EtreQuelqueChoseInteressant") } },
        { DialogType.BonChemin, new DialogConfig[] { new DialogConfig("Tu es... en bonne voie.", 2f, "TuEsEnBonneVoie2") } },
    };
    
    public DialogType Dialog;

    //DEBUG//
    //public AudioClip test;

    private bool _countdownON = false;
    private float _globalDialogLength;
    private bool _needToPlay = true;

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
                _grandFather.GetComponent<SpriteRenderer>().enabled = false;

                _countdownON = false;
            }
        }
	}

    void OnTriggerEnter2D(Collider2D _col) {
        if (_col.gameObject.tag.Equals("Player") && _needToPlay) {
            //reset the fadeout bool previously changed to true
            dialogText.GetComponent<Animator>().SetBool("FadeOut", false);
            var configs = Dialogs[Dialog];
            var randConfig = configs[Random.Range(0, configs.Length)];
            PlayDialog(randConfig);
            _needToPlay = false;
        }
    }

    void PlayDialog(DialogConfig config) {
        Display(config);

        var audio = (AudioClip)Resources.Load("Voices/" + config.audio, typeof(AudioClip));
        _grandFather.GetComponent<AudioSource>().PlayOneShot(audio);
    }

    void Display(DialogConfig config) {
        _grandFather.GetComponent<SpriteRenderer>().enabled = true;

        //sets the text's string
        dialogText.GetComponent<Text>().text = config.text;

        //launches the animation
        dialogText.GetComponent<Animator>().SetBool("FadeIn", true);

        //sets the timer and launches it
        _globalDialogLength = config.duration;
        _countdownON = true;
    }
}