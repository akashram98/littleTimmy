using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NoiseTrapActivation : BaseTrapClass
{
    public OffMeshLink offLink;
    public Transform startLink;
    public Transform endLink;
    public Transform floor;

    // Start is called before the first frame update
    void Start()
    {
        // Set name of trap to Noise
        trapName = "Noise";
        isTriggered = false;
        invisible();
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            //Debug.Log("Enemy Inside sphere");
            if(!isTriggered){
                // Debug.Log(transform.position);
                startLink.SetPositionAndRotation(new Vector3(other.transform.position.x, floor.position.y,
                                                         other.transform.position.z), other.transform.rotation);
                endLink.SetPositionAndRotation(new Vector3(transform.position.x, floor.position.y,
                                         transform.position.z), transform.rotation);
                offLink.activated = true;
                offLink.startTransform = startLink;
                offLink.endTransform = endLink;

                other.GetComponent<StateMachine_Robust>().getNoise(transform.position);
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                //Debug.Log("getNoise called");
                isTriggered = true;

                data.trapActiveOrder.Add("noiseTrap");

                // this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                // this.GetComponent<Renderer>().material.color = Color.grey;
            }
        }
    }

    public void visible()
    {
        transform.GetComponent<MeshRenderer>().enabled = true;
        transform.GetComponent<SphereCollider>().enabled = true;
    }

    public void invisible()
    {
        transform.GetComponent<MeshRenderer>().enabled = false;
        transform.GetComponent<SphereCollider>().enabled = false;
    }
}
