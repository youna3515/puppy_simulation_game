using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 5f; // 배경 스크롤 속도
    public float resetPosition = -10f; // 배경이 재설정되는 위치
    public float startPosition = 10f; // 배경이 시작하는 위치

    void Update()
    {
        transform.Translate(Vector3.forward * scrollSpeed * Time.deltaTime); // 배경을 앞으로 이동

        if (transform.position.z >= resetPosition) // 배경이 resetPosition에 도달하면
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, startPosition); // 배경 위치 재설정
        }
    }
}
