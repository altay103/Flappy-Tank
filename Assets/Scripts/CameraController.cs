using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform follow;// Start is called before the first frame update
    Vector3 different;
    void Start()
    {
        different = follow.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=Vector3.Lerp(transform.position, follow.position - different, 5f);
    }
}
