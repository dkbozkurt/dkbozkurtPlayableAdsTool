// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt

using UnityEditor;
using UnityEngine;

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
            GetWindow<DkbPlayableAdsToolManager>("Playable Ads Tool");
        }

        private void OnGUI()
        {
            UIVisuals();
        }

        private void UIVisuals()
        {
            CtaArea();
            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            EndCardArea();
            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

            BannerArea();
            
            EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            
            TutorialArea();
        }

        private void CtaArea()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("CTA Controller",EditorStyles.boldLabel);
            if (GUILayout.Button("Import CtaController",GUILayout.Width(200),GUILayout.Height(25)))
            {
                CallCtaController();
            }
            GUILayout.EndHorizontal();
        }

        private void EndCardArea()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("EndCard Controller",EditorStyles.boldLabel);
            if (GUILayout.Button("Import EndCardController",GUILayout.Width(200),GUILayout.Height(25)))
            {
                CallEndCardController();
            }
            GUILayout.EndHorizontal();
        }

        private void BannerArea()
        {
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

        }
        
        private void TutorialArea()
        {
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
    }
}
