using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Script
{
    public class GamePlayCanvas : UICanvas
    {   
        public Text coinsText;
        public void PauseBtn()
        {
            UIManager.Instance.PauseGame();
            UIManager.Instance.OpenUI<PauseCanvas>();
        }

        public void Update()
        {
            coinsText.text = PlayerPrefs.GetInt("Coins", 0).ToString();
        }
    }
}