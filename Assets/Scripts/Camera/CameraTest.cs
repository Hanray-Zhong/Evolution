using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log(Input.mousePosition);
            Debug.Log(GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, GetComponent<Camera>().farClipPlane)));
            Debug.Log(GetComponent<Camera>().farClipPlane);

            Ray ray = new Ray();
            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo);
        }
    }
}
