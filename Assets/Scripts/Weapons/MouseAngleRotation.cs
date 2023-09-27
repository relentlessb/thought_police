using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MouseAngleRotation : MonoBehaviour
{
    public float timer = 0;
    void Update()
    {
        Vector3 mousePos = Vector3.zero;
        if (Camera.main != null)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        Vector2 direction = new Vector2((mousePos.x-transform.position.x), (mousePos.y-transform.position.y));
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            timer+=Time.deltaTime;
            if (timer > 1)
            {
                timer = 0;
                transform.up = direction;
            }
        }
        else if (timer > 1 || timer == 0)
        {
            timer = 0;
            transform.up = direction; 

        }
        else { timer+= Time.deltaTime; }
    }
}
