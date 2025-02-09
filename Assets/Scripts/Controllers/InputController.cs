using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// The fact that it's abstract: We are to create instances of these objects.
public abstract class InputController : ScriptableObject
{
    public abstract float RetrieveMoveInput();
    public abstract bool RetrieveJumpInput();
    public abstract bool RetrieveJumpHoldInput();
    public abstract bool RetrieveTridentThrowInput();
    public abstract bool RetrieveTridentRecallInput();
}
