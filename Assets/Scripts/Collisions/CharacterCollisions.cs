using UnityEngine;
using System.Collections;

public class CharacterCollisions : MonoBehaviour {

	void OnTriggerEnter(Collider _col) {
        if (_col.gameObject.tag.Equals("LifeCollectible")) {
            CharacterManager.Instance.UpdateLife(-1);
            Destroy(_col.gameObject);
        }
    }
}
