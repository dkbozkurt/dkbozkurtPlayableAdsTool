// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt

using DkbozkurtPlayableAdsTool.Scripts.PlaygroundConnections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DkbozkurtPlayableAdsTool.Scripts.Editor
{
    public partial class DkbPlayableAdsToolManager : EditorWindow
    {
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

        private bool IsGameObjectAlreadyExistInScene(string gameObjectName)
        {
            if (!GameObject.Find(gameObjectName)) return false;
            
            Debug.LogWarning(gameObjectName + " already exist in the scene!");
            return true;
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