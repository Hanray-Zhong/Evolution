using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Impulse : MonoBehaviour
{
    public abstract void ImpulseInteraction(GameObject other, Vector2 Dir, float velocity);
}
