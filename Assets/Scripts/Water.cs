using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Underground : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cannon"))
            Destroy(other.gameObject);
    }
}
