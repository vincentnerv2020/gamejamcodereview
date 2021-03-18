using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamageToWall : MonoBehaviour
{
    public GameObject stoneOwner;
    public GameObject hitFx;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("Stone touch the wall");
            WallHealth wh = other.gameObject.GetComponent<WallHealth>();
            wh.BreakWall();
            GameObject hit = Instantiate(hitFx, transform.position, Quaternion.identity);
            //ResetBallistaTarget();
            Destroy(gameObject);
        }
    }

   
}
