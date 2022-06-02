using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "_Powerup", menuName = "Scriptable Objects / Powerup")]

public class PowerupsSO : ScriptableObject
{
    public int powerupID;
    public float moveSpeed;
    public float minYPosition;
}