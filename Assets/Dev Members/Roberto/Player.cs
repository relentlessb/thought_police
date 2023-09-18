using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Dictionary<string, float> currentStats;

    private void Start()
    {
        currentStats = new Dictionary<string, float>()
        {
            { "Determination", 3 },
            { "Confidence", 5 },
            { "Wit", 3},
            { "Morale", 0 },
            { "Focus", 0 },
            { "Damage", 5 },
            { "Size", 1 },
            { "Offset X", 0 },
            { "Offset Y", 0 },
            { "Speed", 1 }
        };
}
    public int currentChar = 0;

    private void Update()
    {
        Vector3 playerMovement = new Vector3((Input.GetAxis("Horizontal") * currentStats["Wit"] * Time.deltaTime), (Input.GetAxis("Vertical") * currentStats["Wit"] * Time.deltaTime), 0);
        transform.Translate(playerMovement);
    }
    void OnTriggerEnter2D(UnityEngine.Collider2D other)
    {
        Debug.Log("Trigger Enter");
        if (other.gameObject.CompareTag("Ability"))
        {
            Dictionary<string, float> stats = AbilityList.getStatAbility((int)Variables.Object(other.gameObject).Get("abilityID"));
            transform.parent.gameObject.GetComponent<MainCharacterHandler>().addStats(stats, currentStats);
        }
    }
}
