using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public Rigidbody2D actorPhys;
    public Vector3 startPosition;
    public List<BaseCutsceneAction> actions;
    public List<float> actionTimes;
    public enum actionState
    {
        ready,
        inProgress,
        slowText
    }
    public actionState state = actionState.ready;
    public int currentAction = 0;
    public float actionTimer = 0;
    public float totalMoveTime;
}
