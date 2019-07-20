using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanetoTools;

public class MoveCamera : MonoBehaviour
{
    
    private Vector2 MousePosition;
    private Vector2 relativePos;

    public BoxCollider2D Edge;
    private float edgeHeight;
    private float edgeWidth;
    private float cameraHeight;
    private float cameraWidth;
    
    private void Start() {
        Init();
    }

    void Update() {
        // Debug.Log(Input.mousePosition);
        MousePosition = new Vector2(Mathf.Clamp(Input.mousePosition.x, 0, Screen.width), Mathf.Clamp(Input.mousePosition.y, 0, Screen.height)) - new Vector2(Screen.width / 2, Screen.height / 2);
        relativePos = MousePosition * ((edgeWidth - cameraWidth) / Screen.width);
        // Debug.Log(cameraWidth);
        gameObject.transform.SetPositionAndRotation(new Vector3(relativePos.x, relativePos.y, gameObject.transform.position.z), Quaternion.identity);
    }


    void Init() {
        edgeHeight = Edge.bounds.max.y - Edge.bounds.min.y;
        edgeWidth = Edge.bounds.max.x - Edge.bounds.min.x;
        // Debug.Log(edgeWidth);
        Vector3 Screen_max = new Vector3(Screen.width, Screen.height, 0);
        cameraHeight = 2 * gameObject.GetComponent<Camera>().ScreenToWorldPoint(Screen_max).y;
        cameraWidth = 2 * gameObject.GetComponent<Camera>().ScreenToWorldPoint(Screen_max).x;
    }
    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(1, 0, 0, 1);
        Vector3 a = new Vector3((cameraWidth) / 2, (cameraHeight) / 2, 0);
        Vector3 b = new Vector3((cameraWidth) / 2, -(cameraHeight) / 2, 0);
        Vector3 c = new Vector3(-(cameraWidth) / 2, -(cameraHeight) / 2, 0);
        Vector3 d = new Vector3(-(cameraWidth) / 2, (cameraHeight) / 2, 0);
        
        Gizmos.DrawLine(a, b);
        Gizmos.DrawLine(a, d);
        Gizmos.DrawLine(c, b);
        Gizmos.DrawLine(c, d);
        
    }
}
