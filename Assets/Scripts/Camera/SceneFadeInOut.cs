using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SceneFadeInOut : MonoBehaviour
{
    public float fadeSeconds = 1.5f;          // Speed that the screen fades to and from black.
    public string NextLevel = null;
    public bool FadeInOnLoad = true;

    enum FadingState
    {
        In, Out, Idle
    };
    private float FadeStartTime = 0.0f;
    Color TargetColor = Color.clear;
    private Image _image;
    private FadingState state = FadingState.Idle;      // Whether or not the scene is still fading in.
    public static SceneFadeInOut Instance = null;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        // Set the texture so that it is the the size of the screen and covers it.
        _image = GetComponent<Image>();
        _image.enabled = false;
    }

    void Start()
    {
        if (FadeInOnLoad)
            StartFadeIn();
    }

    void Update()
    {
        if (state == FadingState.Idle)
            return;
        float progress = Mathf.Clamp((Time.time - FadeStartTime) / fadeSeconds, 0.0f, 1.0f);
        bool FadeIsOver = progress > 0.95f;
        _image.color = Color.Lerp(_image.color, TargetColor, progress * progress);

        if (FadeIsOver)
        {
            if (state == FadingState.In)
            {
                _image.color = Color.clear;
                _image.enabled = false;
                state = FadingState.Idle;
            }
            else
            {
                // load next level
                if (NextLevel != null)
                {
                    SceneManager.LoadScene(NextLevel);
                }
                else
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }
    
    public void StartFadeIn()
    {
        if (!_image.enabled)
        {
            Color tmp = Color.black;
            tmp.a = 1.0f;
            _image.color = tmp;
            _image.enabled = true;
        }
        TargetColor = Color.clear;
        FadeStartTime = Time.time;
        state = FadingState.In;
    }

    public void StartFadeOut(string LevelToLoadOverride = null)
    {
        if (LevelToLoadOverride != null)
        {
            NextLevel = LevelToLoadOverride;
        }
        if (!_image.enabled)
        {
            Color tmp = Color.clear;
            tmp.a = 0.0f;
            _image.color = tmp;
            _image.enabled = true;
        }
        TargetColor = Color.black;
        FadeStartTime = Time.time;
        state = FadingState.Out;
    }
}