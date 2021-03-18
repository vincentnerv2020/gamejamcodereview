using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealth : MonoBehaviour
{
    public GameObject[] blocks;
    public int maxWallHealth;
    public int currentHealth;
    public bool isBurning;
    public int randomNum;
    public List<int> BadNumbers = new List<int>();
    public bool damaged;
    public AudioSource getHit;
    public AudioSource repair;
    private void Start()
    {
        maxWallHealth = blocks.Length;
        currentHealth = maxWallHealth;
        damaged = false;
    }


    public void BreakWall()
    {
        if(currentHealth > 0)
        {

            for(int i = 0; i<blocks.Length; i++)
            {
                if (blocks[i].activeSelf)
                {
                    blocks[i].SetActive(false);
                    currentHealth--;
                    getHit.Play();
                    damaged = true;
                    if(currentHealth <= 0)
                    {
                        DestroyWall();
                    }
                    break;
                }
                else
                {
                    continue;
                }
               
            }

        }

           

        
        
    }

    public void RepairWall()
    {
        if (currentHealth < maxWallHealth)
        {
            for (int i = 0; i < blocks.Length; i++)
            {
                if (blocks[i].activeSelf == false)
                {
                    blocks[i].SetActive(true);
                    currentHealth++;
                    repair.Play();
                    if (currentHealth == maxWallHealth)
                    {
                        damaged = false;
                    }
                    break; 
                }
                else
                {
                    continue;
                }
            }
            
        }
    }


    public void DestroyWall()
    {
        PlayerInteraction pi = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerInteraction>();
        pi.Walls.Remove(this);
        GameManager gm = FindObjectOfType<GameManager>();
        gm.Walls--;
        Destroy(gameObject, 1f);
    }
}


