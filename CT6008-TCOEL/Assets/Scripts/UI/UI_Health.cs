////////////////////////////////////////////////////////////
// File:                 <UI_Health.cs>
// Author:               <Jack Peedle>
// Date Created:         <04/02/2021>
// Brief:                <File responsible for controlling the players health through visualization of the UI>
// Last Edited By:       <Jack Peedle>
// Last Edited Date:     <04/02/2021>
// Last Edit Brief:      <Setting up Health to visualize current health>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Health : MonoBehaviour
{
    
    public GameObject goHeart1;
    public GameObject goHeart2;
    public GameObject goHeart3;


    public GameObject goBrokenHeart1;
    public GameObject goBrokenHeart2;
    public GameObject goBrokenHeart3;

    
    // Update is called once per frame
    void Update()
    {
        // If the spacebar is pressed change the health
        if (Input.GetKeyDown("space")) {
            Debug.Log("SpacePressed");
            DestroyHearts();
        }
    }
    public void DestroyHearts() {
        // If all hearts are active and player takes damage, show the appropriate broken heart
        if (goHeart1 && goHeart2 && goHeart3) {
            goHeart3.SetActive(false);
            goBrokenHeart3.SetActive(true);
            Debug.Log("FirstHalfGone");
            return;
        }
        // if the player has 2 and a half hearts, set the half heart to false
        if (goHeart1 && goHeart2 && goBrokenHeart3 && !goHeart3) {
            goBrokenHeart3.SetActive(false);
            Debug.Log("SecondHalfGone");
            return;
        }
        // if the player has 2 hearts make the right one break in half
        if (goHeart1 && goHeart2) {
            goHeart2.SetActive(false);
            goBrokenHeart2.SetActive(true);
            return;
        }
        //if the player has a heart and a half, set the half heart to false
        if (goHeart1 && goBrokenHeart2) {
            goBrokenHeart2.SetActive(false);
            return;
        }
        // if the player has one heart, show a half heart
        if (goHeart1) {
            goHeart1.SetActive(false);
            goBrokenHeart1.SetActive(true);
            return;
        }
        //if the player has half a heart and takes damage, game over
        if (goBrokenHeart1) {
            Debug.Log("GameOver");
            return;
        }
    }
}
