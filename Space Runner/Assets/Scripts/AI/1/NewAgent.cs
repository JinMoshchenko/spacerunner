using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class NewAgent : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float moveSpeed;
    [SerializeField] Material failM;
    [SerializeField] Material rewardM;
    [SerializeField] MeshRenderer indicator;
    Vector3 agentStartingPosition;



    void Start()
    {
        agentStartingPosition = transform.localPosition;
    }
    public override void OnEpisodeBegin()
    {
        //transform.localPosition = agentStartingPosition;
        transform.localPosition = new Vector3(Random.Range(-57.1f, 64.8f), Random.Range(47.3f, -25.7f), Random.Range(78f, 11.7f));
        targetTransform.localPosition = new Vector3(Random.Range(-55.5f, 63f), Random.Range(47.3f, 25.7f), Random.Range(-82.9f, -22.3f));
        gameObject.transform.rotation = Quaternion.identity;
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float moveY = actions.ContinuousActions[2];

        transform.localPosition += new Vector3(-moveX, -moveY, -moveZ) * Time.deltaTime * moveSpeed;
        gameObject.transform.Rotate(0, moveY, 0);
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
        continuousActions[2] = Input.GetAxisRaw("Rotate");
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Goal")
        {
            SetReward(+1f);
            EndEpisode();
            indicator.material = rewardM;
            Debug.Log("Reward +1");
            GameManager.GetInstance.addGoal(true);
        }
        else if(collider.gameObject.tag == "Wall")
        {
            SetReward(-1f);
            EndEpisode();
            indicator.material = failM;
            Debug.Log("Fail -1");
            GameManager.GetInstance.addGoal(false);
        }
    }
}
