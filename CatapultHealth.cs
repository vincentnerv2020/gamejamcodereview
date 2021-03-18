using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatapultHealth : MonoBehaviour
{

    public int maxCatapultHealth;
    public int currentHealth;
    public Image healthImage;
    public bool isBurning;
    public GameObject catapultFire;
    bool bringWater;
    float timer = 3f;
    public bool damaged;
    public WeaponBehaviour wb;
    public GameObject tip;
    private void Start()
    {
        maxCatapultHealth = 12;
        currentHealth = maxCatapultHealth;
        isBurning = false;
        bringWater = false;
        damaged = false;
        wb = gameObject.GetComponent<WeaponBehaviour>();
    }
    private void Update()
    {
        if (isBurning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //Deal fire Damage
                currentHealth--;
                damaged = true;
                if(currentHealth <= 0)
                {
                    DestroyCatapult();
                }
                healthImage.fillAmount = (float)currentHealth / maxCatapultHealth;
                timer = 3f;

            }
        }
        
    }

    public void BreakCatapult(bool isBurn) 
    {
        
        if (currentHealth >0)
        {
            //Play damage sound
            //Show damage FX
            currentHealth--;
            //SFX
            wb.onCatapultaHitSource.Play();

            damaged = true;
            healthImage.fillAmount = (float)currentHealth / maxCatapultHealth;
            if (isBurn)
            {
                isBurning = true;
                tip.SetActive(true);
                //Play Audio
                wb.onFireSource.Play();
                wb.onFireSource.loop = true;


                catapultFire.SetActive(true);//Do stuff here
            }
            
        }

    }

    public void RepairCatapult(bool water)
    {
        bringWater = water;
        if (currentHealth < maxCatapultHealth)
        {
            //Play repair sound
            currentHealth++;
            wb.onCatapultaRepairSource.Play();
            if (currentHealth == maxCatapultHealth)
            {
                damaged = false;
            }
            healthImage.fillAmount = (float)currentHealth / maxCatapultHealth;
            if (isBurning)
            {
                if (bringWater)
                {
                    catapultFire.SetActive(false);
                    isBurning = false;
                    tip.SetActive(false);
                    //Sound FX
                   
                    wb.onFireSource.Stop();
                    wb.onWaterSource.Play();
                }
            }
            else
            {
                
            }

        }
     
    }

    private void DestroyCatapult()
    {
        PlayerInteraction pi = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerInteraction>();
        pi.Catapults.Remove(this);
        Destroy(gameObject, 1.5f);
        GameManager gm = FindObjectOfType<GameManager>();
        gm.Catapults--;
        //Show cracked catapult
    }


}
