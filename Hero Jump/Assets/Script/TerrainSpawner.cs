using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script
{
    public class TerrainSpawner : MonoBehaviour
    {
        [Header("Spawn Settings")]
    [SerializeField] private GameObject terrainPrefab; // Prefab của khối địa hình
    [SerializeField] private float minVerticalGap = 3f; // Khoảng cách tối thiểu giữa các hàng
    [SerializeField] private float maxVerticalGap = 6f; // Khoảng cách tối đa giữa các hàng
    [SerializeField] private float spawnTriggerDistance = 10f; // Khoảng cách để trigger spawn khối mới
    
    private Camera mainCamera;
    private float screenLeftBound;
    private float screenRightBound;
    private float lastSpawnPositionY; // Vị trí Y của khối cuối cùng được spawn
    [SerializeField] private float highestSpawnedY; // Vị trí Y cao nhất đã spawn
    private Transform playerTransform; // Transform của player
    private float lastPlayerY; // Vị trí Y cuối cùng của player để check khoảng cách
    
    // List để lưu các khối đã spawn để có thể tái sử dụng
    private List<GameObject> spawnedTerrains = new List<GameObject>();
    
    private void Start()
    {
        mainCamera = Camera.main;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Tính toán bound của màn hình theo chiều ngang
        Vector2 screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        screenLeftBound = -screenBounds.x;
        screenRightBound = screenBounds.x;
        
        // Khởi tạo các giá trị ban đầu
        lastSpawnPositionY = playerTransform.position.y;
        highestSpawnedY = lastSpawnPositionY;
        lastPlayerY = playerTransform.position.y;
        
        // Spawn khối đầu tiên
        SpawnInitialTerrain();
    }
    
    private void Update()
    {
        // Kiểm tra khoảng cách di chuyển của player
        float playerMovementDistance = playerTransform.position.y - lastPlayerY;
        
        if (playerMovementDistance >= spawnTriggerDistance)
        {
            SpawnNewTerrain();
            lastPlayerY = playerTransform.position.y;
            
            // Cleanup các khối địa hình quá xa
            CleanupOldTerrains();
        }
    }
    
    private void SpawnInitialTerrain()
    {
        // Spawn 6 terrain blocks, each 2 units apart vertically, with random x-coordinate between 0 and 2
        for (int i = 0; i < 6; i++)
        {
            float randomX = Random.Range(-2.75f, 2.75f);
            float positionY = lastSpawnPositionY + (i * 3)+ 1;
            SpawnTerrainBlock(new Vector2(randomX, positionY));
            highestSpawnedY = positionY ;
        }
    }

    
    private void SpawnNewTerrain()
    {
        // Tính toán khoảng cách Y cho khối mới
        float verticalGap = Random.Range(minVerticalGap, maxVerticalGap);
        float newY = highestSpawnedY + verticalGap;
        
        // Tính toán vị trí X ngẫu nhiên trong bound màn hình
        float newX = Random.Range(screenLeftBound, screenRightBound);
        
        // Spawn khối mới
        SpawnTerrainBlock(new Vector2(newX, newY));
        
        // Cập nhật vị trí Y cao nhất
        highestSpawnedY = newY;
    }
    
    private void SpawnTerrainBlock(Vector2 position)
    {
        GameObject terrain;
        
        // Tái sử dụng khối cũ nếu có
        if (spawnedTerrains.Count > 0 && spawnedTerrains[0].transform.position.y < playerTransform.position.y - 20f)
        {
            terrain = spawnedTerrains[0];
            spawnedTerrains.RemoveAt(0);
            terrain.transform.position = position;
        }
        else
        {
            // Tạo khối mới nếu không có khối nào để tái sử dụng
            terrain = Instantiate(terrainPrefab, position, Quaternion.identity);
        }
        
        spawnedTerrains.Add(terrain);
    }
    
    private void CleanupOldTerrains()
    {
        // Xóa các khối quá xa khỏi player
        float cleanupThreshold = playerTransform.position.y - 20f;
        spawnedTerrains.RemoveAll(terrain =>
        {
            if (terrain.transform.position.y < cleanupThreshold)
            {
                Destroy(terrain);
                return true;
            }
            return false;
        });
    }
    
    // Vẽ gizmos để debug
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        
        Gizmos.color = Color.red;
        // Vẽ bound màn hình
        Gizmos.DrawLine(new Vector3(screenLeftBound, -100, 0), new Vector3(screenLeftBound, 100, 0));
        Gizmos.DrawLine(new Vector3(screenRightBound, -100, 0), new Vector3(screenRightBound, 100, 0));
    }

    
    }
}