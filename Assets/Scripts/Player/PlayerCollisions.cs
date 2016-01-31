using UnityEngine;
using System.Collections;

public class PlayerCollisions : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D _col) {
        if (_col.gameObject.tag.Equals("LifeCollectible")) {
            PlayerManager.Instance.UpdateLife(-1);
            Destroy(_col.gameObject);
        }
    }
}
