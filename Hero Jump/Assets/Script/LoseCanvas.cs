using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
namespace Script
{
    public class LoseCanvas :UICanvas
    {
        public Sprite OnVolume;
        public Sprite OffVolume;

        [SerializeField] private Image buttonImage;

        void OnEnable()
        {

            UpdateButtonImage();

        }
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
            SoundManger.Instance.TurnOn = !SoundManger.Instance.TurnOn;
            UpdateButtonImage();
        }

        IEnumerator LoadHomeSence()
        {
            yield return new WaitForSeconds(0.2f);
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<LoadingCanvas>();
            
        }
      

        private void UpdateButtonImage()
        {
            if (SoundManger.Instance.TurnOn)
            {
                buttonImage.sprite = OnVolume;
            }
            else
            {
                buttonImage.sprite = OffVolume;
            }
        }
    }
}