using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : MonoBehaviour, IInteractable
{
    public void Interact () {
        Debug.Log(Random.Range(0,1000));
    }
    // Start is called before the first frame update
    //apple
    //snapple
    //potato
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
