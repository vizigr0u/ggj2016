using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

    public Transform playerTransform;
    public Transform crouchPOV;
    public float xMargin = 1f;
    public float yMargin = 1f;
    public float xySmooth = 3f;
    public float crouchDurationToPOV = 3f;
    public Transform maxXY;
    public Transform minXY;

    private float _targetX;
    private float _targetY;

    PlayerController m_Controller;

    float countdown = 0f;

    void Start() {
        m_Controller = playerTransform.GetComponent<PlayerController>();
    }

    void Update() {
        if (m_Controller._isCrouching) {
            countdown += Time.deltaTime;

            if (countdown >= crouchDurationToPOV) Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, crouchPOV.position, xySmooth * Time.deltaTime);
        }
        else {
            countdown = 0f;
        }
    }

    void LateUpdate() {
        if (!m_Controller._isCrouching) TrackBasic();

        TrackPlayer();
    }

    void TrackBasic() {
        // Move the camera to its basic position (pivot's one)
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, transform.position, xySmooth * Time.deltaTime);
    }

    void TrackPlayer() {
        if (CheckXMargin()) {
            _targetX = Mathf.Lerp(transform.position.x, playerTransform.position.x, xySmooth * Time.deltaTime);
        }
        if (CheckYMargin()) {
            _targetY = Mathf.Lerp(transform.position.y, playerTransform.position.y, xySmooth * Time.deltaTime);
        }

        _targetX = Mathf.Clamp(_targetX, minXY.position.x, maxXY.position.x);
        _targetY = Mathf.Clamp(_targetY, minXY.position.y, maxXY.position.y);

        Vector3 _targetXY = new Vector3(_targetX, _targetY, transform.position.z);
        transform.position = _targetXY;
    }

    bool CheckXMargin() {
        return Mathf.Abs(transform.position.x - playerTransform.position.x) > xMargin;
    }

    bool CheckYMargin() {
        return Mathf.Abs(transform.position.y - playerTransform.position.y) > yMargin;
    }
}
