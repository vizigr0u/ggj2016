using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

    public Transform _playerTransform;
    public float xMargin = 1f;
    public float yMargin = 1f;
    public float xySmooth = 3f;
    public Transform maxXY;
    public Transform minXY;

    private float _targetX;
    private float _targetY;

    void Start() {

    }

    void LateUpdate() {
        TrackPlayer();
    }

    void TrackPlayer() {
        if (CheckXMargin()) {
            _targetX = Mathf.Lerp(transform.position.x, _playerTransform.position.x, xySmooth * Time.deltaTime);
        }
        if (CheckYMargin()) {
            _targetY = Mathf.Lerp(transform.position.y, _playerTransform.position.y, xySmooth * Time.deltaTime);
        }

        _targetX = Mathf.Clamp(_targetX, minXY.position.x, maxXY.position.x);
        _targetY = Mathf.Clamp(_targetY, minXY.position.y, maxXY.position.y);

        Vector3 _targetXY = new Vector3(_targetX, _targetY, transform.position.z);
        transform.position = _targetXY;
    }

    bool CheckXMargin() {
        return Mathf.Abs(transform.position.x - _playerTransform.position.x) > xMargin;
    }

    bool CheckYMargin() {
        return Mathf.Abs(transform.position.y - _playerTransform.position.y) > yMargin;
    }
}
