using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //BuildingElements
    public GameObject block;
    public GameObject wood;
    [Space]
    [Space]
    //ProjectileElements
    public GameObject stoneProjectile;
    public GameObject arrowProjectile;
    [Space]
    [Space]
    //SpawnPositions
    public Transform[] blocksSpawnPosition;
    public Transform[] woodSpawnPosition;
    [Space]
    [Space]
    public Transform[] stonesSpawnPosition;
    public Transform[] arrowSpawnPosition;

    [Space]
    [Space]
    public int blocks;
    public int woods;
    public int stones;
    public int arrows;
    [Space]
    [Space]
    public PlayerMovement[] playerScripts;
    [Space]
    [Space]

    public GameObject playAgainButton;

    [Space]
    [Space]
    private int catapults;

    public int Catapults
    {
        get
        {
            return catapults;
        }
        set
        {
            catapults = value;
            if(catapults == 0)
            {
                GameOver();
            }
        }
    }
    private int walls;
    public int Walls
    {
        get
        {
            return walls;
        }
        set
        {
            walls = value;
            if(walls < 3)
            {
                GameOver();
            }
        }
    }

    public GameObject messages;
    public GameObject Objectives;

    public PlayerMovement[] playerMovScripts;
    public bool gameStarts;
    private void Start()
    {
        gameStarts = false;
        SpawnObjects();
        playAgainButton.SetActive(false);
        Catapults = 3;
        Walls = 3;
        StartCoroutine("StartGame");
    }


    public void SpawnObjects()
    {

        SpawnBlocks();
        SpawnArrows();
        SpawnWood();
        SpawnStones();
    }

    public void SpawnBlocks()
    {
        if (blocks == 0)
        {
            int randomNumber = Random.Range(0, blocksSpawnPosition.Length);
            GameObject newBlock = Instantiate(block, blocksSpawnPosition[randomNumber].position, Quaternion.identity);
            blocks++;
        }
        else
        {
            return;
        }
            
    }

    public void SpawnArrows()
    {
        if (arrows == 0)
        {
            int randomNumber = Random.Range(0, arrowSpawnPosition.Length);
            GameObject newArrow = Instantiate(arrowProjectile, arrowSpawnPosition[randomNumber].position, Quaternion.identity);
            arrows++;
        }
        else
        {
            return;
        }
            
    }

    public void SpawnWood()
    {
        if (woods == 0)
        {
            int randomNumber = Random.Range(0, woodSpawnPosition.Length);
            GameObject newWood = Instantiate(wood, woodSpawnPosition[randomNumber].position, Quaternion.identity);
            woods++;
        }
        else
        {
            return;
        }
    }

    public void SpawnStones()
    {
        if (stones == 0)
        {
            int randomNumber = Random.Range(0, stonesSpawnPosition.Length);
            GameObject newStone = Instantiate(stoneProjectile, stonesSpawnPosition[randomNumber].position, Quaternion.identity);
            stones++;
        }
        else
        {
            return;
        }
      
    }


   public void RemoveItem(string item)
    {
        switch (item)
        {
            case "Arrow":
                arrows--;
                break;
            case "Block":
                blocks--;
                break;
            case "Stone":
                stones--;
                break;
            case "Wood":
                woods--;
                break;
        }
    }

    public void GameStarts()
    {
        gameStarts = true;
        playerMovScripts[0].enabled = true;
        playerMovScripts[1].enabled = true;
        Objectives.SetActive(true);
        Destroy(Objectives, 3f);
    }

    public void GameOver()
    {

        playerScripts[0].enabled = false;
        playerScripts[1].enabled = false;
        playAgainButton.SetActive(true);
        //Make Restart


    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator StartGame()
    {
        playerMovScripts[0].enabled = false;
        playerMovScripts[1].enabled = false;
        messages.SetActive(true);
        messages.GetComponentInChildren<Text>().text = "5";
        yield return new WaitForSeconds(1f);
        messages.GetComponentInChildren<Text>().text = "4";
        yield return new WaitForSeconds(1f);
        messages.GetComponentInChildren<Text>().text = "3";
        yield return new WaitForSeconds(1f);
        messages.GetComponentInChildren<Text>().text = "2";
        yield return new WaitForSeconds(1f);
        messages.GetComponentInChildren<Text>().text = "1";
        yield return new WaitForSeconds(1f);
        messages.GetComponentInChildren<Text>().text = "1";
        yield return new WaitForSeconds(1f);
        messages.GetComponentInChildren<Text>().text = "Start Battle!";
        yield return new WaitForSeconds(1f);
        messages.SetActive(false);
        GameStarts();
    }

}
