using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Script
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private DataGame dataGame;
        public Transform spawnPos;
        
        
        
        public Transform player;

    
        void Awake()
        {
            playerPrefab = Instantiate(dataGame.selectedCharacter, spawnPos.position, Quaternion.identity);
        }

        void Start()
        {
            player = playerPrefab.transform;
        }

        // Update is called once per frame


        
    }
}

