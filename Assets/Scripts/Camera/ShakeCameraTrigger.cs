using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCameraTrigger : MonoBehaviour
{
    public GameObject Camera;
    void Update()
    {
        Camera.GetComponent<ShakeCamera>().enabled = true;
    }
}
