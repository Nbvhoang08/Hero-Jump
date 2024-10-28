using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
namespace Script
{
    public class LoseCanvas :UICanvas
    {
        public void RetryBtn()
        {
            UIManager.Instance.ResumeGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            UIManager.Instance.CloseUI<LoseCanvas>(0.1f);
        }
        public void HomeBtn()
        {
            UIManager.Instance.ResumeGame();
            StartCoroutine(LoadHomeSence());
            
        }

        public void SoundBtn()
        {
            
        }

        IEnumerator LoadHomeSence()
        {
            yield return new WaitForSeconds(0.2f);
            UIManager.Instance.OpenUI<LoadingCanvas>();
            
        }
    }
}