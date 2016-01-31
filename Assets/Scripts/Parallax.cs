using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {
    public float SpeedMultiplier = 0.5f;
    public float LeftBorder = 1.0f;
    public float RightBorder = 1.0f;

    private float rightMostX;
    private float leftMostX;
    private Vector3 lastCameraPos;
    
	void Start () {
        lastCameraPos = Camera.main.transform.position;
        GetExtremumOffsets();
    }

    void GetExtremumOffsets() {
        rightMostX = 0.0f;
        leftMostX = 0.0f;
        foreach (Transform t in transform) {
            float width = t.localScale.x;
            float leftX = t.localPosition.x - width / 2;
            float rightX = t.localPosition.x + width / 2;
            if (leftX < leftMostX) {
                leftMostX = leftX;
            }
            if (rightX > rightMostX) {
                rightMostX = rightX;
            }
        }
    }

    void OnDrawGizmos() {
        DrawGizmoLines(Color.grey);
    }

    void OnDrawGizmosSelected() {
        DrawGizmoLines(Color.blue);
    }

    void DrawGizmoLines(Color color) {
        float halfLength = 2.0f;
        if (leftMostX == rightMostX)
        {
            GetExtremumOffsets();
        }
        Gizmos.color = color;
        Vector3 RightMost = transform.position + (transform.right * (rightMostX + RightBorder));
        Vector3 LeftMost = transform.position + (transform.right * (leftMostX - LeftBorder));
        Gizmos.DrawLine(LeftMost - Vector3.up * halfLength, LeftMost + Vector3.up * halfLength);
        Gizmos.DrawLine(RightMost - Vector3.up * halfLength, RightMost + Vector3.up * halfLength);
        Gizmos.DrawLine(LeftMost - Vector3.up * halfLength, RightMost - Vector3.up * halfLength);
        Gizmos.DrawLine(RightMost + Vector3.up * halfLength, LeftMost + Vector3.up * halfLength);
    }
	
	void Update () {
        Vector3 posd = Camera.main.transform.position - lastCameraPos;
        lastCameraPos = Camera.main.transform.position;
        transform.position += posd * SpeedMultiplier;

        float rightOffset = rightMostX + RightBorder;
        float leftOffset = leftMostX - LeftBorder;
        if (posd.x > 0) { // Camera moving right
            Vector3 RightMostViewPos = Camera.main.WorldToViewportPoint(transform.position + transform.right * rightOffset);
            if (RightMostViewPos.x < 0)
            {
                float viewportRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, RightMostViewPos.z)).x;
                transform.position = new Vector3(viewportRight, transform.position.y, transform.position.z);
                transform.position -= transform.right * leftOffset;
            }
        } else {
            Vector3 LeftMostViewPos = Camera.main.WorldToViewportPoint(transform.position + transform.right * leftOffset);
            if (LeftMostViewPos.x > 1)
            {
                float viewportLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, LeftMostViewPos.z)).x;
                transform.position = new Vector3(viewportLeft, transform.position.y, transform.position.z);
                transform.position -= transform.right * rightOffset;
            }
        }
    }
}
