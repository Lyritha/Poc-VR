using UnityEngine;

[DisallowMultipleComponent]
public class TooltipNote : MonoBehaviour
{
    [TextArea(2, 5)]
    public string note;

    [HideInInspector]
    public bool locked = false;
}