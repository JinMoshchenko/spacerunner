using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipForRepair : MonoBehaviour
{
    public float spaceshipDurability;
    [SerializeField] private float durabilityRepairValue;
    [SerializeField] private float durabilityRepairAmount;
    [SerializeField] private int durabilityDealDamageEverySeconds;
    [SerializeField] private int minSecondsRandom, maxSecondsRandom;
    [SerializeField] private float durabilityDamageAmount;
    public Image durabilityFill;
    public GameObject lose;
    private float nextActionTime;
    private int previosActionTime = 0;


    void Start()
    {
        spaceshipDurability = durabilityRepairValue;
        durabilityDealDamageEverySeconds = UnityEngine.Random.Range(minSecondsRandom, maxSecondsRandom);
    }



    void Update()
    {
        // Deal damage to the spaceship every X seconds
        if (GameManager.gameFinished != true)
        {
            DamageSpaceship();
        }

            // Update durability bar
            durabilityFill.fillAmount = spaceshipDurability;
        if(spaceshipDurability >= durabilityRepairValue)
        {
            spaceshipDurability = durabilityRepairValue;
        }
    }

    void DamageSpaceship()
    {
        nextActionTime = durabilityDealDamageEverySeconds + previosActionTime;
        if (Time.timeSinceLevelLoad > nextActionTime && spaceshipDurability>0f)
        {
            spaceshipDurability -= durabilityDamageAmount;
            //Debug.Log("Damage after " + Time.timeSinceLevelLoad + " seconds");
            previosActionTime += durabilityDealDamageEverySeconds;
            durabilityDealDamageEverySeconds = UnityEngine.Random.Range(minSecondsRandom, maxSecondsRandom);
        }
    }

    // Collision/repair
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.R))
            {
                spaceshipDurability += durabilityRepairAmount;
            }
        }
    }
}
