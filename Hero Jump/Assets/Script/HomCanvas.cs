using UnityEngine;
using UnityEngine.UI;
namespace Script
{
    public class HomCanvas : UICanvas
    {
        public Sprite OnVolume;
        public Sprite OffVolume;
        private bool isSoundOn = true;
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
            isSoundOn = !isSoundOn;
            UpdateButtonImage();
            // Thêm chức năng bật/tắt âm thanh tại đây
            // if (isSoundOn)
            // {
            //     // Bật âm thanh
            //     AudioListener.volume = 1;
            // }
            // else
            // {
            //     // Tắt âm thanh
            //     AudioListener.volume = 0;
            // }
        }

        private void UpdateButtonImage()
        {
            if (isSoundOn)
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