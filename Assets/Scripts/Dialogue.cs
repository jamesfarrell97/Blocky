using UnityEngine;

// Code referenced: https://www.youtube.com/watch?v=_nRzoTzeyxU&t=89s&ab_channel=Brackeys
//
//
//

[System.Serializable]
public class Dialogue
{
    public string title;

    [TextArea(3, 10)]
    public string[] setences;
}
