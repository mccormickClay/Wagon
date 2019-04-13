using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    CombatStates state;
    float health;
    GameObject player;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        state = CombatStates.NONE;
        health = 100.0f;
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CombatStates GetState()
    {
        return (state);
    }

    public void SetState(CombatStates _state)
    {
        state = _state;
        DebugMobileManager.Log("Enemy state is: " + state.ToString());
    }

    void SetAffectableAttack()
    {
        SetState(CombatStates.ATTACK);
    }

    void SetAffectableBlock()
    {
        SetState(CombatStates.BLOCK);
    }

    void SetAffectableParry()
    {
        SetState(CombatStates.PARRY);
    }

    void SetAffectableBlockAndParry()
    {
        SetState(CombatStates.BLOCK_AND_PARRY);
    }

    void SetAffectableNone()
    {
        SetState(CombatStates.NONE);
    }

    void Attack()
    {
        // Check Player Choice currently...
        CombatStates tempState = GameObject.Find("Player").GetComponent<PlayerCombat>().GetState();
        switch(tempState)
        {
            case CombatStates.BLOCK:
                // 20% damage
                player.GetComponent<PlayerCombat>().Damage(20.0f);
                return;
            case CombatStates.PARRY:
                // 0% damage
                // 100 damage to self
                Damage(50.0f);
                return;
            default:
                // 100% damage
                player.GetComponent<PlayerCombat>().Damage(50.0f);
                return;
        }
    }

    public void Damage(float _damage)
    {
        health -= _damage;
        ChangeToIdle();
        DebugMobileManager.Log("Enemy health is: " + health);
        if (health <= 0)
        {
            DebugMobileManager.Log("ENEMY DEFEATED");
            Destroy(this.gameObject);
        }
    }

    public void ChangeToIdle()
    {
        anim.Play("CubeIdle");
    }
}
