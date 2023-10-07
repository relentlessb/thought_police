using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PongBallScript : MonoBehaviour
{
    [SerializeField] Rigidbody2D ballPhys;
    [SerializeField] float startSpeed = 2f;
    [SerializeField] float collisionMultiplier;
    [SerializeField] TextMeshProUGUI playerScoreText;
    [SerializeField] TextMeshProUGUI cpuScoreText;
    [SerializeField] float scoreDuration = 5;
    [SerializeField] float maxSpeed = 5;

    public int playerScore = 0;
    public int cpuScore = 0;
    public bool score = false;
    GameObject lastCollision = null;
    float timer;
    private void Start()
    {
        ballPhys.velocity = new Vector2(Random.Range(1, 101), Random.Range(1, 101)).normalized*startSpeed;
    }
    private void Update()
    {
        if(score)
        {
            timer += Time.deltaTime;
            if(timer>=scoreDuration)
            {
                timer = 0;
                lastCollision.GetComponent<Animator>().SetBool("Blink", false);
                score = false;
                transform.position = Vector3.zero;
                ballPhys.velocity = new Vector2(Random.Range(1, 101), Random.Range(1, 101)).normalized * startSpeed;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int randomChangeDirection = Random.Range(0, 3);
        float xAddition = 0;
        float yAddition = 0;
        switch (randomChangeDirection)
        {
            case 0: break;
            case 1: xAddition = Random.Range(-2,3); break;
            case 2: yAddition = Random.Range(-2, 3); break;
        }
        ballPhys.velocity = new Vector2(ballPhys.velocity.x+xAddition, ballPhys.velocity.y+yAddition);
        if (ballPhys.velocity.magnitude < maxSpeed)
        {
            ballPhys.velocity *= collisionMultiplier;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Finish")
        {
            ballPhys.velocity = Vector2.zero;
            collision.GetComponent<Animator>().SetBool("Blink", true);
            lastCollision = collision.gameObject;
            if(collision.transform.position.x > 0)
            {
                playerScore += 1;
                playerScoreText.text = playerScore.ToString();
            }
            else
            {
                cpuScore += 1;
                cpuScoreText.text = cpuScore.ToString();
            }
            score = true;
        }
    }
}
