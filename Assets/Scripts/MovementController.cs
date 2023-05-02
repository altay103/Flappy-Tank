using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class MovementController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    float force = 1000;
    [SerializeField]
    float zMaxSpeed = 300;
    [SerializeField]
    float maxHeight = 200;
    bool first=true;
    Quaternion firstRot;
    Vector3 firstPos;
    void Start()
    {
        if (!GetComponent<PhotonView>().IsMine)
        {
            this.enabled = false;
        }
        rb = GetComponent<Rigidbody>();
        Time.timeScale = 0.5f;
        firstRot = transform.rotation;
        firstPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (first)
            {
                rb.AddRelativeForce(Vector3.forward * force*2);
                first = false;
            }
            else
            {
                if (transform.position.y > 180)
                {
                    rb.AddRelativeForce(Vector3.forward * force / 2);
                    rb.AddRelativeTorque(transform.right * force / 2);
                }
                else
                {
                    rb.AddRelativeForce(Vector3.forward * force);
                    rb.AddRelativeTorque(transform.right * force);
                }
            }
            
        }
        if (rb.velocity.z> zMaxSpeed) { 
        
            rb.velocity = new Vector3(0, rb.velocity.y,zMaxSpeed);
        }
        if (transform.position.y > 200)
        {
            transform.position = new Vector3(transform.position.x,
                                           200, transform.position.z);
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
        


    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                transform.position = firstPos;
                transform.rotation = firstRot;
                break;
        }
    }
}
