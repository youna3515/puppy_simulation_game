using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    public GameObject roadChunkPrefab; // 길 구간 프리팹
    public GameObject obstaclePrefab; // 장애물 프리팹
    public float chunkLength = 10f; // 구간의 길이
    public Transform player; // 플레이어 Transform
    public int numObstaclesPerChunk = 1; // 각 구간당 장애물 개수 (조정 가능)

    private Queue<GameObject> activeChunks = new Queue<GameObject>();
    private Vector3 nextSpawnPosition;

    void Start()
    {
        nextSpawnPosition = Vector3.zero;

        // 처음 몇 개의 길 구간을 생성
        for (int i = 0; i < 5; i++)
        {
            SpawnChunk();
        }
    }

    void Update()
    {
        // 플레이어가 다음 스폰 위치에 가까워지면 새로운 구간을 스폰
        if (player.position.z > nextSpawnPosition.z - (chunkLength * 3))
        {
            SpawnChunk();
            RemoveOldestChunk();
        }
    }

    void SpawnChunk()
    {
        GameObject newChunk = Instantiate(roadChunkPrefab, nextSpawnPosition, Quaternion.identity);
        newChunk.tag = "Ground"; // 길 구간 태그 설정
        activeChunks.Enqueue(newChunk);
        nextSpawnPosition.z += chunkLength; // 다음 구간의 시작 위치를 업데이트

        // 장애물 생성
        for (int i = 0; i < numObstaclesPerChunk; i++)
        {
            if (Random.value > 0.7f) // 70% 확률로 장애물 생성 (확률 조정 가능)
            {
                float obstacleXPosition = Random.Range(-2f, 2f); // 길의 랜덤 위치 (길의 폭에 맞게 조정)

                Vector3 obstaclePosition = new Vector3(
                    obstacleXPosition,
                    0f, // 장애물의 높이 (바닥에서 조금 띄워서 생성)
                    nextSpawnPosition.z - chunkLength + Random.Range(2f, chunkLength) // 구간 내 랜덤 위치
                );
                GameObject obstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity, newChunk.transform);
                obstacle.tag = "Obstacle"; // 장애물 태그 설정
            }
        }
    }

    void RemoveOldestChunk()
    {
        if (activeChunks.Count > 5)
        {
            GameObject oldestChunk = activeChunks.Dequeue();
            Destroy(oldestChunk);
        }
    }
}
