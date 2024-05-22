using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog")]
public class DialogObject : ScriptableObject
{
    public string DialogName;

    [TextArea(15, 20)] public string EntryText;
    [TextArea(15, 20)] public string ExitText;

    public string yesText;
    public string noText;
}
