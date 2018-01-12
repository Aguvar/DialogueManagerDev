using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
[System.Serializable]
public class SpeakableText : ScriptableObject {

    public string speaker;
    public float charDelay;
    [TextArea(3,5)]
    public string text;



}
