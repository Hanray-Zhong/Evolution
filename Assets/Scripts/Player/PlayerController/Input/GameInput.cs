using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameInput : MonoBehaviour {
    public abstract Vector2 GetMoveDir();
    public abstract float GetInputInteraction();
    
}
