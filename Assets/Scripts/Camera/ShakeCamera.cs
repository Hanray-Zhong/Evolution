using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public float ShakeStrength;
    private Vector3 deltaPos = Vector3.zero;

    void Update () {
        transform.localPosition -= deltaPos;
        deltaPos = Random.insideUnitSphere / ShakeStrength;
        transform.localPosition += deltaPos;
    }
    
}
