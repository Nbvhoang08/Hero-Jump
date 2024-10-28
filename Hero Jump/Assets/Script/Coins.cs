using UnityEngine;

namespace Script
{
    public class Coins : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // Tăng số lượng coins
                int coins = PlayerPrefs.GetInt("Coins", 0);
                coins += 10; // Hoặc số lượng coins bạn muốn thêm
                PlayerPrefs.SetInt("Coins", coins);
                PlayerPrefs.Save();

                // Hiển thị thông báo hoặc âm thanh thu thập coin
                Debug.Log("Coin collected! Total coins: " + coins);

                // Hủy đối tượng coin sau khi thu thập
                Destroy(gameObject);
            }
        }
    }
}