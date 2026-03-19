using System;
using UnityEngine;

public class Ex5Bis : MonoBehaviour
{
    [Flags]
    public enum Color
    {
        Red = 1 ,
        Green = 2,
        Blue = 4,
    }
    
    public Color currentColor;
    void Start()
    {
        switch (currentColor)
        {
            case Color.Red | Color.Green | Color.Blue:
                Debug.Log("White");
                break;
            case  Color.Red | Color.Green:
                Debug.Log("Yellow");
                break;
            case  Color.Blue | Color.Green:
                Debug.Log("Cyan");
                break;
            case  Color.Red | Color.Blue:
                Debug.Log("Magenta");
                break;
            default:
                Debug.Log(currentColor);
                break;
        }
    }

}
