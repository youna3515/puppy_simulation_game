using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class PuppyMovement : MonoBehaviour
{
    public float speed = 5f; // 강아지의 이동 속도
    public float laneWidth = 2.0f; // 강아지가 이동할 수 있는 한 레인의 너비
    private Vector2 startTouchPosition, endTouchPosition;
    private float swipeThreshold = 50f; // 스와이프 감지 임계값

    private Vector3 targetPosition; // 강아지의 목표 위치
    private bool isMoving = false;

    void Start()
    {
        targetPosition = transform.position; // 초기 위치 설정
    }

    void Update()
    {
        HandleTouchInput();

        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                Vector2 swipeDelta = endTouchPosition - startTouchPosition;

                if (swipeDelta.magnitude > swipeThreshold)
                {
                    if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                    {
                        if (swipeDelta.x > 0)
                        {
                            MoveRight();
                        }
                        else
                        {
                            MoveLeft();
                        }
                    }
                }
            }
        }
    }

    void MoveRight()
    {
        if (targetPosition.x < transform.position.x + laneWidth)
        {
            targetPosition = new Vector3(transform.position.x + laneWidth, transform.position.y, transform.position.z);
            isMoving = true;
        }
    }

    void MoveLeft()
    {
        if (targetPosition.x > transform.position.x - laneWidth)
        {
            targetPosition = new Vector3(transform.position.x - laneWidth, transform.position.y, transform.position.z);
            isMoving = true;
        }
    }

    void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            isMoving = false;
        }
    }
}
