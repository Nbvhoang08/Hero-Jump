using Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{
    public GameObject coinPrefab; // Prefab của coin
    public Transform spawnPoint; // Vị trí spawn coin
    public DataGame dataGame;
    [SerializeField] private SpriteRenderer sprite;
    private void Awake()
    {
        sprite.sprite = dataGame.selectedTerrain;
    }
    void Start()
    {
        SpawnCoin();
    }

    void SpawnCoin()
    {
        // Tạo một số ngẫu nhiên từ 0 đến 99
        int randomValue = Random.Range(0, 100);

        // Kiểm tra xác suất
        if (randomValue < 40)
        {
            // 30% xác suất spawn coin
            Instantiate(coinPrefab, spawnPoint.position, spawnPoint.rotation);
        }
        // 60% xác suất không spawn coin (không cần làm gì)
    }
}
