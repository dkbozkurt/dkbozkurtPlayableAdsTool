// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt

using DkbozkurtPlayableAdsTool.Scripts.PlaygroundConnections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Important to import for tools

namespace DkbozkurtPlayableAdsTool.Scripts.Editor
{
    /// <summary>
    /// TOOLS OPTION'S FUNCTIONS HAS TO BE 'STATIC' !!!
    /// </summary>
    public class DkbPlayableAdsToolManager : EditorWindow
    {
        private static GameObject _playableGameManager;
        private static GameObject _playableParentCanvas;
        
        private static GameObject _endCardConnectionsObj;
        private Button _endCardButton;

        private static GameObject _bannerConnectionsObj;
        private Button _bannerButton;
        private bool _bannerWithIcon = true;
        private bool _bannerWithText = true;
        private bool _bannerWithGetButton = true;

        private static GameObject _tutorialConnectionsObj;
        private bool _tutorialWithText = true;
        private bool _tutorialWithTutorialHand = true;
        private bool _tutorialHandWithEndlessLoop = true;
        private bool _tutorialHandWithPointGlow = true;
        private bool _tutorialWithWordSpaceArrow = false;
        
        [MenuItem("Tools/Dkbozkurt/PlayableAdsTool")]
        public static void ShowWindow()
        {
            GetWindow<DkbPlayableAdsToolManager>("Dkbozkurt Playable Ads Tool");
        }

        private void OnGUI()
        {
            UIButtons();
        }

        private void UIButtons()
        {
            GUILayout.Label("CTA Controller",EditorStyles.boldLabel);
            if (GUILayout.Button("Import CtaController",GUILayout.Width(200),GUILayout.Height(25)))
            {
                CallCtaController();
            }

            GUILayout.Space(20);
            
            GUILayout.Label("EndCard Controller",EditorStyles.boldLabel);
            if (GUILayout.Button("Import EndCardController",GUILayout.Width(200),GUILayout.Height(25)))
            {
                CallEndCardController();
            }
            
            GUILayout.Space(20);
            
            GUILayout.Label("Banner Controller",EditorStyles.boldLabel);
            _bannerWithIcon = EditorGUILayout.Toggle("With Icon", _bannerWithIcon);
            _bannerWithText = EditorGUILayout.Toggle("With Text", _bannerWithText);
            _bannerWithGetButton = EditorGUILayout.Toggle("With Get Button", _bannerWithGetButton);
            if (GUILayout.Button("Import BannerController",GUILayout.Width(200),GUILayout.Height(25)))
            {
                CallBannerController();
            }
            
            GUILayout.Space(20);
            
            GUILayout.Label("Tutorial Controller",EditorStyles.boldLabel);
            _tutorialWithText = EditorGUILayout.Toggle("With Text", _tutorialWithText);
            _tutorialWithTutorialHand = EditorGUILayout.Toggle("With Tutorial Hand", _tutorialWithTutorialHand);
            if (_tutorialWithTutorialHand)
            {
                GUILayout.BeginHorizontal();
                _tutorialHandWithEndlessLoop = EditorGUILayout.Toggle("Hand With Endless Loop Image",
                    _tutorialHandWithEndlessLoop);
                _tutorialHandWithPointGlow =
                    EditorGUILayout.Toggle("Hand With Point Glow Image", _tutorialHandWithPointGlow);
                GUILayout.EndHorizontal();
            }

            _tutorialWithWordSpaceArrow =
                EditorGUILayout.Toggle("With World Space Arrow", _tutorialWithWordSpaceArrow);
            
            if (GUILayout.Button("Import TutorialController",GUILayout.Width(200),GUILayout.Height(25)))
            {
                CallTutorialController();
            }
            
            GUILayout.Space(20);
        }

        private void CallCtaController()
        {
            if (GameObject.Find("PlayableGameManager") != null)
            {
                _playableGameManager = GameObject.Find("PlayableGameManager");

                if (_playableGameManager.TryGetComponent(out CtaController ctaController))
                {
                    return;
                }
                else
                {
                    _playableGameManager.AddComponent<CtaController>();
                    _playableGameManager.transform.position = Vector3.zero;
                }
                return;
            }
            
            _playableGameManager = new GameObject("PlayableGameManager");
            _playableGameManager.AddComponent<CtaController>();
            _playableGameManager.transform.position = Vector3.zero;
            Debug.Log("Playable Game Manager successfully instantiated!");
        }

