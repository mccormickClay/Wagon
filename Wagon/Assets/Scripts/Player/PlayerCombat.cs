using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // the current action the player is trying to do.
    CombatStates state;
    CombatStates nextState;
    // The max amount of health the player can have
    float maxHealth;
    // the current health that will be updated continually
    float currHealth;
    // the amount health will regen overtime
    float regenAmountHealth;

    // The largest amount of stamina the player can have
    float maxStamina;
    // current amount of stamina. This all continually be updated
    float currStamina;
    // The amount stamina will regen overtime
    float regenAmountStamina;
    // the minimum amount of stamina needed to be able to attack, block, parry
    float staminaActionMinimal;
    // the amount over time block has on currStamina
    float blockStaminaDecrease;
    // the amount over time parry has on currStamina
    float parryStaminaDecrease;
    // the amount attack takes away from currStamina. This is NOT OVER TIME
    float attackStamina;

    // the amount the player can do to an enemy
    float damage;
    // percentage damage that is blocked when in BLOCK CombatState
    // use 0.0f - 1.0f
    float blockResist;

    float parryTimer;

    float parryTimerHold;
    // The UI stats of Health and Stamina in the Canvas
    public StatsUI stats;

    // Start is called before the first frame update
    void Start()
    {
        state = CombatStates.NONE;
        nextState = CombatStates.NONE;
        maxHealth = 100.0f;
        currHealth = 100.0f;
        regenAmountHealth = 0.1f;

        maxStamina = 100.0f;
        currStamina = 100.0f;
        regenAmountStamina = 0.1f;
        staminaActionMinimal = 20.0f;
        blockStaminaDecrease = 0.2f;
        parryStaminaDecrease = 0.4f;
        attackStamina = 20.0f;

        damage = 20.0f;

        blockResist = 0.5f;

        parryTimer = 0.0f;
        parryTimerHold = 1.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*if (currHealth < maxHealth)
        {
            currHealth += regenAmountHealth;
            stats.UpdateHealthUI(currHealth);
        }*/

        if(parryTimer < 0.0f)
        {
            state = CombatStates.NONE;
            parryTimer = 0.0f;
        }

        switch(GetState())
        {
            case CombatStates.ATTACK:
                Attack();
                break;
            case CombatStates.BLOCK:
                currStamina -= blockStaminaDecrease;
                break;
            case CombatStates.PARRY:
                currStamina -= parryStaminaDecrease;
                parryTimer -= Time.deltaTime;
                break;
            default:
                if (currStamina < maxStamina)
                {
                    currStamina += regenAmountStamina;                 
                }
                break;
        }

        stats.UpdateStaminaUI(currStamina);

        if(currStamina < staminaActionMinimal && GetState() != CombatStates.NONE)
        {
            SetState(CombatStates.NONE);
        }
    }

    public CombatStates GetState()
    {
        return (state);
    }


    void SetState(CombatStates _state)
    {
        if (currStamina >= staminaActionMinimal)
        {
            if (GetState() != CombatStates.PARRY)
            {
                parryTimer = parryTimerHold;
            }

            if (GetState() != _state)
            {
                state = _state;
                nextState = CombatStates.NONE;
            }
            else
            {
                state = CombatStates.NONE;
                nextState = CombatStates.NONE;
            }
        }
        else
        {
            state = CombatStates.NONE;
            nextState = CombatStates.NONE;
        }

        DebugMobileManager.Log("Player State is: " + GetState().ToString());
    }

    public void SetNextState(CombatStates _state)
    {
        nextState = _state;
        SetState(nextState);
    }

    public void Attack()
    {
        EnemyCombat temp = GameObject.Find("Cube").GetComponent<EnemyCombat>();

        if (currStamina > staminaActionMinimal)
        {
            currStamina -= attackStamina;
            stats.UpdateStaminaUI(currStamina);

            if (temp.GetState() == CombatStates.ATTACK)
            {
                temp.Damage(damage);
            }

        }
        SetState(CombatStates.NONE);
    }

    void NonblockableAttack(float _damage)
    {
        EnemyCombat temp = GameObject.Find("Cube").GetComponent<EnemyCombat>();
        temp.ParryDamage(damage);
    }

    public void Damage(float _damage)
    {
        switch (state)
        {
            case CombatStates.BLOCK:
                // 50% damage
                currHealth -= _damage * blockResist; // should be resistance val
                break;
            case CombatStates.PARRY:
                // 0% damage
                // 100% damage back to enemy
                NonblockableAttack(_damage);
                state = CombatStates.NONE;
                parryTimer = 0.0f;
                break;
            default:
                // 100% damage
                currHealth -= _damage;
                break;
        }
       
        stats.UpdateHealthUI(currHealth);
        DebugMobileManager.Log("Player health is: " + currHealth);
        if(currHealth <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            DebugMobileManager.Log("GAME OVER");
        }
    }

}
