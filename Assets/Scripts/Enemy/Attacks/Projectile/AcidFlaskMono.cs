using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AcidFlaskMono : MonoBehaviour
{
    float timer;
    [SerializeField] float airtime;
    [SerializeField] int damage;
    [SerializeField] float knockback;
    [SerializeField] AudioSource breakSound;
    bool audioPlayed = false;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= airtime && audioPlayed == false)
        {
            breakFlask();
        }
        if (breakSound.isPlaying)
        {
            audioPlayed = true;
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else if (!breakSound.isPlaying)
        {
            if (audioPlayed)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!audioPlayed)
        {
            if(collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * knockback;
                collision.gameObject.GetComponent<HealthManager>().currentHP -= damage;
                collision.gameObject.GetComponent<HealthManager>().SetHealthBar();
            }
            breakFlask();
        }
    }
    void breakFlask()
    {
        breakSound.Play();
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
