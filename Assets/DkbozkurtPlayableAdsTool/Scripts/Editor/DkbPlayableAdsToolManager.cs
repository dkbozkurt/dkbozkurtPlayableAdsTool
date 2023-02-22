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
    public partial class DkbPlayableAdsToolManager : EditorWindow
    {
        private GameObject _playableParentCanvas;
        
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
            GUILayout.BeginHorizontal();
            GUILayout.Label("CTA Controller",EditorStyles.boldLabel);
            if (GUILayout.Button("Import CtaController",GUILayout.Width(200),GUILayout.Height(25)))
            {
                CallCtaController();
            }
            GUILayout.EndHorizontal();
            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            GUILayout.BeginHorizontal();
            GUILayout.Label("EndCard Controller",EditorStyles.boldLabel);
            if (GUILayout.Button("Import EndCardController",GUILayout.Width(200),GUILayout.Height(25)))
            {
                CallEndCardController();
            }
            GUILayout.EndHorizontal();
            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Banner Controller",EditorStyles.boldLabel);
            if (GUILayout.Button("Import BannerController",GUILayout.Width(200),GUILayout.Height(25)))
            {
                CallBannerController();
            }
            
            GUILayout.EndHorizontal();
            _bannerWithIcon = EditorGUILayout.Toggle("With Icon", _bannerWithIcon);
            _bannerWithText = EditorGUILayout.Toggle("With Text", _bannerWithText);
            _bannerWithGetButton = EditorGUILayout.Toggle("With Get Button", _bannerWithGetButton);

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Tutorial Controller",EditorStyles.boldLabel);
            if (GUILayout.Button("Import TutorialController",GUILayout.Width(200),GUILayout.Height(25)))
            {
                CallTutorialController();
            }
            GUILayout.EndHorizontal();
            
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

            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

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

        private void PreCheckForAlreadyExist()
        {
            // TODO pre check if there is a named object already in the scene.
        }

        public void SetComponentAsLastChild(RectTransform focusObj)
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
