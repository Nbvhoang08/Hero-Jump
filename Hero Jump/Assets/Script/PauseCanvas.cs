using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace Script
{
    public class PauseCanvas :UICanvas
    {
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
            UIManager.Instance.OpenUI<LoadingCanvas>();
            
        }

        public void SoundBtn()
        {
            
        }
    }
}