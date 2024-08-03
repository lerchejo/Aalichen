using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class CutSceneDialogObjectCollection : ScriptableObject
{
    public List<CutScenesDialogObject> DialogObjects = new();
    
    
    public String GetDialog(int index)
    {
        if(index >= DialogObjects.Count)
        {
            return null;
        }
        return DialogObjects[index].ContentHolder;
    }
    
    public Sprite GetImage(int index)
    {
        if(index >= DialogObjects.Count)
        {
            return null;
        }
        return DialogObjects[index].BackDrop;
    }
    
}


[Serializable]
public class CutScenesDialogObject
{
    
    [TextArea(8,10)]public string ContentHolder;
    public Sprite BackDrop = null;
}

