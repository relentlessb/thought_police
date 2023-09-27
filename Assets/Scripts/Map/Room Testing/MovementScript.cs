using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField] Camera cameraObject;
    Camera MainCamera;
    Rigidbody2D playerPhys;
    
    // Start is called before the first frame update
    void Start()
    {
        playerPhys = gameObject.GetComponent<Rigidbody2D>();
        MainCamera = Instantiate(cameraObject, transform.parent = this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerMovement = new Vector2((Input.GetAxis("Horizontal") * 5), (Input.GetAxis("Vertical") * 5));
        playerPhys.velocity = playerMovement;
    }
}
