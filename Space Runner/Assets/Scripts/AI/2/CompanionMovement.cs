using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionMovement : MonoBehaviour
{
    public Transform target;
    public float moveSpeed;
    [SerializeField] float rotatinalDamp = 0.5f;
    [SerializeField] float detectionDistance = 80f;
    [SerializeField] float rayCastOffset = 5.5f;



    void Update()
    {
        Pathfinding();
        Move();
    }

    void Turn()
    {
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotatinalDamp * Time.deltaTime);
    }
    void Move()
    {
        transform.position += transform.forward *moveSpeed * Time.deltaTime;
    }
    void Pathfinding()
    {
        RaycastHit hit;
        Vector3 raycastOffset = Vector3.zero;

        Vector3 left = transform.position - transform.right * rayCastOffset;
        Vector3 right = transform.position + transform.right * rayCastOffset;
        Vector3 up = transform.position + transform.up * rayCastOffset;
        Vector3 down = transform.position - transform.up * rayCastOffset;

        Debug.DrawRay(left, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(right, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(up, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(down, transform.forward * detectionDistance, Color.cyan);

        if(Physics.Raycast(left, transform.forward, out hit, detectionDistance))
        {
            raycastOffset += Vector3.right;
        }
        else if(Physics.Raycast(right, transform.forward, out hit, detectionDistance))
        {
            raycastOffset -= Vector3.right;
        }

        if (Physics.Raycast(up, transform.forward, out hit, detectionDistance))
        {
            raycastOffset -= Vector3.up;
        }
        else if (Physics.Raycast(down, transform.forward, out hit, detectionDistance))
        {
            raycastOffset += Vector3.up;
        }

        if(raycastOffset != Vector3.zero)
        {
            transform.Rotate(raycastOffset * 5f * Time.deltaTime);
        }
        else
        {
            Turn();
        }
    }
}
