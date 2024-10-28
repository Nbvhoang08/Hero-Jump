using System;
using System.Collections;   
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Script
{
    public class LoadingCanvas :UICanvas
    {
        private void OnEnable()
        {
            StartCoroutine(LoadGamePlayScene());
        }
        private IEnumerator LoadGamePlayScene()
        {
            yield return new WaitForSeconds(2f);

            string currentScene = SceneManager.GetActiveScene().name;

            if (currentScene == "GamePlay")
            {
                SceneManager.LoadScene("Home");
                yield return null;
                yield return null;
                UIManager.Instance.CloseAll();
                UIManager.Instance.OpenUI<HomCanvas>();
            }
            else if (currentScene == "Home")
            {
                SceneManager.LoadScene("GamePlay");
                yield return null;
               
                yield return null;
                UIManager.Instance.CloseAll();
                UIManager.Instance.OpenUI<GamePlayCanvas>();
            }
        }
    }
}