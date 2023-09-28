using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveHolder : MonoBehaviour
{
    public BaseAbility ability;
    float cooldownTime;
    float activeTime;
    enum abilityState
    {
        ready,
        active,
        cooldown,
    }
    abilityState state = abilityState.ready;

    [SerializeField] KeyCode key;

    private void Update()
    {
        switch (state)
        {
            case abilityState.ready:
                if (Input.GetKeyDown(key))
                {
                    ability.ActiveAbility(GetComponent<Player>());
                    state = abilityState.active;
                    activeTime = ability.activeAbilityTime;
                }
                break;
            case abilityState.active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    state = abilityState.cooldown;
                    activeTime = ability.activeAbilityTime;
                    cooldownTime = ability.activeCooldownTime;
                }
                break;
            case abilityState.cooldown:
                GetComponent<Player>().canMove = true;
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = abilityState.ready;
                    cooldownTime = ability.activeCooldownTime;
                }
                break;
        }
    }
}
