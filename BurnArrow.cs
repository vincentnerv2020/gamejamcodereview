using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;

public class BurnArrow : MonoBehaviour
{
    public GameObject fire;



    public void OnDisable()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Fire")
        {
            fire.SetActive(true);
        }
        else if(other.gameObject.tag == "Ballist")
        {
            fire.SetActive(false);
        }
        

    }
}
