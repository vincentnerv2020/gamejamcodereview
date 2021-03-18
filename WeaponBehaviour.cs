using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponBehaviour : MonoBehaviour
{
    public Transform weaponOwner;
    public int playerNumber;
    public GameObject shooter;
    public List<Transform> Targets = new List<Transform>();
    public Transform currentTarget; //By standart it will always be the middle object in the scene if weapon is loaded
    public bool readyToShoot;
    public GameObject targetSphere;
    public string projectileName;
    [Space]
    [Space]
    Quaternion oldRotation;
    public float smoothSpeed = 0.5f;
    public GameManager gm;
    public int targetNumber;
    [Space]
    [Space]
    //WEAPON ANIMATIONS
    public Animator anim;
    public GameObject arrowProjectile;
    public GameObject stoneProjectile;
    [Space]
    [Space]
    public bool onFire;
    [Space]
    [Space]
    public AudioSource onFireSource;
    public AudioSource onWaterSource;
    [Space]
    public AudioSource onCatapultaShotSource;
    public AudioSource onCatapultaHitSource;
    public AudioSource onCatapultaDestroySource;
    public AudioSource onCatapultaRepairSource;
    [Space]
    public AudioSource onBalistaShotSource;
    
     void Start()
    {
        targetNumber = 0;
        currentTarget = Targets[targetNumber];
        oldRotation = transform.rotation; //Store the  Standart value of rotation before we get a target
        gm = FindObjectOfType<GameManager>();
        onFire = false;
    }


    private void Update()
    {

        if (readyToShoot)
        {
            TurnToTheTarget();
            if (playerNumber == 1)
            {
                if (Input.GetKeyDown(KeyCode.RightControl))
                {
                    LaunchProjectile();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    LaunchProjectile();
                }
            }
        }
        else
        {
            TurnBackToNormal();
        }
        
        ChangeTarget();
        foreach(Transform tb in Targets)
        {
            if(tb == null)
            {
                Targets.Remove(tb);
            }
        }


    }
    private void OnTriggerEnter(Collider other)
    {
        //If player close
        if(other.gameObject.tag == "Player")
        {
            weaponOwner = other.gameObject.transform;
            playerNumber = weaponOwner.GetComponent<PlayerMovement>().playerNumber;
        }
    }
    public void PrepareWeaponToShot(GameObject player, string projectile, bool burningArrow)
    {
        onFire = burningArrow;
        //Get projectile name 
        projectileName = projectile;
        //Stop player from movement and get the player#
        playerNumber = player.GetComponent<PlayerMovement>().playerNumber;
        shooter = player;
        player.GetComponent<PlayerMovement>().enabled = false; //Disabling player(weapon owners) movement script
        playerNumber = player.GetComponent<PlayerMovement>().playerNumber;
        if (projectile == "Arrow")
        {
            readyToShoot = true; //Its time to pick up a target
            PickUpTarget(); //Get standart Target
            //Load
        }
        else if(projectile == "Stone")
        {
            readyToShoot = true; //Its time to pick up a target
            PickUpTarget(); //Get standart Target
            //Load
        }
        else
        {
            return;
            //We can not shot because projectile is not a bullet....
        }

    }
    public void PickUpTarget()
    {
        //Look at current targets transform by LookRotation or transform.LookAt
        //Activate icon(Sphere) UNDER current targets TRANSFORM
        targetNumber = 0;
        currentTarget = Targets[targetNumber];
        TargetBehaviour tb = currentTarget.GetComponent<TargetBehaviour>();
        targetSphere = tb.targetSphere;
        targetSphere.SetActive(true);
        //Change target by clicking UP or DOWN arrow
        TurnToTheTarget();
    }
    public void ChangeTarget()
    {
        
        if (readyToShoot)
        {
            if(playerNumber == 1)
            {
                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    //ChangeTarget
                    if (Targets.Count == 3)
                    {
                        if (targetNumber <= Targets.Count - 1)
                        {
                            if (targetNumber != Targets.Count - 1) //Means not = 2
                            {
                                targetNumber++;//was 0 become 1, was 1 become 2
                                ShowTarget();
                            }
                            else if (targetNumber == 2)
                            {
                                targetNumber = 0;
                                ShowTarget();
                            }
                        }
                        else if (targetNumber == Targets.Count - 1)
                        {
                            targetNumber = 0;
                            ShowTarget();
                        }
                    }
                    else if (Targets.Count == 2)
                    {
                        if (targetNumber == 0)
                        {
                            targetNumber = 1;
                            ShowTarget();
                        }
                        else
                        {
                            targetNumber = 0;
                            ShowTarget();
                        }

                    }
                    else if (Targets.Count == 1)
                    {
                        targetNumber = Targets.Count - 1;
                        ShowTarget();
                    }
                }
            }
            if(playerNumber == 2)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    //ChangeTarget
                    if (Targets.Count == 3)
                    {
                        if (targetNumber <= Targets.Count - 1)
                        {
                            if (targetNumber != Targets.Count - 1) //Means not = 2
                            {
                                targetNumber++;//was 0 become 1, was 1 become 2
                                ShowTarget();
                            }
                            else if (targetNumber == 2)
                            {
                                targetNumber = 0;
                                ShowTarget();
                            }
                        }
                        else if (targetNumber == Targets.Count - 1)
                        {
                            targetNumber = 0;
                            ShowTarget();
                        }
                    }
                    else if (Targets.Count == 2)
                    {
                        if (targetNumber == 0)
                        {
                            targetNumber = 1;
                            ShowTarget();
                        }
                        else
                        {
                            targetNumber = 0;
                            ShowTarget();
                        }

                    }
                    else if (Targets.Count == 1)
                    {
                        targetNumber = Targets.Count - 1;
                        ShowTarget();
                    }
                }
            }
        }
         //GetActivated Number of target
    }
    public void ShowTarget()
    {
        targetSphere.gameObject.SetActive(false);
        currentTarget = Targets[targetNumber];
        targetSphere = currentTarget.GetComponent<TargetBehaviour>().targetSphere;
        targetSphere.gameObject.SetActive(true);
    }
    public void ResetTarget()
    {
        readyToShoot = false;
        targetNumber = 0;
        currentTarget = Targets[targetNumber];
        targetSphere.SetActive(false);

        //RE-Activate player movement
        shooter.GetComponent<PlayerMovement>().enabled = true; 
    }
    public void LaunchProjectile()
    {
        if (currentTarget && readyToShoot)
        {
            if(playerNumber == 2)
            {
                SetAnimatorTarget(currentTarget.name);
                gm.RemoveItem(projectileName);
                gm.SpawnObjects();
                ResetTarget();
                TurnBackToNormal();
            }
            else if(playerNumber ==1)
            {
                SetAnimatorTarget(currentTarget.name);
                gm.RemoveItem(projectileName);
                gm.SpawnObjects();
                ResetTarget();
                TurnBackToNormal();
            }
        }
        
    }
    public void TurnToTheTarget()
    {
        //If ready to shoot
        Vector3 relativePos = currentTarget.position - transform.position; //Calculate diffrence btw target and self
        Quaternion newRotation = Quaternion.LookRotation(relativePos, Vector3.up); //Get the new value for rotation 
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, smoothSpeed);
    }
    public void TurnBackToNormal()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, oldRotation, smoothSpeed);
    }


    public void SpawnArrowProjectile(string triggerName)
    {
        
        Debug.Log("SpawnArrow");
        GameObject newArrow = Instantiate(arrowProjectile, transform.position, Quaternion.identity);
        newArrow.GetComponent<DealDamageToCatapult>().FireArrow = onFire; //Tell if fire arrow
        newArrow.GetComponent<DealDamageToCatapult>().arrowOwner = this.gameObject;
        anim = newArrow.GetComponent<Animator>();
        anim.SetTrigger(triggerName);
        
    }
    public void SpawnStoneProjectile(string triggerName)
    {
        
        Debug.Log("SpawnStone");
        GameObject newStone = Instantiate(stoneProjectile, transform.position, Quaternion.identity);
        newStone.GetComponent<DealDamageToWall>().stoneOwner = this.gameObject;
        anim = newStone.GetComponent<Animator>();
        anim.SetTrigger(triggerName);
        onCatapultaShotSource.Play();
    }
    public void SetAnimatorTarget(string target)
    {
        
        //Player 1 SPAWNS ARROW
        if(playerNumber == 1)
        {
            switch (target)
            {
                case "Catapulta1"://means LEFT
                    if(gameObject.name == "Balist 1")//means LEFT
                    {
                        SpawnArrowProjectile("LL");
                    }else if(gameObject.name == "Balist 2")//means CENTER
                    {
                        SpawnArrowProjectile("CL");
                    }else if(gameObject.name == "Balist 3")//means RIGHT
                    {
                        SpawnArrowProjectile("RL");
                    }
                    break;
                case "Catapulta2"://means MIDDLE CENTER
                    if (gameObject.name == "Balist 1")//means LEFT
                    {
                        SpawnArrowProjectile("LC");
                        
                    }
                    else if (gameObject.name == "Balist 2")//means CENTER
                    {
                        SpawnArrowProjectile("CC");
                        
                    }
                    else if (gameObject.name == "Balist 3")//means RIGHT
                    {
                        SpawnArrowProjectile("RC"); 
                    }

                    break;
                case "Catapulta3"://means RIGHT
                    if (gameObject.name == "Balist 1")//means LEFT
                    {
                        SpawnArrowProjectile("LR");
                    }
                    else if (gameObject.name == "Balist 2")//means CENTER
                    {
                        SpawnArrowProjectile("CR");
                    }
                    else if (gameObject.name == "Balist 3")//means RIGHT
                    {
                        SpawnArrowProjectile("RR");
                    }
                    break;
                default:

                    break;
            }
            
        }
        else if(playerNumber == 2)
        {
            switch (target)
            {
                case "Wall3"://means LEFT
                    if (gameObject.name == "Catapulta3")//means LEFT
                    {
                        SpawnStoneProjectile("LL");
                    }
                    else if (gameObject.name == "Catapulta2")//means CENTER
                    {
                        SpawnStoneProjectile("CL");
                    }
                    else if (gameObject.name == "Catapulta1")//means RIGHT
                    {
                        SpawnStoneProjectile("RL");
                    }
                    break;
                case "Wall2"://means LEFT
                    if (gameObject.name == "Catapulta3")//means LEFT
                    {
                        SpawnStoneProjectile("LC");
                    }
                    else if (gameObject.name == "Catapulta2")//means CENTER
                    {
                        SpawnStoneProjectile("CC");
                    }
                    else if (gameObject.name == "Catapulta1")//means RIGHT
                    {
                        SpawnStoneProjectile("RC");
                    }
                    break;
                case "Wall1"://means right
                    if (gameObject.name == "Catapulta3")//means LEFT
                    {
                        SpawnStoneProjectile("LR");
                    }
                    else if (gameObject.name == "Catapulta2")//means CENTER
                    {
                        SpawnStoneProjectile("CR");
                    }
                    else if (gameObject.name == "Catapulta1")//means RIGHT
                    {
                        SpawnStoneProjectile("RR");
                    }
                    break;
                default:
                    break;
            }

        }
    }




}
