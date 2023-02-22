// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt

using DkbozkurtPlayableAdsTool.Scripts.PlaygroundConnections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace DkbozkurtPlayableAdsTool.Scripts.Editor
{
    public partial class DkbPlayableAdsToolManager : EditorWindow
    {
        private GameObject _bannerConnectionsObj;
        private Button _bannerButton;
        private bool _bannerWithIcon = true;
        private bool _bannerWithText = true;
        private bool _bannerWithGetButton = true;
        
        private void CallBannerController()
        {
            if (FindObjectOfType<BannerController>())
            {
                Debug.LogWarning("There is already a BannerController exist in the scene!");
                return;
            }
            
            if (GameObject.Find("BannerController")) return;
            
            if (GameObject.Find("Canvas") == null)
            {
                GenerateCanvasPack();
            }
            else
            {
                _playableParentCanvas = GameObject.Find("Canvas");
            }

            #region BannerController
            
            _bannerConnectionsObj = GenerateUIObject("BannerController",_playableParentCanvas.transform);
            var bannerController = _bannerConnectionsObj.AddComponent<BannerController>();
            var bannerConnectionsRectTransform = _bannerConnectionsObj.GetComponent<RectTransform>();
            bannerConnectionsRectTransform.anchorMin = new Vector2(0.5f,0);
            bannerConnectionsRectTransform.anchorMax = new Vector2(0.5f,0);
            bannerConnectionsRectTransform.pivot = new Vector2(0.5f,0);
            bannerConnectionsRectTransform.offsetMax = new Vector2(1364.03f, 138.0483f);
            bannerConnectionsRectTransform.offsetMin = new Vector2(0, 0);
            bannerConnectionsRectTransform.anchoredPosition = Vector2.zero;

            #endregion

            #region Banner Background
            
            GameObject bannerBackground = GenerateUIObject("BannerBackground",_bannerConnectionsObj.transform);
            AddImageComponent(bannerBackground);
            var bannerBackgroundRectTransform = bannerBackground.GetComponent<RectTransform>();
            bannerBackgroundRectTransform.anchorMin = Vector2.zero;
            bannerBackgroundRectTransform.anchorMax = Vector2.one;
            bannerBackgroundRectTransform.pivot = new Vector2(0.5f,0.5f);
            bannerBackgroundRectTransform.offsetMax = new Vector2(0, 0);
            bannerBackgroundRectTransform.offsetMin = new Vector2(0, 0);

            var bannerBackgroundImage = bannerBackground.GetComponent<Image>();
            bannerBackgroundImage.raycastTarget = true;
            bannerBackgroundImage.color = new Color(0f, 0f, 0f, 0.75f);

            bannerBackground.AddComponent<Button>();
            _bannerButton = bannerBackground.GetComponent<Button>();
            _bannerButton.transition = Selectable.Transition.None;
            
            AddStoreConnectionOntoButton(_bannerButton);

            bannerController.BannerBackgroundImage = bannerBackgroundImage;
            bannerBackground.SetActive(bannerController.IsActive);
            
            #endregion
            
            #region Banner Splash Image
            
            GameObject bannerSplash = GenerateUIObject("BannerSplash", bannerBackground.transform);
            AddImageComponent(bannerSplash);
            bannerController.BannerSplashImage = bannerSplash.GetComponent<Image>();
            LocateRectTransform(bannerSplash.GetComponent<RectTransform>(), new Vector2(0f,0f),new Vector2(1125f,138));

            #endregion

            #region Banner Icon Image

            if (_bannerWithIcon)
            {
                GameObject bannerIcon = GenerateUIObject("BannerIcon", bannerBackground.transform);
                AddImageComponent(bannerIcon);
                bannerController.BannerIconImage = bannerIcon.GetComponent<Image>();
                bannerIcon.GetComponent<Image>().color = Color.black;
                LocateRectTransform(bannerIcon.GetComponent<RectTransform>(), new Vector2(-364.75f,0f),new Vector2(120f,120f));    
            }
            
            #endregion
            
            #region Banner Text Image

            if (_bannerWithText)
            {
                GameObject bannerText = GenerateUIObject("BannerText", bannerBackground.transform);
                AddImageComponent(bannerText);
                bannerController.BannerTextImage = bannerText.GetComponent<Image>();
                bannerText.GetComponent<Image>().color = Color.black;
                LocateRectTransform(bannerText.GetComponent<RectTransform>(), new Vector2(9.375f,0f),new Vector2(547.75f,120f));    
            }
            
            #endregion
            
            #region Banner Get Button Image

            if (_bannerWithGetButton)
            {
                GameObject bannerGetButton = GenerateUIObject("BannerGetButton", bannerBackground.transform);
                AddImageComponent(bannerGetButton);
                bannerController.BannerGetButtonImage = bannerGetButton.GetComponent<Image>();
                bannerGetButton.GetComponent<Image>().color = Color.black;
                LocateRectTransform(bannerGetButton.GetComponent<RectTransform>(), new Vector2(429.25f,0f),new Vector2(200f,120f));    
            }
            
            #endregion
            
            SetComponentAsFirstChild(bannerConnectionsRectTransform);
            Debug.Log("Banner Controller successfully instantiated!");
        }
    }
}
