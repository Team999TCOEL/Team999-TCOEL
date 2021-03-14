////////////////////////////////////////////////////////////
// File:                 <UIStaminaBar.cs>
// Author:               <Jack Peedle>
// Date Created:         <25/02/2021>
// Brief:                <Stamina bar for the player to attack and gain health with>
// Last Edited By:       <Morgan Ellis>
// Last Edited Date:     <14/03/2021>
// Last Edit Brief:      <Merging the stamia bar into the game>
////////////////////////////////////////////////////////////
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStaminaBar : MonoBehaviour
{
    // Public slider / stamina bar
    public Slider staminaBar;

    // Max stamina int
    private int iMaxStamina = 100;

    // Current stamina int
    private int iCurrentStamina;

    // Wait for seconds (regenTick)
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);

    // regen cororutine
    private Coroutine regen;

    // Start is called before the first frame update
    void Start()
    {
        // Current stamina = max stamina
        iCurrentStamina = iMaxStamina;

        // stamina bars max value = max stamina
        staminaBar.maxValue = iMaxStamina;

        // stamina value = max stamina
        staminaBar.value = iMaxStamina;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            UseStamina(10);
        }
    }

    // Use stamina
    public void UseStamina(int iAmount) {

        // If current stamina - amount is more than or = 0, we have stamina
        if (iCurrentStamina - iAmount >= 0) {

            // current stamina - amount
            iCurrentStamina -= iAmount;

            // stamina bar value = current stamina
            staminaBar.value = iCurrentStamina;

            // If already regenerating stamina
            if (regen != null) {

                // Stop coroutine
                StopCoroutine(regen);
            }

            // start regen stamina Coroutine
            regen = StartCoroutine(RegenStamina());

        } else {

            // Not enough stamina
            Debug.Log("Not enough stamina");
        }

    }

    // Regen stamina ienumerator
    private IEnumerator RegenStamina() {

        // Wait for 2 seconds
        yield return new WaitForSeconds(2);

        // While current stamina is less than max stamina
        while (iCurrentStamina < iMaxStamina) {

            // current stamina + max stamina divided by 100
            iCurrentStamina += iMaxStamina / 100;

            // stamina bar value = current stamina
            staminaBar.value = iCurrentStamina;

            // regen tick (wait for 0.1 second)
            yield return regenTick;
        }
        // Regen = null
        regen = null;
    }
    
}


       