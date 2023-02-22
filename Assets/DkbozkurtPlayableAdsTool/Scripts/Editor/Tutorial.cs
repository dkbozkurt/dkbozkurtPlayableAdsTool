// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt

using DkbozkurtPlayableAdsTool.Scripts.PlaygroundConnections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace DkbozkurtPlayableAdsTool.Scripts.Editor
{
    public partial class DkbPlayableAdsToolManager : EditorWindow
    {
        private GameObject _tutorialConnectionsObj;
        private bool _tutorialWithText = true;
        private bool _tutorialWithTutorialHand = true;
        private bool _tutorialHandWithEndlessLoop = true;
        private bool _tutorialHandWithPointGlow = true;
        private bool _tutorialWithWordSpaceArrow = false;
        
        private void CallTutorialController()
        {
            if (FindObjectOfType<TutorialController>())
            {
                Debug.LogWarning("There is already a TutorialController exist in the scene!");
                return;
            }
            
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
                // TMP_FontAsset fontAsset = Resources.FindObjectsOfTypeAll(typeof(TMP_FontAsset))[0] as TMP_FontAsset;
                TMP_FontAsset fontAsset = Resources.Load<TMP_FontAsset>("DkbozkurtPlayableAdsToolResources/Fonts/Baloo-Regular SDF Ft");
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

                #region Tutorial Hand

                var tutorialHand = GenerateUIObject("TutorialHand", tutorialHandParentRectTransform.transform);
                var tutorialHandRectTransform = tutorialHand.GetComponent<RectTransform>();
                tutorialHandRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                tutorialHandRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                tutorialHandRectTransform.pivot = new Vector2(0.5f, 0.5f);
                LocateRectTransform(tutorialHandRectTransform, new Vector2(0f,0f),new Vector2(380f,380f));
                
                SetComponentAsLastChild(tutorialHandRectTransform);

                #endregion
                
                #region Tutorial Hand Image

                var tutorialHandImage = GenerateUIObject("TutorialHandImage", tutorialHandRectTransform.transform);
                var tutorialHandImageRectTransform = tutorialHandImage.GetComponent<RectTransform>();
                tutorialHandImageRectTransform.anchorMin = new Vector2(0f,1f);
                tutorialHandImageRectTransform.anchorMax = new Vector2(0f,1f);
                tutorialHandImageRectTransform.pivot = new Vector2(0f,1f);
                // sprite.rect can be used for scaling image depends on resource ref witdh and height.
                LocateRectTransform(tutorialHandImageRectTransform, new Vector2(130f,-173f),new Vector2(500f,596f));
                
                var tutorialHandImage_Image = tutorialHandImage.AddComponent<Image>();
                var sprite = Resources.Load<Sprite>("DkbozkurtPlayableAdsToolResources/Textures/Hand");
                
                tutorialHandImage_Image.sprite = sprite;
                tutorialHandImage_Image.raycastTarget = false;
                
                SetComponentAsLastChild(tutorialHandImageRectTransform);

                #endregion
                
                #region Endless Loop Image

                if (_tutorialHandWithEndlessLoop)
                {
                    var tutorialEndlessLoop = GenerateUIObject("TutorialEndlessLoop", tutorialHandParentRectTransform.transform);
                    var tutorialEndlessLoopRectTransform = tutorialEndlessLoop.GetComponent<RectTransform>();
                    tutorialEndlessLoopRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                    tutorialEndlessLoopRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                    tutorialEndlessLoopRectTransform.pivot = new Vector2(0.5f, 0.5f);
                    LocateRectTransform(tutorialEndlessLoopRectTransform, new Vector2(0f,0f),new Vector2(761f, 389f));
                
                    var tutorialEndlessLoopImage = tutorialEndlessLoop.AddComponent<Image>();
                    tutorialEndlessLoopImage.sprite = Resources.Load<Sprite>("DkbozkurtPlayableAdsToolResources/Textures/Infinity");
                    tutorialEndlessLoopImage.raycastTarget = false;
                    SetComponentAsFirstChild(tutorialEndlessLoopRectTransform);
                }
                #endregion

                #region Tutorial Hand Glow Image

                if (_tutorialHandWithPointGlow)
                {
                    var tutorialPointGlow = GenerateUIObject("TutorialPointGlow", tutorialHandRectTransform.transform);
                    var tutorialPointGlowRectTransform = tutorialPointGlow.GetComponent<RectTransform>();
                    tutorialPointGlowRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                    tutorialPointGlowRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                    tutorialPointGlowRectTransform.pivot = new Vector2(0.5f, 0.5f);
                    LocateRectTransform(tutorialPointGlowRectTransform, new Vector2(0f,0f),new Vector2(512f,512f));
                
                    var tutorialPointGlowImage = tutorialPointGlow.AddComponent<Image>();
                    tutorialPointGlowImage.sprite = Resources.Load<Sprite>("DkbozkurtPlayableAdsToolResources/Textures/PointGlow");
                    tutorialPointGlowImage.raycastTarget = false;
                    
                    SetComponentAsFirstChild(tutorialPointGlowRectTransform);
                }
                #endregion
            }
            
            #endregion

            #region Tutorial World Space Arrow

            if (_tutorialWithWordSpaceArrow)
            {
                GameObject tutorialWorldSpaceArrowPrefab =
                    Resources.Load<GameObject>("DkbozkurtPlayableAdsToolResources/Prefabs/TutorialWorldSpaceArrowParent");
                var tutorialWorldSpaceArrow = Instantiate(tutorialWorldSpaceArrowPrefab);
                tutorialWorldSpaceArrow.name = "TutorialWorldSpaceArrowParent";
                tutorialController.TutorialArrowParent = tutorialWorldSpaceArrow.transform;
            }
            
            #endregion
            
            SetComponentAsFirstChild(tutorialConnectionsRectTransform);
            Debug.Log("Tutorial Controller successfully instantiated!");
        }
    }
}
