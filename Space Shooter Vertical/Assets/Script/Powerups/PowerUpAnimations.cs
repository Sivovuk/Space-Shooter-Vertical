using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpAnimations : MonoBehaviour
{
    [SerializeField]
    private Player playerScript;

    [SerializeField]
    private GameObject trippleShot;
    [SerializeField]
    private GameObject speedBoost;
    [SerializeField]
    private GameObject shield;

    public static float timePassTrippleShot = 5;
    public static float timePassSpeedBoost = 5;
    public static float timePassShield = 5;


    void Start()
    {
        
    }

   
    void Update()
    {
        Calculations();
    }

    private void Calculations() 
    {
		#region Tripple shot

		if (playerScript.GetTrippleShotBoolean())
        {
            if (!trippleShot.activeSelf) 
            {
                trippleShot.SetActive(true);
            }

            timePassTrippleShot -= Time.deltaTime;
            float temp = timePassTrippleShot / 5;
            trippleShot.GetComponent<Image>().fillAmount = temp;
        }
        else 
        {
            if (trippleShot.activeSelf)
            {
                trippleShot.SetActive(false);
            }

            timePassTrippleShot = 5;
            trippleShot.GetComponent<Image>().fillAmount = 1;
        }

		#endregion

		#region Speed Boost

		if (playerScript.GetSpeedBoostAcive())
        {
            if (!speedBoost.activeSelf)
            {
                speedBoost.SetActive(true);
            }

            timePassSpeedBoost -= Time.deltaTime;
            float temp = timePassSpeedBoost / 5;
            speedBoost.GetComponent<Image>().fillAmount = temp;
        }
        else
        {
            if (speedBoost.activeSelf)
            {
                speedBoost.SetActive(false);
            }

            timePassSpeedBoost = 5;
            speedBoost.GetComponent<Image>().fillAmount = 1;
        }

        #endregion

        #region Shield

        if (playerScript.GetShiedActives())
        {
            if (!shield.activeSelf)
            {
                shield.SetActive(true);
            }

            timePassShield -= Time.deltaTime;
            float temp = timePassShield / 5;
            shield.GetComponent<Image>().fillAmount = temp;
        }
        else
        {
            if (shield.activeSelf)
            {
                shield.SetActive(false);
            }

            timePassShield = 5;
            shield.GetComponent<Image>().fillAmount = 1;
        }

        #endregion
    }

}
