using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
    public float modificator;
    public bool isMoving = false;
    public Vector3 offset;
    bool isFirst = false;
    public Transform hands;
    public Transform player;
    public bool isPointing = false;
    Movement playerMove;
    public bool isHolding = false;
    Transform gameControllerTr;
    MeshCollider coll;
    bool wasWithGravity = false;
    Camera cam;
    Rigidbody rb;
    bool isKilling = false;
    
    public void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        coll = GetComponent<MeshCollider>();
        foreach (Transform tr in FindObjectsOfType<Transform>())
        {
            if (tr.tag == "GameController")
            {
                gameControllerTr = tr;
                
            }
            if(tr.tag == "MainCamera")
            {
                cam = tr.GetComponent<Camera>();
            }
        }
        playerMove = FindObjectOfType<Movement>();
        
    }
    public float force;
    public void Update()
    {
        if (isPointing&&!isHolding)
        {
            if ((Input.GetKeyDown(KeyCode.Joystick1Button6) || Input.GetKeyDown(KeyCode.F)))
            {
                
                
                isHolding = true;
                OnClick();
                playerMove.isHolding = true;
                return;
            }
        }
        if (isHolding && (Input.GetKey(KeyCode.Joystick1Button5)||Input.GetKey(KeyCode.F)))
        {
            force += modificator;
            
        }
        if(Input.GetKeyUp(KeyCode.Joystick1Button5) || Input.GetKeyUp(KeyCode.F))
        {
            if (!isFirst)
            {
                isFirst = true;
                return;
            }
            Realese();
            isFirst = false;
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.E))
        {
            isKilling = true;
            OnClick();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button6) || Input.GetKeyDown(KeyCode.Q))
        {   
            Freeze();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button8) || Input.GetKeyDown(KeyCode.C))
        {
            Duplicate();
        }

        Invoke("IsKillingFalse", 0.3f);

        if (isMoving)
        {
            transform.localPosition = offset;
        }
    }
    void Duplicate()
    {
        if (!isPointing) return;
        GameObject go = Instantiate(gameObject, transform.position + Vector3.left, transform.rotation);
      
      
    }
    void IsKillingFalse()
    {
        isKilling = false;
    }
    public void KillGravity()
    {
        rb.useGravity = !rb.useGravity;
        wasWithGravity = !wasWithGravity;
    }
    void KillGravityAll()
    {
        rb.useGravity = !rb.useGravity;
        wasWithGravity = !wasWithGravity;
    }
    public void Realese()
    {
        isMoving = false;
        isHolding = false;
        playerMove.isHolding = false;
        MoveAbovePlayer();
        coll.isTrigger = false;
        if(!wasWithGravity)
        rb.useGravity = true;
        rb.velocity = cam.transform.rotation * Vector3.forward * force;
        force = 1f;
    }
    void MoveAbovePlayer()
    {
        transform.SetParent(gameControllerTr);
    }

    void MoveUnderPlayer()
    {
        transform.SetParent(hands);
    }
    public void OnEnter()
    {
        isPointing = true;
    
    }
    void Freeze()
    {
        if (!isPointing) return;
        if (rb.isKinematic)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            return;
        }
        rb.isKinematic = true;
    }
    public void OnClick()
    {
        if (!isPointing) return;

        if (isKilling)
        {
            KillGravity();
            return;
        }
        else if(isHolding)
        {
            SetHolding();
        }
        
    
    }
    public void SetHolding()
    {
        coll.isTrigger = true;
        transform.GetComponent<Rigidbody>().useGravity = false;
        
        MoveUnderPlayer();
        // transform.localPosition = offset;
        isHolding = true;
        playerMove.isHolding = true;
        isMoving = true;
    }

    public void OnExit()
    {
        isPointing = false;
    }



}
