using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    CombatStates state;
    float health;
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        state = CombatStates.NONE;
        health = 100.0f;
        damage = 20.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CombatStates GetState()
    {
        return (state);
    }

    public void button_State(string _state)
    {
        switch(_state)
        {
            case "ATTACK":
                SetState(CombatStates.ATTACK);
                return;
            case "BLOCK":
                SetState(CombatStates.BLOCK);
                return;
            case "PARRY":
                SetState(CombatStates.PARRY);
                return;
        }
    }

    void SetState(CombatStates _state)
    {
        state = _state;
        DebugMobileManager.Log("Player State is: " + state.ToString());
    }

    public void Attack()
    {
        EnemyCombat temp = GameObject.Find("Cube").GetComponent<EnemyCombat>();
        if (temp.GetState() == CombatStates.ATTACK)
        {
            temp.Damage(damage);
        }
        else
        {
            Damage(10.0f);
        }
        SetState(CombatStates.NONE);
    }

    public void Damage(float _damage)
    {
        health -= _damage;
        DebugMobileManager.Log("Player health is: " + health);
        if(health <= 0)
        {
            DebugMobileManager.Log("GAME OVER");
        }
    }
}
