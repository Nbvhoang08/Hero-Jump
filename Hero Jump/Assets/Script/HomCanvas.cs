using UnityEngine;
using UnityEngine.UI;
namespace Script
{
    public class HomCanvas : UICanvas
    {
        public Sprite OnVolume;
        public Sprite OffVolume;

        [SerializeField] private Image buttonImage;

        void Start()
        {
  
            UpdateButtonImage();
  
        }
        public void playBtn()
        {
            UIManager.Instance.CloseAll();
            UIManager.Instance.OpenUI<ChooseCanvas>();
            
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