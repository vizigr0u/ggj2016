using UnityEngine;
using System.Collections;
using UnityEditor;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class AttachGrass : MonoBehaviour {
    public Texture grassTexture = null;

    private Shader displayShader = null;
    private MeshRenderer grassRenderer;
    private Material materialInstance = null;

    void Start () {
        grassRenderer = GetComponent<MeshRenderer>();
        if (displayShader == null)
            displayShader = Shader.Find("Standard");
        if (materialInstance == null)
        {
            materialInstance = new Material(displayShader);
            materialInstance.mainTexture = grassTexture;
            grassRenderer.sharedMaterial = materialInstance;
        }
        // hide ground white block
        transform.parent.GetComponent<MeshRenderer>().enabled = !Application.isPlaying;
        FitParentTransform();
    }

    void FitParentTransform() {
        grassRenderer.sharedMaterial.mainTextureScale = new Vector2(transform.parent.lossyScale.x, 1);
    }
    
	void Update () {
#if UNITY_EDITOR
        FitParentTransform();
#endif
    }
}
