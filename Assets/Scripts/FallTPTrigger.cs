using UnityEngine;
using System.Collections;

public class FallTPTrigger : MonoBehaviour {
    public GameObject StartPos;

    void OnTriggerEnter2D(Collider2D _col) {
        if (_col.gameObject.tag.Equals("Player")) {
            _col.gameObject.transform.position = StartPos.transform.position;
        }
    }
}
