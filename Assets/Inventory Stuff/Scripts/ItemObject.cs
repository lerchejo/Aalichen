using UnityEngine;


public abstract class ItemObject : ScriptableObject
{
    public Sprite Sprite;
    public int Stack;

    [TextArea(15,20)]
    public string description;

    public int Price = 5;
}
