using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float pushStrength = 20f;




    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Hit!");
            collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * pushStrength * Time.deltaTime, ForceMode.Impulse);
            Destroy(this.gameObject);
        }
    }
}
