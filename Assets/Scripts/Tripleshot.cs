using UnityEngine;

public class Tripleshot : MonoBehaviour
{
    void Update()
    {
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }       
    }
}
