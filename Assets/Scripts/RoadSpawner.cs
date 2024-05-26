using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] GameObject _roadChunkPrefab; // 길 구간 프리팹
    [SerializeField] GameObject _obstaclePrefab; // 장애물 프리팹
    [SerializeField] List<GameObject> _backgroundPrefabs; // 나무 에셋들
    string _backgroundPrefabPath = "Prefabs/Scene/TakeWalkScene/Trees";
    [SerializeField] float _chunkLength; // 구간의 길이
    [SerializeField] Transform _player; // 플레이어 Transform
    public Transform Player
    {
        get
        {
            return _player;
        }
        set
        {
            _player = value;
        }
    }
    [SerializeField] int _numObstaclesPerChunk = 1; // 각 구간당 장애물 개수 (조정 가능)
    [SerializeField] float _spawnRangeX; // 산책로 양 옆으로의 스폰 범위
    [SerializeField] float _roadWidth; // 산책로의 폭
    [SerializeField] int _numBackgroundsPerChunk; // 각 구간당 배경 에셋 개수
    [SerializeField] float _backgroundSpawnDistance; // 배경 에셋들이 산책로를 따라 배치될 간격

    [SerializeField] List<Sprite> _sprites;
    string _spritePath = "Images/Obstacle/";

    private Queue<GameObject> _activeChunks = new Queue<GameObject>();
    private Vector3 _nextSpawnPosition;

    void Start()
    {
        _sprites = Resources.LoadAll<Sprite>(_spritePath).ToList<Sprite>();

        _backgroundPrefabs = Resources.LoadAll<GameObject>(_backgroundPrefabPath).ToList<GameObject>();
        _nextSpawnPosition = Vector3.zero;

        // 플레이어의 roadWidth 설정
        _player.GetComponent<TakeWalkPuppyController>().SetRoadWidth(_roadWidth);

        // 처음 몇 개의 길 구간을 생성
        for (int i = 0; i < 20; i++)
        {
            SpawnChunk();
        }
    }

    void Update()
    {
        // 플레이어가 다음 스폰 위치에 가까워지면 새로운 구간을 스폰
        if (_player.position.z > _nextSpawnPosition.z - (_chunkLength * 20))
        {
            SpawnChunk();
            RemoveOldestChunk();
        }
    }

    void SpawnChunk()
    {
        GameObject newChunk = Instantiate(_roadChunkPrefab, _nextSpawnPosition, Quaternion.identity);
        newChunk.tag = "Ground"; // 길 구간 태그 설정
        _activeChunks.Enqueue(newChunk);
        _nextSpawnPosition.z += _chunkLength; // 다음 구간의 시작 위치를 업데이트

        // 장애물 생성
        for (int i = 0; i < _numObstaclesPerChunk; i++)
        {
            if (Random.value > 0.7f) // 70% 확률로 장애물 생성 (확률 조정 가능)
            {
                float obstacleXPosition = Random.Range(-2f, 2f); // 길의 랜덤 위치 (길의 폭에 맞게 조정)

                Vector3 obstaclePosition = new Vector3(
                    obstacleXPosition,
                    0f, // 장애물의 높이 (바닥에서 조금 띄워서 생성)
                    _nextSpawnPosition.z - _chunkLength + Random.Range(2f, _chunkLength) // 구간 내 랜덤 위치
                );
                GameObject obstacle = Instantiate(_obstaclePrefab, obstaclePosition, Quaternion.identity, newChunk.transform);
                obstacle.GetComponentInChildren<SpriteRenderer>().sprite = _sprites[UnityEngine.Random.Range(0, _sprites.Count)];
                obstacle.tag = "Obstacle"; // 장애물 태그 설정
            }
        }

        // 배경 에셋 생성
        SpawnBackgrounds(newChunk);
    }

    void SpawnBackgrounds(GameObject chunk)
    {
        float roadWidth = this._roadWidth; // RoadManager에서 roadWidth 값 가져오기
        int numSpawns = (int)(_chunkLength / _backgroundSpawnDistance);

        for (int i = 0; i < numSpawns; i++)
        {
            // 산책로를 따라 Z축으로 이동
            float spawnZ = chunk.transform.position.z + i * _backgroundSpawnDistance;

            // 산책로 양 옆에 배치
            float leftSpawnX = -roadWidth - Random.Range(0, _spawnRangeX);
            float rightSpawnX = roadWidth + Random.Range(0, _spawnRangeX);

            Vector3 leftSpawnPosition = new Vector3(leftSpawnX, 0, spawnZ);
            Vector3 rightSpawnPosition = new Vector3(rightSpawnX, 0, spawnZ);

            // 랜덤으로 에셋 선택
            GameObject leftPrefabToSpawn = _backgroundPrefabs[Random.Range(0, _backgroundPrefabs.Count)];
            GameObject rightPrefabToSpawn = _backgroundPrefabs[Random.Range(0, _backgroundPrefabs.Count)];

            // 에셋 스폰
            Instantiate(leftPrefabToSpawn, leftSpawnPosition, Quaternion.identity, chunk.transform);
            Instantiate(rightPrefabToSpawn, rightSpawnPosition, Quaternion.identity, chunk.transform);
        }
    }

    void RemoveOldestChunk()
    {
        if (_activeChunks.Count > 30)
        {
            GameObject oldestChunk = _activeChunks.Dequeue();
            Destroy(oldestChunk);
        }
    }
}