        private void CallEndCardController()
        {
            if (GameObject.Find("EndCardController")) return;

            if (GameObject.Find("Canvas") == null)
            {
                GenerateCanvasPack();
            }
            else
            {
                _playableParentCanvas = GameObject.Find("Canvas");
            }

            #region EndCardController

            _endCardConnectionsObj = GenerateUIObject("EndCardController",_playableParentCanvas.transform);
            var endCardController = _endCardConnectionsObj.AddComponent<EndCardController>();
            var endCardConnectionsRectTransform = _endCardConnectionsObj.GetComponent<RectTransform>();
            endCardConnectionsRectTransform.anchorMin = Vector2.zero;
            endCardConnectionsRectTransform.anchorMax = Vector2.one;
            endCardConnectionsRectTransform.pivot = new Vector2(0.5f,0.5f);
            endCardConnectionsRectTransform.offsetMax = new Vector2(0, 0);
            endCardConnectionsRectTransform.offsetMin = new Vector2(0, 0);
            
            #endregion

            #region EndCard Background

            GameObject endCardBackground = GenerateUIObject("EndCardBackground",_endCardConnectionsObj.transform);
            AddImageComponent(endCardBackground);
            var endCardBackgroundRectTransform = endCardBackground.GetComponent<RectTransform>();
            endCardBackgroundRectTransform.anchorMin = Vector2.zero;
            endCardBackgroundRectTransform.anchorMax = Vector2.one;
            endCardBackgroundRectTransform.pivot = new Vector2(0.5f,0.5f);
            endCardBackgroundRectTransform.offsetMax = new Vector2(0, 0);
            endCardBackgroundRectTransform.offsetMin = new Vector2(0, 0);

            var endCardBackgroundImage = endCardBackground.GetComponent<Image>();
            endCardBackgroundImage.raycastTarget = true;
            endCardBackgroundImage.color = new Color(0f, 0f, 0f, 0.75f);

            endCardBackground.AddComponent<Button>();
            _endCardButton = endCardBackground.GetComponent<Button>();
            _endCardButton.transition = Selectable.Transition.None;
            
            AddStoreConnectionOntoButton(_endCardButton);

            endCardController.EndCardBackground = endCardBackgroundImage;
            endCardBackground.SetActive(false);

            #endregion

            #region EndCard Icon

            GameObject endCardIcon = GenerateUIObject("EndCardIcon", endCardBackground.transform);
            AddImageComponent(endCardIcon);
            endCardController.EndCardIcon = endCardIcon.GetComponent<Image>();
            LocateRectTransform(endCardIcon.GetComponent<RectTransform>(), new Vector2(0f,650f),new Vector2(650f,650f));

            #endregion

            #region EndCard Text

            GameObject endCardText = GenerateUIObject("EndCardText", endCardBackground.transform);
            AddImageComponent(endCardText);
            endCardController.EndCardText = endCardText.GetComponent<Image>();
            LocateRectTransform(endCardText.GetComponent<RectTransform>(),new Vector2(0f,-150f),new Vector2(1000f,400f));

            #endregion

            #region EndCard PlayButton

            GameObject endCardPlayButton = GenerateUIObject("EndCardPlayButton", endCardBackground.transform);
            AddImageComponent(endCardPlayButton);
            endCardController.EndCardPlayButton = endCardPlayButton.GetComponent<Image>();
            LocateRectTransform(endCardPlayButton.GetComponent<RectTransform>(),new Vector2(0f,-800f),new Vector2(756f,300f));

            #endregion
            
            SetComponentAsLastChild(endCardConnectionsRectTransform);
            
            Debug.Log("End Card Controller successfully instantiated!");
        }

