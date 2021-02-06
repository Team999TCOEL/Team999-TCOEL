////////////////////////////////////////////////////////////
// File:                 <UI_Health.cs>
// Author:               <Jack Peedle>
// Date Created:         <04/02/2021>
// Brief:                <File responsible for controlling the players health through visualization of the UI>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <06/02/2021>
// Last Edit Brief:      <Creating an easier way to take hearts away for future proofing>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health : MonoBehaviour
{
    // Integer values for the health and number of hearts
    public int iHealth;
    public int iNumOfHearts;

    public Image[] hearts;
    public Sprite sFullHeart;
    public Sprite sBrokenHeart;

    // Update
    void Update() {

        // If health is more than the number of hearts then set health to number of hearts
        if (iHealth > iNumOfHearts) {
            iHealth = iNumOfHearts;
        }

        // If i is less than hearts in heart array
        for (int i = 0; i < hearts.Length; i++) {

            // if i is less than health, set heart to full heart sprite
            if (i < iHealth) {
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

}
