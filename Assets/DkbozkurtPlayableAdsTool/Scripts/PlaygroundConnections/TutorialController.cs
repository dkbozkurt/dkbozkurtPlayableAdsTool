using System;
using System.Collections;
using DG.Tweening;
using DkbozkurtPlayableAdsTool.Scripts.Helpers;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

namespace DkbozkurtPlayableAdsTool.Scripts.PlaygroundConnections
{
    public class TutorialController : SingletonBehaviour<TutorialController>
    {
        protected override void OnAwake() { }

        [LunaPlaygroundField("Tutorial Texts", 0, "Tutorial Settings")] [SerializeField]
        private string[] _tutorialTexts = new string[] { };

        [Header("Core Tutorial Properties")] 
        [SerializeField] private bool _autoDeactivateTutorial = false;
        [SerializeField] private float _durationToDeactivateTutorial = 2f;

        [Header("Tutorial Hand Properties")]
        [SerializeField] private GameObject _tutorialHandParent;
        private Animator _tutorialHandAnimator;
        
        [Header("Tutorial Text Properties")]
        [SerializeField] private GameObject _tutorialTextParent;
        [SerializeField] private TextMeshProUGUI _tutorialText;
        
        [Header("Tutorial Arrow Properties")]
        [SerializeField] private Transform _tutorialArrowParent;
        [SerializeField] private Vector3[] _tutorialArrowWorldSpacePositions;

        private Coroutine _tutorialTextCoroutine;
        private Coroutine _tutorialHandCoroutine;
        private Coroutine _tutorialArrowCoroutine;
        
        private void OnEnable()
        {
            TutorialTextSetter(true);
            AnimateTutorialText();
            TutorialHandSetterWithAnimation(true);
            TutorialArrowSetter(true);
            AnimateTutorialArrow();
            
        }
        
        public void TutorialTextSetter(bool status,int index = 0)
        {
            if(IsObjectNull(_tutorialTextParent)) return;
            
            _tutorialTextParent.SetActive(status);
            
            if(_tutorialTextCoroutine != null) StopCoroutine(_tutorialHandCoroutine);
            
            if(!status) return;
            
            _tutorialText.text = _tutorialTexts[index];
            
            if(!_autoDeactivateTutorial) return;
            _tutorialTextCoroutine =
                StartCoroutine(DeactivatorCoroutine(_durationToDeactivateTutorial, 
                    ()=> TutorialTextSetter(false)));
        }

        public void TutorialHandSetterWithAnimation(bool status,string animName="",string animToSetFalse = "")
        {
            if(IsObjectNull(_tutorialHandParent)) return;
            
            if ( status && animName == "")
            {
                Debug.LogError("Tutorial hands animation name is empty !!!");
                return;
            }
            
            _tutorialHandParent.SetActive(status);

            if(_tutorialHandCoroutine != null) StopCoroutine(_tutorialHandCoroutine);

            if (!status) return; 
            
            _tutorialHandAnimator.SetBool(animName,true);
            if(animToSetFalse != "") _tutorialHandAnimator.SetBool(animToSetFalse,false);
            
            if(!_autoDeactivateTutorial) return;
            _tutorialHandCoroutine = 
                StartCoroutine(DeactivatorCoroutine(_durationToDeactivateTutorial,
                    () => TutorialHandSetterWithAnimation(false)));
        }
        
        public void TutorialArrowSetter(bool status,int index = 0)
        {
            if(IsObjectNull(_tutorialArrowParent.gameObject)) return;
            
            _tutorialArrowParent.gameObject.SetActive(status);
            
            if(_tutorialArrowCoroutine != null) StopCoroutine(_tutorialArrowCoroutine);
            
            if (!status) return;

            _tutorialArrowParent.transform.position = _tutorialArrowWorldSpacePositions[index];
            
            if(!_autoDeactivateTutorial) return;
            _tutorialArrowCoroutine = StartCoroutine(DeactivatorCoroutine(_durationToDeactivateTutorial,
                () => TutorialArrowSetter(false)));
        }

        public void CloseUI()
        {
            TutorialArrowSetter(false);
            TutorialHandSetterWithAnimation(false);
            TutorialTextSetter(false);
        }

        private void AnimateTutorialText()
        {
            if(IsObjectNull(_tutorialTextParent)) return;
            _tutorialTextParent.transform.DOScale(Vector3.one * 0.85f, 1.5f).SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void AnimateTutorialArrow()
        {
            if(IsObjectNull(_tutorialArrowParent.gameObject)) return;
            
            _tutorialArrowParent.transform.GetChild(0).DOMoveY(1f, 1f).SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private bool IsObjectNull(GameObject objectToCheck)
        {
            return objectToCheck == null;
        }

        private IEnumerator DeactivatorCoroutine(float duration, Action action = null)
        {
            yield return new WaitForSeconds(duration);
            action?.Invoke();
        }
    }
}
