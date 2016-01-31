using UnityEngine;
using System.Collections;

public class GotoLevel : MonoBehaviour {
    public string LevelToLoad = "level1";

    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.gameObject.tag.Equals("Player"))
        {
            SceneFadeInOut.Instance.StartFadeOut(LevelToLoad);
        }
    }
}
