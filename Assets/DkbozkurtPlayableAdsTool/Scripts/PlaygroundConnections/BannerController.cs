using System;
using UnityEngine;
using UnityEngine.UI;

namespace DkbozkurtPlayableAdsTool.Scripts.PlaygroundConnections
{
    public class BannerController : MonoBehaviour
    {
        [Header("Banner Properties")] 
        public Image BannerBackgroundImage;
        public Image BannerSplashImage;
        public Image BannerIconImage;
        public Image BannerTextImage;
        public Image BannerGetButtonImage;

        public bool IsActive = true;

        private void Awake()
        {
            BannerBackgroundImage.gameObject.SetActive(IsActive);
        }
    }
}
