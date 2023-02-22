// Dogukan Kaan Bozkurt
//      github.com/dkbozkurt

using DkbozkurtPlayableAdsTool.Scripts.PlaygroundConnections;
using UnityEditor;
using UnityEngine;

namespace DkbozkurtPlayableAdsTool.Scripts.Editor
{
    public partial class DkbPlayableAdsToolManager : EditorWindow
    {
        private GameObject _playableGameManager;
        
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
    }
}
