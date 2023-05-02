using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlayerAnimController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject ramp,rampEnd;
    [SerializeField]
    float speed = 1f;
    GameObject turret,rotor,track;
    Rigidbody rb;
    bool move = false;
    float oldPosZ;
    [SerializeField]
    bool debugMode = false;
    void Start()
    {
        ramp = GameObject.Find("Ramp");
        rampEnd = GameObject.Find("RampEnd");
        turret = GameObject.Find("Main_Turret");
        rotor = GameObject.Find("Main_Rotor");
        track = GameObject.Find("Track");
        DOTween.Init();
        rb = GetComponent<Rigidbody>();
        move = true;
        oldPosZ = transform.position.z;
        
        
    }
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            debugMode = true;
        }
            if (debugMode)
        {
            //Time.timeScale = 20;
        }
        else
        {
            //Time.timeScale = 1;
        }
        if (move)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, speed);
            rotor.transform.RotateAround(rotor.GetComponent<Renderer>().bounds.center, Vector3.right,
                                        (speed*80)*(transform.position.z-oldPosZ));
            oldPosZ = transform.position.z;
            track.GetComponent<MeshRenderer>().material.SetTextureOffset("_MainTex", new Vector2(0, Time.time*4));
        }
    }
    
    private void AddController()
    {
        Time.timeScale = 1;
        gameObject.AddComponent<MovementController>();
        Destroy(GetComponent<PlayerAnimController>());
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
   
        switch (other.gameObject.name)
        {
            case "RampEnd2":
           
                turret.transform.DOLocalRotate(new Vector3(0, 180, 0), 5).OnComplete(() => Invoke("AddController", 0.5f));
                move = false;
                rb.velocity = new Vector3(0, 0, 0);
                break;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ramp":
                transform.DORotate(collision.transform.eulerAngles, 1f);
                
                break;
        }
        
        
    }


}
