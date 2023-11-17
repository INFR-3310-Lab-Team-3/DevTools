using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowballBehaviour : MonoBehaviour
{
    GameObject player;

    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        body.AddForce(((player.transform.position + new Vector3(0, 0, 10)) - this.transform.position).normalized * 100, ForceMode.Impulse);
        //body.AddForce(transform.forward * 100, ForceMode.Impulse);
        Destroy(this.gameObject, 8);
    }
}
