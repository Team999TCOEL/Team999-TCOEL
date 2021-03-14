using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    // Integer values for the health and number of hearts
    //public int iHealth;
    public int iNumOfHearts;

    public Image[] hearts;
    public Sprite sFullHeart;
    public Sprite sBrokenHeart;

    public BlackBoard blackBoard;

    // Public slider / stamina bar
    public Slider staminaBar;

    // Wait for seconds (regenTick)
    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);

    // regen cororutine
    private Coroutine regen;

    void Start() {
        // Current stamina = max stamina
        //blackBoard.iCurrentStamina = blackBoard.iMaxStamina;

        // stamina bars max value = max stamina
        staminaBar.maxValue = blackBoard.iMaxStamina;

        // stamina value = max stamina
        staminaBar.value = blackBoard.iMaxStamina;
    }

    void Update() {

        // If health is more than the number of hearts then set health to number of hearts
        if (blackBoard.iPlayerHealth > iNumOfHearts) {
            blackBoard.iPlayerHealth = iNumOfHearts;
        }

        // If i is less than hearts in heart array
        for (int i = 0; i < hearts.Length; i++) {

            // if i is less than health, set heart to full heart sprite
            if (i < blackBoard.iPlayerHealth) {
                hearts[i].sprite = sFullHeart;
            } else { // if i is more than health, set heart to broken heart sprite
                hearts[i].sprite = sBrokenHeart;
            }

            // Check if i is smaller, if true set hearts to true
            if (i < iNumOfHearts) {
                hearts[i].enabled = true;
            } else { // Check if i is bigger, hide hearts
                hearts[i].enabled = false;
            }
        }
    }

    public void UseStamina(int iAmount) {

        // If current stamina - amount is more than or = 0, we have stamina
        if (blackBoard.iCurrentStamina - iAmount >= 0) {

            // current stamina - amount
            blackBoard.iCurrentStamina -= iAmount;

            // stamina bar value = current stamina
            staminaBar.value = blackBoard.iCurrentStamina;

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
        while (blackBoard.iCurrentStamina < blackBoard.iMaxStamina) {

            // current stamina + max stamina divided by 100
            blackBoard.iCurrentStamina += blackBoard.iMaxStamina / 100;

            // stamina bar value = current stamina
            staminaBar.value = blackBoard.iCurrentStamina;

            Debug.Log(staminaBar.value);

            // regen tick (wait for 0.1 second)
            yield return regenTick;
        }
        // Regen = null
        regen = null;
    }

}
