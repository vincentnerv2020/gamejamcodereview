using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    GameManager gm;
    public Transform[] items;
    public bool holdSomething;
    public bool arrowIsBurning;
    public bool waterBucket;
    public string activeItem;
    public GameObject fire;
    public List<CatapultHealth> Catapults = new List<CatapultHealth>();
    public List<WallHealth> Walls = new List<WallHealth>();
    [Space]
    [Space]
  
    public bool everythingIsOk;
    public bool fireInTheHole;




    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        holdSomething = false;
        arrowIsBurning = false;
        waterBucket = false;
        fireInTheHole = false;
       
    }

    private void Update()
    {
        CheckCatapultHealth();
        CheckWallHealth();
        CheckFire();
        if (activeItem == "Wood" || activeItem == "Water" || activeItem == "Block")
        {
            if (everythingIsOk)
            {
                UseItem(activeItem);
            }
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!holdSomething)
        {
            switch (other.gameObject.tag)
            {
                //ITEMS OR PROJECTILES, IF CLOSE TO THIS OBJECTS PICKUP!!!
                case "Arrow":
                    PickUp(other.gameObject.tag, other.gameObject);
                    break;
                case "Block":
                    if (!everythingIsOk)
                    {
                        PickUp(other.gameObject.tag, other.gameObject);
                    }
                    else
                    {
                        return;
                    }
                    break;
                case "Stone":
                    PickUp(other.gameObject.tag, other.gameObject);
                    break;
                case "Wood":
                    if (!everythingIsOk)
                    {
                        PickUp(other.gameObject.tag, other.gameObject);
                    }
                    else
                    {
                        return;
                    }
                    break;
                case "Water":
                    if (fireInTheHole)
                    {
                        PickUp(other.gameObject.tag, other.gameObject);
                        waterBucket = true;
                    }
                    break;
                default:
                    //Do something
                    break;
            }
        }
        else //IF HOLD SOMETHING
        {
            switch (other.gameObject.tag) 
            {
                //IF HOLD SOMETHING AND CLOSE TO THE WEAPONS OR BUILDINGS OR Other elements
                case "Ballist":
                    if (activeItem == "Arrow" || activeItem == "Stone")
                    {
                        LoadWeapon(activeItem, other.gameObject);
                        arrowIsBurning = false;
                        fire.SetActive(arrowIsBurning);
                    }
                    break;
                case "Catapult":
                        if (activeItem == "Wood")
                        {
                                CatapultHealth ch = other.gameObject.GetComponent<CatapultHealth>();
                                if(ch.isBurning == false)
                                {
                                    if (ch.currentHealth < ch.maxCatapultHealth)
                                    {
                                        ch.RepairCatapult(waterBucket); //Heal the catapult
                                        UseItem("Wood");
                                    }
                                    else
                                    {
                                       
                                    }
                                }
                                else
                                {
                                  UseItem("Wood");
                                }
                           
                        }
                        else if (activeItem == "Stone")
                        {
                            LoadWeapon(activeItem, other.gameObject);
                        }
                        else if (activeItem == "Water")
                        {
                            CatapultHealth ch = other.gameObject.GetComponent<CatapultHealth>();
                            if (ch.isBurning) 
                            {
                                ch.RepairCatapult(waterBucket); //Heal the catapult with water
                                foreach (Transform t in items) //Deactivate water
                                {
                                    if (t.gameObject.activeSelf)
                                    {
                                        waterBucket = false;
                                        t.gameObject.SetActive(false);
                                        holdSomething = false;
                                        continue;
                                    }
                                    else
                                    {
                                        t.gameObject.SetActive(false);
                                    }
                                }
                            }
                        }   
                        break;
                case "Fire":
                    if (activeItem == "Arrow")
                    {
                        BurnTheArrow();
                    }
                    break;
                case "Wall":
                    //Do something
                    if (activeItem == "Block")
                    {
                        WallHealth wh = other.gameObject.GetComponent<WallHealth>();
                        if (wh.currentHealth < wh.maxWallHealth)
                        {
                            wh.RepairWall(); //Heal the wall
                            UseItem("Block");
                        }
                        
                    }break;
                default:
                    //Do something
                    break;
            }

        }

    }


    void PickUp(string itemName, GameObject item)
    {
        ActivateNewGameObject(itemName);
        if (item.tag != "Water")
        {
            Destroy(item);
        }
    }

    void ActivateNewGameObject(string newItemName)// Activate pick upped item in player hands
    {
        foreach (Transform t in items)
        {
            if (t.gameObject.name == newItemName)
            {
                t.gameObject.SetActive(true);
                activeItem = t.gameObject.name;
                holdSomething = true;
                continue;
            }
            else
            {
                t.gameObject.SetActive(false);
            }
        }
    }

    private void LoadWeapon(string projectileType, GameObject weaponType)
    {
        //Deactivate projectile object that holds player
        
            foreach (Transform t in items)
            {
                if (t.gameObject.activeSelf)
                {
                    t.gameObject.SetActive(false);
                    holdSomething = false;
                    continue;
                }
                else
                {
                    t.gameObject.SetActive(false);
                }

                weaponType.GetComponent<WeaponBehaviour>().PrepareWeaponToShot(gameObject, projectileType, arrowIsBurning);
            }
        
    }


    private void UseItem(string itemTag)
    {
        foreach (Transform t in items)
        {
            if (t.tag == itemTag)                   //Find BLOCK in our items
            {
                if (t.gameObject.activeSelf)        //Check if BLOCK is currently active object
                {
                    t.gameObject.SetActive(false);  //Deactivate the block
                    holdSomething = false; 

                    GameManager gm = FindObjectOfType<GameManager>(); //Get GAME MANAGER
                    gm.RemoveItem(itemTag);
                    gm.SpawnObjects();
                }
            }
        }


        
       
     
       
    }

    private void BurnTheArrow()
    {
        arrowIsBurning = true;
        fire.SetActive(arrowIsBurning);
    }


    public void CheckCatapultHealth()
    {
        foreach (CatapultHealth ch in Catapults)
        {
            if (ch.damaged)
            {
                everythingIsOk = false;
                break;
            }
            else
            {
                everythingIsOk = true;
            }

        }
    }

    public void CheckFire()
    {
        foreach (CatapultHealth ch in Catapults)
        {
            if (ch.isBurning)
            {
                fireInTheHole = true;
                break;
            }
            else
            {
                fireInTheHole = false;
            }
        }
    }

    public void CheckWallHealth()
    {
        foreach (WallHealth wh in Walls)
        {
            if (wh.damaged)
            {
                everythingIsOk = false;
                break;
            }
            else
            {
                everythingIsOk = true;
            }
        }
    }

}
