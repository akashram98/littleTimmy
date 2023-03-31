using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class NoiseTrapActivation : BaseTrapClass
{
    public OffMeshLink offLink;
    public workingOffLinkScript offLinkScript;
    public Transform startLink;
    public Transform endLink;
    public Transform floor;
    public LayerMask obstacleMask;
    public Color activeColor = new Color(1f, 0f, 0f, 1f);
    public Color inactiveColor = new Color(1f, 0.843f, 0f, 1f);
    private List<Collider> npcArray= new List<Collider>();
    private Collider temp;
    private Collider[] npcArray1;
    private int count = 0;

    private GameObject ring;

    private bool calculateOffLink = false;
    private Transform enemyTransform;
    private StateMachine_Robust enemyMachine;
    private NavMeshAgent enemyAgent;

    //public Transform myTransform;

    //private Vector3 ogPosition = new Vector3();
    //private Quaternion ogRotation = new Quaternion();

    //private bool justStarted = true;

    //private void FixedUpdate()
    //{
    //    if (justStarted)
    //    {
    //        ogPosition.Set(myTransform.position.x, myTransform.position.y, myTransform.position.z);
    //        ogRotation.Set(myTransform.rotation.x, myTransform.rotation.y, myTransform.rotation.z, myTransform.rotation.w);
    //        //transform.SetPositionAndRotation(ogPosition, ogRotation);
    //        justStarted = false;
    //    }
    //}

    private void Update()
    {
        if (!offLinkScript.enabled)
        {
            calculateOffLink = false;
        }
        //if (calculateOffLink && !walledOff(transform.position, enemyTransform.position))
        if (calculateOffLink && !walledOff(transform.position, enemyTransform.position))
        {
            //enemyAgent.isStopped = true;
            enemyAgent.enabled = false;
            
            offLink.enabled = true;
            startLink.SetPositionAndRotation(new Vector3(enemyTransform.position.x, floor.position.y,
            enemyTransform.position.z), enemyTransform.rotation);
            endLink.SetPositionAndRotation(new Vector3(transform.position.x, floor.position.y,
            transform.position.z), transform.rotation);
            offLink.startTransform = startLink;
            offLink.endTransform = endLink;
            offLink.GetComponent<workingOffLinkScript>().enabled = true;
            calculateOffLink = false;

            enemyAgent.enabled = true;
            enemyMachine.getNoise(endLink.position);
        }
        
        
        npcArray1 = Physics.OverlapSphere(transform.position, 8.25f);
        
        //Debug.Log(npcArray1.Length);
        if (count == 0)
        {
            for (var i = 0; i < npcArray1.Length; i++)
            {
                if (npcArray1[i].gameObject.layer == 7)
                {
                    count = count + 1;
                }

            }
            Debug.Log(count);
        }
        
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 8.25f);
        Gizmos.color = Color.green;
    }
    public void respawnMe()
    {
        isTriggered = false;
        calculateOffLink = false;
        transform.parent.GetComponent<Renderer>().material.color = Color.grey;
        invisible();
        gameObject.GetComponentInParent<Respawn>().respawnMe();
        ring.GetComponent<ParticleSystem>().enableEmission =true; 
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set name of trap to Noise
        trapName = "Noise";
        isTriggered = false;
        transform.parent.GetComponent<Renderer>().material.color = inactiveColor;

        GameObject parent = this.transform.parent.gameObject;
        //GameObject parent = this.gameObject.GetComponentInParent<GameObject>();
        GameObject particle = parent.transform.GetChild(1).gameObject;
        ring = particle.transform.GetChild(1).gameObject;

        invisible();

        ring.GetComponent<ParticleSystem>().startColor = Color.yellow;
    }

    /*
    void OnTriggerStay(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            StateMachine_Robust SM = other.gameObject.GetComponent<StateMachine_Robust>();
            //Debug.Log("Enemy Inside sphere");
            if(!isTriggered && SM.conscious && SM.alive){
                calculateOffLink = true;
                enemyTransform = other.transform;
                offLink.GetComponent<workingOffLinkScript>().enabled = true;
                enemyMachine = SM;
                enemyAgent = other.gameObject.GetComponent<NavMeshAgent>();
                // Debug.Log(transform.position);


                other.GetComponent<StateMachine_Robust>().getNoise(transform.position);
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                //Debug.Log("getNoise called");
                isTriggered = true;
                //data.trapActiveOrder.Add("noiseTrap-1");
                data.noiseTrap.Add(1);

                // ring.GetComponent<ParticleSystem>().startColor = Color.white;
                ring.GetComponent<ParticleSystem>().Clear();
                ring.GetComponent<ParticleSystem>().enableEmission =true;   //Enable the ring  (if you dont want to remove ring then delete this and false line in void visible)
                
                transform.parent.GetComponent<Renderer>().material.color = Color.grey;

            }
  
        }
    }
    
    */
    
    
    void goToCenter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            StateMachine_Robust SM = other.gameObject.GetComponent<StateMachine_Robust>();
            Debug.Log("Enemy Inside sphere");
            if (!isTriggered && SM.conscious && SM.alive)
            {
                calculateOffLink = true;
                enemyTransform = other.transform;
                offLink.GetComponent<workingOffLinkScript>().enabled = true;
                enemyMachine = SM;
                enemyAgent = other.gameObject.GetComponent<NavMeshAgent>();
                // Debug.Log(transform.position);


                other.GetComponent<StateMachine_Robust>().getNoise(transform.position);
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                //Debug.Log("getNoise called");
                //isTriggered = true;
                //data.trapActiveOrder.Add("noiseTrap-1");
                data.noiseTrap.Add(1);

                // ring.GetComponent<ParticleSystem>().startColor = Color.white;
                ring.GetComponent<ParticleSystem>().Clear();
                ring.GetComponent<ParticleSystem>().enableEmission = true;   //Enable the ring  (if you dont want to remove ring then delete this and false line in void visible)

                transform.parent.GetComponent<Renderer>().material.color = Color.grey;
                count = count - 1;
            }

        }
    }

    

        
    IEnumerator OnTriggerStay(Collider other)
    {
        yield return new WaitForSeconds(0.1f);
        if (other.gameObject.layer==7)
        {
            goToCenter(other);
            if(count==0)
            {
                isTriggered = true;
            }
            
            
        }
        
        /*
        if (npcArray.Count == 1 && other.gameObject.layer == 7)
        {
            Debug.Log("hi");
            npcArray.Add(other);

            //Debug.Log(other.gameObject);
            //Debug.Log(npcArray.Count);
            while (npcArray.Count != 0)
            {
                //Debug.Log("Hi");
                //Debug.Log(npcArray.Count);
                Debug.Log(npcArray.Count);
                if (npcArray.Count > 1)
                {
                    isTriggered = false;
                }
                //isTriggered = false;
                goToCenter(npcArray[0]);
                npcArray.RemoveAt(0);
            }
        }
        
        //Debug.Log(other.gameObject.name);
        */
        
   }
    


    public void visible()
    {
        transform.GetComponent<MeshRenderer>().enabled = true;
        transform.GetComponent<SphereCollider>().enabled = true;
        transform.parent.GetComponent<Renderer>().material.color = activeColor;
        ring.GetComponent<ParticleSystem>().enableEmission =false;  //Disable the ring
        // ring.GetComponent<ParticleSystem>().startColor = Color.yellow;
    }

    public void invisible()
    {
        transform.GetComponent<MeshRenderer>().enabled = false;
        transform.GetComponent<SphereCollider>().enabled = false;
        ring.GetComponent<ParticleSystem>().startColor = Color.white;
        ring.GetComponent<ParticleSystem>().Clear();
    }

    private bool walledOff(Vector3 firstPoint, Vector3 secondPoint)
    {
        Vector3 direction = (secondPoint - firstPoint);
        float maxDist = direction.magnitude;
        direction.Normalize();

        return Physics.Raycast(firstPoint, direction, maxDist, obstacleMask);
    }
}
