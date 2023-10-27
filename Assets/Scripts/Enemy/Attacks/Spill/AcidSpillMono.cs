using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSpillMono : MonoBehaviour
{
    public float timer;
    float totalTime;
    [SerializeField] float acidHurtCooldown; //Time between acid attack hits (0 means every frame, .25 means every 1/4 second, 1 means every second) 
    [SerializeField] int damage;
    [SerializeField] float acidAttackLength;
    [SerializeField] AudioSource acidBubble; //Add bubble sound when we receive it.
    bool hurtPlayer;

    private void FixedUpdate()
    {
        totalTime += Time.deltaTime;
        timer += Time.deltaTime;
        if (timer / acidHurtCooldown > 1)
        {
            hurtPlayer = true;
            timer = 0;
        }
        else
        {
            hurtPlayer = false;
        }
        if((totalTime>acidAttackLength))
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && hurtPlayer)
        {
            
            collision.gameObject.GetComponent<HealthManager>().UpdateHealth(-damage);
            collision.gameObject.GetComponent<HealthManager>().SetHealthBar();
        }
    }
}
