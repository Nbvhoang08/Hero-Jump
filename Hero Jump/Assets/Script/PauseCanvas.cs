using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Script
{
    public class PauseCanvas :UICanvas
    {
        public Sprite OnVolume;
        public Sprite OffVolume;

        [SerializeField] private Image buttonImage;
        void OnEnable()
        {

            UpdateButtonImage();

        }
        public void ResumeBtn()
        {
            UIManager.Instance.ResumeGame();
            UIManager.Instance.CloseUIDirectly<PauseCanvas>();
        }

        public void HomeBtn()
        {
            UIManager.Instance.ResumeGame();
            StartCoroutine(LoadHomeSence());
        }

        IEnumerator LoadHomeSence()
        {
            yield return new WaitForSeconds(0.2f);
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<LoadingCanvas>();
           
            
        }

        public void SoundBtn()
        {
            SoundManger.Instance.TurnOn = !SoundManger.Instance.TurnOn;
            UpdateButtonImage();
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