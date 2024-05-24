using System.Collections;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // 장애물 프리팹
    public Transform player; // 플레이어 Transform
    public float spawnInterval = 2f; // 장애물 생성 간격
    public float spawnDistanceAhead = 20f; // 플레이어 앞에 장애물 생성할 거리
    public float spawnRangeX = 2f; // 장애물의 X축 생성 범위

    private void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            // 장애물 생성 간격 대기
            yield return new WaitForSeconds(spawnInterval);

            // 장애물의 X 위치를 랜덤으로 설정
            float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);

            // 장애물의 Z 위치는 플레이어의 앞쪽으로 설정
            Vector3 spawnPos = new Vector3(spawnPosX, 0.5f, player.position.z + spawnDistanceAhead);

            // 장애물 생성
            Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);
        }
    }
}
