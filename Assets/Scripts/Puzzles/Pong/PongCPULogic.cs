using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongCPULogic : MonoBehaviour
{
    [SerializeField] Rigidbody2D paddlePhys;
    [SerializeField] PongBallScript ball;

    float cpuStartSpeed = 3;
    float correctDirectionChance = .75f;
    float decideTime = .4f;

    bool ballAbove = false;
    int randomDirection;
    float speedMultiplier = 1;
    string lastDirection = "down";
    float decisionTimer = 0;

    private void Start()
    {
        if (correctDirectionChance > 1)
        {
            correctDirectionChance = 1;
        }
    }
    void Update()
    {
        if (ball.score)
        {
            switch (ball.playerScore - ball.cpuScore)
            {
                case -3: cpuStartSpeed = 1; correctDirectionChance = .3f; decideTime = 1; break;
                case -2: cpuStartSpeed = 2; correctDirectionChance = .4f; decideTime = .75f; break;
                case -1: cpuStartSpeed = 3; correctDirectionChance = .5f; decideTime = .5f; break;
                case 0: cpuStartSpeed = 3; correctDirectionChance = .75f; decideTime = .3f; break;
                case 1: cpuStartSpeed = 3; correctDirectionChance = .8f; decideTime = .25f; break;
                case 2: cpuStartSpeed = 4; correctDirectionChance = .9f; decideTime = .25f; break;
                case 3: cpuStartSpeed = 4.5f; correctDirectionChance = .95f; decideTime = .2f; break;
                default: break;
            }
        }
        decisionTimer += Time.deltaTime;
        if (decisionTimer > decideTime)
        {
            decisionTimer = 0;
            if (ball.transform.position.y > transform.position.y)
            {
                ballAbove = true;
            }
            else
            {
                ballAbove = false;
            }
            if (correctDirectionChance != 1)
            {
                randomDirection = Random.Range(1, 101);
            }
            else
            {
                randomDirection = 0;
            }
            if (randomDirection <= correctDirectionChance * 100)
            {
                if (ballAbove)
                {
                    paddlePhys.velocity = new Vector2(0, cpuStartSpeed * speedMultiplier);
                    if (lastDirection == "up")
                    {
                        speedMultiplier += .1f;
                    }
                    else
                    {
                        speedMultiplier = 1;
                    }
                    lastDirection = "up";
                }
                else
                {
                    paddlePhys.velocity = new Vector2(0, -cpuStartSpeed * speedMultiplier);
                    if (lastDirection == "down")
                    {
                        speedMultiplier += .1f;
                    }
                    else
                    {
                        speedMultiplier = 1;
                    }
                    lastDirection = "down";
                }
            }
            else
            {
                if (ballAbove)
                {
                    paddlePhys.velocity = new Vector2(0, -cpuStartSpeed * speedMultiplier);
                    if (lastDirection == "down")
                    {
                        speedMultiplier += .1f;
                    }
                    else
                    {
                        speedMultiplier = 1;
                    }
                    lastDirection = "down";
                }
                else
                {
                    paddlePhys.velocity = new Vector2(0, cpuStartSpeed * speedMultiplier);
                    if (lastDirection == "up")
                    {
                        speedMultiplier += .1f;
                    }
                    else
                    {
                        speedMultiplier = 1;
                    }
                    lastDirection = "up";
                }
            }
        }
    }
}
