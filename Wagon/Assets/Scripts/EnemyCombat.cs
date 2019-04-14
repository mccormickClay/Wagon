using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    CombatStates state;
    float health;
    float damage;
    GameObject player;
    Animator anim;

    public StatsUI stats;

    // Start is called before the first frame update
    void Start()
    {
        state = CombatStates.NONE;
        health = 100.0f;
        damage = 20.0f;
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
        //DebugMobileManager.Log("Enemy state is: " + state.ToString());
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
        player.GetComponent<PlayerCombat>().Damage(damage);
    }

    public void Damage(float _damage)
    {
        health -= _damage;
        stats.UpdateEnemyHpUI(health);
        DebugMobileManager.Log("Enemy health is: " + health);
        if (health <= 0)
        {
            DebugMobileManager.Log("ENEMY DEFEATED");
            Destroy(this.gameObject);
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    public void ParryDamage(float _damage)
    {
        ChangeToIdle();
        Damage(_damage);
    }

    private void ChangeToIdle()
    {
        anim.Play("CubeIdle");
    }
}
