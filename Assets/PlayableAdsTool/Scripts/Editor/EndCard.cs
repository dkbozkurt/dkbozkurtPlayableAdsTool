using PlayableAdsTool.Scripts.PlaygroundConnections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace PlayableAdsTool.Scripts.Editor
{
    public partial class PlayableAdsToolManager : EditorWindow
    {
        private GameObject _endCardConnectionsObj;
        private Button _endCardButton;
        
        private void CallEndCardController()
        {
            if (FindObjectOfType<EndCardController>())
            {
                Debug.LogWarning("There is already an EndCardController exist in the scene!");
                return;
            }
            
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
    }
}
