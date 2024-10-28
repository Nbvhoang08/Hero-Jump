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

        public GameObject[] backgrounds; // Mảng chứa 3 hình nền
        public float backgroundHeight; // Chiều cao của mỗi hình nền
        public float scrollSpeed; // Tốc độ cuộn
        public Transform player;

        // Start is called before the first frame update
        void Awake()
        {
            playerPrefab = Instantiate(dataGame.selectedCharacter, spawnPos.position, Quaternion.identity);
        }

        void Start()
        {
            player = playerPrefab.transform;
        }

        // Update is called once per frame


        private void Update()
        {
            // Di chuyển nền theo chiều dọc dựa trên vị trí của người chơi
            for (int i = 0; i < backgrounds.Length; i++)
            {
                // Kiểm tra nếu hình nền đã ra khỏi màn hình phía dưới
                if (backgrounds[i].transform.position.y < player.position.y - backgroundHeight)
                {
                    Vector3 newPos = backgrounds[i].transform.position;
                    newPos.y += backgroundHeight * backgrounds.Length;
                    backgrounds[i].transform.position = newPos;
                }
                // Kiểm tra nếu hình nền đã ra khỏi màn hình phía trên
                else if (backgrounds[i].transform.position.y > player.position.y + backgroundHeight)
                {
                    Vector3 newPos = backgrounds[i].transform.position;
                    newPos.y -= backgroundHeight * backgrounds.Length;
                    backgrounds[i].transform.position = newPos;
                }
            }


        }
    }
}

