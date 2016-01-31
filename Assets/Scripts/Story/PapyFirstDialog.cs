using UnityEngine;
using System.Collections;

public class PapyFirstDialog : MonoBehaviour {
    void OnTriggerEnter2D(Collider2D _col)
    {
        if (_col.gameObject.tag.Equals("Player"))
        {
            var controller = _col.gameObject.GetComponent<PlayerController>();
            controller.CanMove = false;
        }
    }
}
