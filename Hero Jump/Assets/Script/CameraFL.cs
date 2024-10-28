using UnityEngine;
  
namespace Script
{
    public class CameraFL : MonoBehaviour
    {
        public Transform player;  // Người chơi (hoặc đối tượng) mà camera sẽ theo dõi
        public float smoothSpeed = 0.125f; // Tốc độ làm mượt chuyển động
        public Vector3 offset;    // Độ lệch giữa camera và người chơi

        void Start()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }
        void FixedUpdate()
        {
            Vector3 desiredPosition = player.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = new Vector3(transform.position.x, smoothedPosition.y, transform.position.z); // Chỉ theo dõi theo chiều dọc
        }
    }
}