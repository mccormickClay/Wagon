using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        keyboard_State();
    }

    public void button_State(string _state)
    {
        PlayerCombat playerCombat = Player.GetComponent<PlayerCombat>();
        switch (_state)
        {
            case "ATTACK":
                playerCombat.SetNextState(CombatStates.ATTACK);
                break;
            case "BLOCK":
                playerCombat.SetNextState(CombatStates.BLOCK);
                break;
            case "PARRY":
                playerCombat.SetNextState(CombatStates.PARRY);
                break;
        }

    }

    public void keyboard_State()
    {
        PlayerCombat playerCombat = Player.GetComponent<PlayerCombat>();
        if (Input.GetKeyDown(KeyCode.W))
            playerCombat.SetNextState(CombatStates.ATTACK);
        else if (Input.GetKeyDown(KeyCode.A))
            playerCombat.SetNextState(CombatStates.BLOCK);
        else if(Input.GetKeyDown(KeyCode.D))
            playerCombat.SetNextState(CombatStates.PARRY);

    }

}
