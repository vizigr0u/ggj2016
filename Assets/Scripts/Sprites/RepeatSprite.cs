using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class RepeatSprite : MonoBehaviour {
    public Texture TextureToRepeat = null;
    public float TextureSize = 1.0f;
    public bool RepeatX = true;
    public bool RepeatY = true;
    public bool RESET = false;
    
    private MeshRenderer spriteRenderer = null;
    private static Material MaterialRef = null;
    [SerializeField]private Material materialInstance = null;

    void Start () {
        Init();
    }

    void Init(bool ForceNewMat = false)
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<MeshRenderer>();
        }
        if (MaterialRef == null)
        {
            MaterialRef = (Material)Resources.Load("Materials/SpriteRepeat", typeof(Material));
        }
        if (materialInstance == null || ForceNewMat)
        {
            if (!Application.isPlaying)
            {
                materialInstance = new Material(MaterialRef);
                spriteRenderer.sharedMaterial = materialInstance;
            }
            else
                materialInstance = spriteRenderer.sharedMaterial;
        }
        if (materialInstance.mainTexture != TextureToRepeat)
        {
            materialInstance.mainTexture = TextureToRepeat;
        }
        FitScale();
    }

    void FitScale() {
        float textureRatio = (float) materialInstance.mainTexture.width / (float) materialInstance.mainTexture.height;
        TextureSize = Mathf.Max(0.01f, TextureSize);
        Vector2 TextureScale = new Vector2(textureRatio, 1f) * TextureSize;
        materialInstance.mainTextureScale = 
            new Vector2(
                RepeatX ? transform.lossyScale.x / TextureScale.x : 1,
                RepeatY ? transform.lossyScale.y / TextureScale.y : 1);
    }

    void Update() {
#if UNITY_EDITOR
        Init(RESET);
        RESET = false;
#endif
    }
}
