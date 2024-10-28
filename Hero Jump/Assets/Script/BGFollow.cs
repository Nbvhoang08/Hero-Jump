using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGFollow : MonoBehaviour
{
    public Transform player; // Tham chiếu đến Player 1
    public float distanceThreshold = 5.0f; // Khoảng cách theo chiều dọc để bắt đầu di chuyển
    public float moveAmount = 1.0f; // Khoảng cách di chuyển lên hoặc xuống

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        // Kiểm tra khoảng cách theo chiều dọc giữa đối tượng và Player 1
        float verticalDistance = transform.position.y - player.position.y;

        if (Mathf.Abs(verticalDistance) > distanceThreshold)
        {
            if (verticalDistance > 0)
            {
                // Player 1 ở dưới, di chuyển đối tượng xuống
                transform.position = new Vector3(transform.position.x, transform.position.y - moveAmount, transform.position.z);
            }
            else
            {
                // Player 1 ở trên, di chuyển đối tượng lên
                transform.position = new Vector3(transform.position.x, transform.position.y + moveAmount, transform.position.z);
            }
        }
    }
}
