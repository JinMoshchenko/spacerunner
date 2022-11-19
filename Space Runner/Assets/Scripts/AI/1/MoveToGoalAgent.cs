using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToGoalAgent : Agent
{
    [SerializeField] float agentMoveSpeed;
    [SerializeField] Transform repairStationTransform;
    [SerializeField] Material failM;
    [SerializeField] Material rewardM;
    [SerializeField] MeshRenderer sphere;
    Vector3 agentStartingPosition;

    void Start()
    {
        agentStartingPosition = transform.localPosition;
        repairStationTransform = GameObject.FindGameObjectWithTag("RepairStation").transform;
    }


    public override void OnEpisodeBegin()
    {
        transform.localPosition = agentStartingPosition;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(repairStationTransform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        //float moveY = actions.ContinuousActions[2];

        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * agentMoveSpeed;
        //gameObject.transform.Rotate(moveZ, moveX, moveY);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
        //continuousActions[2] = Input.GetAxisRaw("Rotate");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "RepairStation")
        {
            Debug.Log("+1 for RepairStation");
            AddReward(+1f);
            EndEpisode();
            sphere.material = rewardM;
        }
        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("-1 for Wall");
            SetReward(-1f);
            EndEpisode();
            sphere.material = failM;
        }
    }
}
