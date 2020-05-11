using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public Transform camPivot;
    float heading = 0;
    public Transform cam;
    public bool isHolding = false;
    Vector2 input;
    //ItemMovement item;
    void Start()
    {
        
    }

   
    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1);

        Vector3 camF = cam.forward;
        Vector3 camR = cam.right;

        camF.y = 0;
        camR.y = 0;
        camF = camF.normalized;
        camR = camR.normalized;

        //transform.position += new Vector3(input.x,0,input.y) *Time.deltaTime”S5;


        transform.position += (camF * input.y + camR * input.x) * Time.deltaTime * speed;    
    }

    public ItemMovement itemMovement;
    public void KillGravity(ItemMovement item)
    {
        itemMovement = item;
        Debug.Log(item.name);
        item.gameObject.GetComponent<Rigidbody>().useGravity = false;
        
    }


}
