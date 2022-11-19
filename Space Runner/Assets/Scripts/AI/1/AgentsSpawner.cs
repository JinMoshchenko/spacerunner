using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentsSpawner : MonoBehaviour
{
    public int ufoSpeed;
    public float Timer;
    public GameObject agentPF;
    public Transform agentParent;
    GameObject agentClone;
    int count;

    void Update()
    {
        //Timer -= Time.deltaTime;
        //if (count < 50)
        //{
        //    if (Timer <= 0f)
        //    {
        //        count++;
        //        agentClone = Instantiate(agentPF, new Vector3(-933.6f, 346.8f, 1539.4f), Quaternion.identity, agentParent);
        //        agentClone.transform.Rotate(new Vector3(-90, 0, 0));
        //        Timer = 2f;
        //    }
        //}
    }
}
