using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class RepeatSprite : MonoBehaviour {
    public Texture TextureToRepeat = null;
    public float TextureSize = 1.0f;
    public bool RepeatX = true;
    public bool RepeatY = true;
    
    private MeshRenderer spriteRenderer;
    private static Material MaterialRef = null;
    private Material materialInstance = null;

    void Start () {
        spriteRenderer = GetComponent<MeshRenderer>();
        if (MaterialRef == null)
        {
            MaterialRef = (Material)Resources.Load("SpriteRepeat", typeof(Material));
        }
        if (materialInstance == null)
        {
            materialInstance = new Material(MaterialRef);
            materialInstance.mainTexture = TextureToRepeat;
            spriteRenderer.sharedMaterial = materialInstance;
        }
        FitScale();
    }

    void FitScale() {
        float textureRatio = materialInstance.mainTexture.width / materialInstance.mainTexture.height;
        TextureSize = Mathf.Max(0.01f, TextureSize);
        Vector2 TextureScale = new Vector2(textureRatio, 1f) * TextureSize;
        materialInstance.mainTextureScale = 
            new Vector2(
                RepeatX ? transform.lossyScale.x / TextureScale.x : 1,
                RepeatY ? transform.lossyScale.y / TextureScale.y : 1);
    }
    
	void Update () {
#if UNITY_EDITOR
        if (materialInstance.mainTexture != TextureToRepeat) {
            materialInstance.mainTexture = TextureToRepeat;
        }
        FitScale();
#endif
    }
}
