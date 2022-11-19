using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceshipController : MonoBehaviour
{
    public Camera mainCam;
    public Transform spaceship;
    public Rigidbody rb;
    public ParticleSystem turboParticle;
    public ParticleSystem regularParticle;
    public MovementType movementType;
    [Space]
    [Header("PlayerSettings")]
    public float turnSpeed;
    public float boostSpeed;
    public float rollSpeed;
    public float moveSpeed;
    public float turboSpeed;
    [Space]
    [Header("Turbo")]
    public Image fill;
    float maxTurbo = 1f;
    [SerializeField]float turboAmount;
    public float turboDecrease;
    public float turboIncrease;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        movementType = MovementType.None;
        turboParticle.Stop();
        regularParticle.Stop();
        turboAmount = maxTurbo;
    }

    private void FixedUpdate()
    {
        Turn();
        Thrust();
        RefillTurbo();

        #region Turbo
        if (Input.GetKey(KeyCode.LeftShift) && movementType == MovementType.Turbo && turboAmount > 0)
        {
            spaceship.position += spaceship.right * turboSpeed * -Time.deltaTime * Input.GetAxis("Throttle");
            turboAmount -= turboDecrease;
            fill.fillAmount = turboAmount;
        }
        else
        {
            turboParticle.Stop();
        }
        #endregion

        #region StatesChanges 
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E) || Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            if (Input.GetKey(KeyCode.LeftShift) != true)
            {
                movementType = MovementType.Regular;
            }
            else
            {
                movementType = MovementType.Turbo;
            }
        }
        else
        {
            movementType = MovementType.None;
        }
        #endregion

        #region Paricles
        if (movementType == MovementType.None)
        {
            regularParticle.Stop();
            turboParticle.Stop();
        }
        else if(movementType == MovementType.Turbo && turboAmount > 0)
        {
            regularParticle.Stop();
            turboParticle.Play();
        }
        else if (movementType == MovementType.Regular)
        {
            regularParticle.Play();
            turboParticle.Stop();
        }
        #endregion
    }

    void Turn()
    {
        float yaw = boostSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float pitch = boostSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        float roll = rollSpeed * Time.deltaTime * Input.GetAxis("Rotate");
        spaceship.Rotate(pitch, yaw, roll);
    }
    void Thrust()
    {
        spaceship.position += spaceship.right * moveSpeed * -Time.deltaTime * Input.GetAxis("Throttle");
    }

    void RefillTurbo()
    {
        if(movementType != MovementType.Turbo && turboAmount <=1)
        {
            turboAmount += turboIncrease;
            fill.fillAmount = turboAmount;
        }
    }
}

public enum MovementType
{
    None,
    Regular,
    Turbo
}
