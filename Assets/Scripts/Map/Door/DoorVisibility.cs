using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorVisibility : MonoBehaviour
{
    [SerializeField] float enableDistance;
    Transform player;
    SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerDoorTrigger")
        {
            player = collision.gameObject.GetComponent<Transform>();
            sprite.enabled = false;
        }
    }
    private void Update()
    {
        if (player!= null && sprite.enabled == false)
        {
            if(Vector2.Distance(transform.position, player.position) > enableDistance)
            {
                sprite.enabled = true;
            };
        }
    }
}
