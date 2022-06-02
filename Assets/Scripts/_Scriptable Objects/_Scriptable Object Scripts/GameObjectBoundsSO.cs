using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_Bounds", menuName = "Scriptable Objects / Bounds")]

public class GameObjectBoundsSO : ScriptableObject
{
    public float maxYPosition;
    public float minYPosition;
    public float maxXPosition;
    public float minXPosition; 
}