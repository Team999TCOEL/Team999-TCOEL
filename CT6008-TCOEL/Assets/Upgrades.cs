using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    #region Stamina Upgrade Variables
    private float fStaminaUpgradeMultiplier = 1.07f;
    private float fStaminaCostMultiplier = 4f;
    private float fNewStaminaValue;
    private float fStaminaUpgradeCost;

    public Text StaminaUpgradeText;
    public Text StaminaUpgradeCostText;
    public Slider staminaBarSlider;
    #endregion

    #region SMG Upgrade Variables
    private float fSMGDamageUpgradeMultiplier = 1.11f;
    private float fSMGDamageCostMultiplier = 7.28f;
    private float fNewSMGDamageValue;
    private float fSMGDamageUpgradeCost;

    public Text SMGDamageUpgradeText;
    public Text SMGDamageUpgradeCostText;

    public SMG smg;
    #endregion

    public BlackBoard blackBoard;

    public PlayerController playerController;

    void Start()
    {
        smg = GameObject.Find("MP5").GetComponent<SMG>();
    }

    // Update is called once per frame
    void Update()
    {
        UpgradeStaminaUI();
        UpgradeSMGUI();
    }

    public void UpgradeSMGUI() {
        fSMGDamageUpgradeCost = Mathf.RoundToInt(smg.fDamage * fSMGDamageCostMultiplier);

        fNewSMGDamageValue = Mathf.RoundToInt(smg.fDamage * fSMGDamageUpgradeMultiplier);

        SMGDamageUpgradeText.text = Mathf.RoundToInt(smg.fDamage).ToString() + " > " + fNewSMGDamageValue.ToString();
        SMGDamageUpgradeCostText.text = fSMGDamageUpgradeCost.ToString();
    }

    public void UpgradeSMG() {
        if (blackBoard.iFuelCount > fSMGDamageUpgradeCost) {
            smg.fDamage = Mathf.RoundToInt((smg.fDamage * fSMGDamageUpgradeMultiplier));
            blackBoard.iFuelCount = blackBoard.iFuelCount - Mathf.RoundToInt(fSMGDamageUpgradeCost);
        }
    }

    public void UpgradeStaminaUI() {
        fStaminaUpgradeCost = Mathf.RoundToInt(blackBoard.fMaxStamina * fStaminaCostMultiplier);

        fNewStaminaValue = Mathf.RoundToInt(blackBoard.fMaxStamina * fStaminaUpgradeMultiplier);

        StaminaUpgradeText.text = Mathf.RoundToInt(blackBoard.fMaxStamina).ToString() + " > " + fNewStaminaValue.ToString();
        StaminaUpgradeCostText.text = fStaminaUpgradeCost.ToString();
    }

    public void UpgradeStamina() {
        if(blackBoard.iFuelCount > fStaminaUpgradeCost) {
            blackBoard.fMaxStamina = Mathf.RoundToInt((blackBoard.fMaxStamina * fStaminaUpgradeMultiplier));
            blackBoard.fCurrentStamina = Mathf.RoundToInt(blackBoard.fMaxStamina);
            blackBoard.iFuelCount = blackBoard.iFuelCount - Mathf.RoundToInt(fStaminaUpgradeCost);

            blackBoard.fCurrentStamina = blackBoard.fMaxStamina;
            staminaBarSlider.maxValue = blackBoard.fMaxStamina;
            staminaBarSlider.value = blackBoard.fMaxStamina;

            playerController.fMaxStamina = blackBoard.fMaxStamina;
        }
	}
}
