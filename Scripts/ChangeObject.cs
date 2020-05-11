using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeObject : MonoBehaviour
{
    public ParticleSystem particle;
    SphereCollider sphCol;
    public GameObject ObgToStepSpawn;
    Movement playerMove;
    int numberOfBranches = 0;
    public void Start()
    {
        sphCol = GetComponent<SphereCollider>();
        playerMove = FindObjectOfType<Movement>();
        
    }
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Branch"))
        {
            Destroy(other.gameObject);
            
            ObgToStepSpawn.transform.GetChild(numberOfBranches).gameObject.SetActive(true);
            
            numberOfBranches++;
            if (numberOfBranches == 3)
            {
                particle.Play();
            }
        }        
    }

    void Update()
    {
        if (playerMove.isHolding)
        {
            sphCol.enabled = false;
        }
        else
        {
            sphCol.enabled = true;
        }  
    }
}
