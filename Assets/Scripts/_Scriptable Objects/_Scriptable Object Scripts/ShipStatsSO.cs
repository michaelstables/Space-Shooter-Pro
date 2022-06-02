using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_Stats", menuName = "Scriptable Objects / Ship Stats")]
public class ShipStatsSO : ScriptableObject
{
    public GameObject laser;
    public GameObject tripleshotLaser;
    public GameObject currentLaser;

    public int lives;
    public int moveSpeed;
    public int boostedMoveSpeed;
    public float fireRate;
}