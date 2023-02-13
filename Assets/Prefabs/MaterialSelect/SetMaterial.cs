using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterial : MonoBehaviour
{
    public GetMaterial getmaterial;
    public GameObject Parent;

    // Start is called before the first frame update
    void Start()
    {

             if (Parent.name=="Noise Trap")
             {
                GetComponent<Renderer>().material = getmaterial.NoiseTrap;
            
             }  

             else if (Parent.name=="Freeze Trap")
             {
                GetComponent<Renderer>().material = getmaterial.FreezeTrap;
            
             }  
            else if (Parent.name=="Door" | Parent.name == "Trap Door Button")
             {
                GetComponent<Renderer>().material = getmaterial.TrapDoor;
            
             }  

             else if (Parent.name=="Launcher" | Parent.name == "Spike Trap Button")
             {
                GetComponent<Renderer>().material = getmaterial.SpikeTrap_launcher;
            
             }  

            else if (Parent.name=="spikeGrid")
             {
                GetComponent<Renderer>().material = getmaterial.SpikeTrap_spikes;
            
             }  

             
            else if (Parent.name=="Drop Trap")
             {
                GetComponent<Renderer>().material = getmaterial.DropTrap;
            
             }  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
