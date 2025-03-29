using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public ColorType colorType;
    public int scoreValue = 0;

    public enum ColorType
    {
        Red,
        Green, 
        Black
    }
}
