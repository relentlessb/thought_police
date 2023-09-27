using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateDoor : MonoBehaviour
{
    [SerializeField] GameObject door;
    void Start()
    {
        GameObject thisDoor = Instantiate(door, transform.parent = this.transform);
    }
}
