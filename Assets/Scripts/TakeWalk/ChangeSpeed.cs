using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpeed : MonoBehaviour
{
    public float initialSpeed = 5f;
    public float maxSpeed = 15f;
    public float speedIncreaseRate = 0.1f; // 초당 속도 증가량

    private PuppyMovement puppyMovement;

    void Start()
    {
        puppyMovement = GetComponent<PuppyMovement>();

        if (puppyMovement != null)
        {
            puppyMovement.speed = initialSpeed;
        }
    }

    void Update()
    {
        if (puppyMovement != null && puppyMovement.speed < maxSpeed)
        {
            puppyMovement.speed += speedIncreaseRate * Time.deltaTime;
            puppyMovement.speed = Mathf.Clamp(puppyMovement.speed, initialSpeed, maxSpeed);
        }
    }
}