        private void CallBannerController()
        {
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

        private void CallTutorialController()
        {
            if (GameObject.Find("TutorialController")) return;
            
            if (GameObject.Find("Canvas") == null)
            {
                GenerateCanvasPack();
            }
            else
            {
                _playableParentCanvas = GameObject.Find("Canvas");
            }

            #region TutorialController

            _tutorialConnectionsObj = GenerateUIObject("TutorialController", _playableParentCanvas.transform);
            var tutorialController = _tutorialConnectionsObj.AddComponent<TutorialController>();
            var tutorialConnectionsRectTransform = _tutorialConnectionsObj.GetComponent<RectTransform>();
            tutorialConnectionsRectTransform.anchorMin = Vector2.zero;
            tutorialConnectionsRectTransform.anchorMax = Vector2.one;
            tutorialConnectionsRectTransform.pivot = new Vector2(0.5f,0.5f);
            tutorialConnectionsRectTransform.offsetMax = new Vector2(0, 0);
            tutorialConnectionsRectTransform.offsetMin = new Vector2(0, 0);

            #endregion

            #region Tutorial Text Parent

            if (_tutorialWithText)
            {
                var tutorialTextParent = GenerateUIObject("TutorialTextParent", _tutorialConnectionsObj.transform);
                tutorialController.TutorialTextParent = tutorialTextParent;
                var tutorialTextParentRectTransform = tutorialTextParent.GetComponent<RectTransform>();
                tutorialTextParentRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                tutorialTextParentRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                tutorialTextParentRectTransform.pivot = new Vector2(0.5f, 0.5f);
                LocateRectTransform(tutorialTextParentRectTransform, new Vector2(0f,600f),new Vector2(1125f,390f));
                
                #region Tutorial Text

                var tutorialText = GenerateUIObject("TutorialText", tutorialTextParent.transform).AddComponent<TextMeshProUGUI>();
                tutorialController.TutorialText = tutorialText;
                var tutorialTextRectTransform = tutorialText.GetComponent<RectTransform>();
                tutorialTextRectTransform.anchorMin = Vector2.zero;
                tutorialTextRectTransform.anchorMax = Vector2.one;
                tutorialTextRectTransform.pivot = new Vector2(0.5f,0.5f);
                tutorialTextRectTransform.offsetMax = new Vector2(0, 0);
                tutorialTextRectTransform.offsetMin = new Vector2(0, 0);
            
                tutorialText.text = "New Text New Text New Text New Text New Text"; 
                TMP_FontAsset fontAsset=  Instantiate(Resources.FindObjectsOfTypeAll(typeof(TMP_FontAsset))[0] as TMP_FontAsset);
                tutorialText.font = fontAsset;
                tutorialText.fontSize = 110;
                tutorialText.characterSpacing = 0.5f;
                tutorialText.lineSpacing = -65f;
                tutorialText.alignment = TextAlignmentOptions.Center;
                tutorialText.raycastTarget = false;

                #endregion
            }
            #endregion

            #region Tutorial Hand Parent

            if (_tutorialWithTutorialHand)
            {
                var tutorialHandParent = GenerateUIObject("TutorialHandParent", _tutorialConnectionsObj.transform);
                tutorialController.TutorialHandAnimator = tutorialHandParent.AddComponent<Animator>();
                tutorialController.TutorialHandParent = tutorialHandParent;
                var tutorialHandParentRectTransform = tutorialHandParent.GetComponent<RectTransform>();
                tutorialHandParentRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                tutorialHandParentRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                tutorialHandParentRectTransform.pivot = new Vector2(0.5f, 0.5f);
                LocateRectTransform(tutorialHandParentRectTransform, new Vector2(0f,-600f),new Vector2(380f,380f));
                
                #region Tutorial Hand Image

                var tutorialHand = GenerateUIObject("TutorialHand", tutorialHandParentRectTransform.transform);
                var tutorialHandRectTransform = tutorialHand.GetComponent<RectTransform>();
                tutorialHandRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                tutorialHandRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                tutorialHandRectTransform.pivot = new Vector2(0.5f, 0.5f);
                LocateRectTransform(tutorialHandRectTransform, new Vector2(190f,-180f),new Vector2(380f,380f));
                
                var tutorialHandImage = tutorialHand.AddComponent<Image>();
                tutorialHandImage.raycastTarget = false;
                
                SetComponentAsLastChild(tutorialHandRectTransform);

                #endregion
                
                #region Endless Loop Image

                if (_tutorialHandWithEndlessLoop)
                {
                    
                }
                #endregion

                #region Tutorial Hand Glow Image

                if (_tutorialHandWithPointGlow)
                {
                    
                }
                #endregion
            }
            
            #endregion

            #region Tutorial World Space Arrow

            

            #endregion
            
            
            SetComponentAsFirstChild(tutorialConnectionsRectTransform);
            Debug.Log("Tutorial Controller successfully instantiated!");
        }

        private void GenerateCanvasPack()
        {
            _playableParentCanvas = new GameObject("Canvas");
            _playableParentCanvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            var canvasScaler = _playableParentCanvas.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1125, 2436);
            canvasScaler.matchWidthOrHeight =1f;
            _playableParentCanvas.AddComponent<GraphicRaycaster>();

            if(GameObject.Find("Event System")) return;
            
            var eventSystem = new GameObject("Event System");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        private GameObject GenerateUIObject(string name,Transform parent= null)
        {
            var obj = new GameObject(name);
            obj.AddComponent<RectTransform>();
            if(parent != null) obj.transform.SetParent(parent);
            return obj;
        }

        private void AddImageComponent(GameObject targetObj)
        {
            var image = targetObj.AddComponent<Image>();
            image.raycastTarget = false;
        }

        private void LocateRectTransform(RectTransform rectTransform,Vector2 location,Vector2 size)
        {
            rectTransform.anchoredPosition = location;
            rectTransform.sizeDelta = size;
        }

        private void AddStoreConnectionOntoButton(Button button)
        {
            CallCtaController();
            
#if UNITY_EDITOR
            UnityEditor.Events.UnityEventTools.AddPersistentListener(button.onClick, new UnityAction(CtaController.Instance.OpenStore));
#endif
            // In run time, unity event listeners can be added by following lines. 
            // button.onClick.AddListener(()=>CtaController.Instance.OpenStore());
            // button.onClick.AddListener(delegate { CtaController.Instance.OpenStore(); });
        }

        private void SetComponentAsLastChild(RectTransform focusObj)
        {
            if(_playableParentCanvas == null) return;

            focusObj.SetAsLastSibling();
        }

        private void SetComponentAsFirstChild(RectTransform focusObj)
        {
            if(_playableParentCanvas == null) return;
            
            focusObj.SetAsFirstSibling();
        }
    }
}
