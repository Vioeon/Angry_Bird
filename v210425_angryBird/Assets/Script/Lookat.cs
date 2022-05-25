using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat : MonoBehaviour
{
    private Transform target;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Floor")
        {
            this.gameObject.transform.LookAt(target);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
