using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public bool trapActive = true;
    public GameObject trapParent;
    //forward direction
    float animDirection = 1.0f; 
    // Start is called before the first frame update
    public void playAnimation()
    {
        //changing spike trap's animation direction to forward
        trapParent.GetComponent<Animation>().Play("SpikeTrapRot");
        gameObject.GetComponent<Animation>()["Spike Tutorial Hallway Anim"].speed = animDirection;
        gameObject.GetComponent<Animation>().Play("Spike Tutorial Hallway Anim");
    }
}
