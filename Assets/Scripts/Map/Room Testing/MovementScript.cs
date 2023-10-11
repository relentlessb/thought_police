using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField] Camera cameraObject;
    Camera MainCamera;
    Rigidbody2D playerPhys;
    bool movementDisabled = false;
    float timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        playerPhys = gameObject.GetComponent<Rigidbody2D>();
        if (Camera.main!= null)
        {
            cameraObject = Camera.main;
            cameraObject.transform.parent = this.transform;
            cameraObject.transform.localPosition = new Vector3(0, 0, -10);
        }
        else
        {
            MainCamera = Instantiate(cameraObject, transform.parent = this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        playerPhys.AddForce(Vector2.zero);
        if(movementDisabled == false)
        {
            Vector2 playerMovement = new Vector2((Input.GetAxis("Horizontal") * 5), (Input.GetAxis("Vertical") * 5));
            playerPhys.velocity = playerMovement;
        }
        else
        {
            if (timer < .5)
            {
                timer += Time.deltaTime;
            }
            else
            {
                movementDisabled= false;
                GetComponent<SpriteRenderer>().color = Color.white;
                timer = 0;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            movementDisabled= true;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}
