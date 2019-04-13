using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    public Slider Hp;
    public Slider Stamina;

    // Start is called before the first frame update
    void Start()
    {
        //Hp = GameObject.Find("HP_Slider").GetComponent<Slider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void UpdateHealthUI(float _currHealth)
    {
        Hp.value = _currHealth / 100.0f;
    }

    public void UpdateStaminaUI(float _currStamina)
    {
        Stamina.value = _currStamina / 100.0f;
    }

}
