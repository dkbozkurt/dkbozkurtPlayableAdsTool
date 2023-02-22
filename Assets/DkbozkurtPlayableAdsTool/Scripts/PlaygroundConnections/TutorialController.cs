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
        private string[] _tutorialTexts = new string[]{};

        [Header("Core Tutorial Properties")] 
        [SerializeField] private bool _autoDeactivateTutorial = false;
        [SerializeField] private float _durationToDeactivateTutorial = 2f;

        [Header("Tutorial Text Properties")]
        public GameObject TutorialTextParent;
        public TextMeshProUGUI TutorialText;
        
        [Header("Tutorial Hand Properties")]
        public GameObject TutorialHandParent;
        public Animator TutorialHandAnimator;

        [Header("Tutorial Arrow Properties")]
        public Transform TutorialArrowParent;
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
            if (_tutorialTexts.Length <= 0)
            {
                Debug.LogError("Fill Tutorial Text array !!!");
                return;
            }
            if(IsObjectNull(TutorialTextParent)) return;
            
            TutorialTextParent.SetActive(status);
            
            if(_tutorialTextCoroutine != null) StopCoroutine(_tutorialHandCoroutine);
            
            if(!status) return;
            
            TutorialText.text = _tutorialTexts[index];
            
            if(!_autoDeactivateTutorial) return;
            _tutorialTextCoroutine =
                StartCoroutine(DeactivatorCoroutine(_durationToDeactivateTutorial, 
                    ()=> TutorialTextSetter(false)));
        }

        public void TutorialHandSetterWithAnimation(bool status,string animName="",string animToSetFalse = "")
        {
            if(IsObjectNull(TutorialHandParent)) return;
            
            if ( status && animName == "")
            {
                Debug.LogError("Tutorial hands animation name is empty !!!");
                return;
            }
            
            TutorialHandParent.SetActive(status);

            if(_tutorialHandCoroutine != null) StopCoroutine(_tutorialHandCoroutine);

            if (!status) return; 
            
            TutorialHandAnimator.SetBool(animName,true);
            if(animToSetFalse != "") TutorialHandAnimator.SetBool(animToSetFalse,false);
            
            if(!_autoDeactivateTutorial) return;
            _tutorialHandCoroutine = 
                StartCoroutine(DeactivatorCoroutine(_durationToDeactivateTutorial,
                    () => TutorialHandSetterWithAnimation(false)));
        }
        
        public void TutorialArrowSetter(bool status,int index = 0)
        {
            if (_tutorialArrowWorldSpacePositions.Length <= 0)
            {
                Debug.LogError("Fill Tutorial World Space Arrow Positions array !!!");
                return;
            }
            
            if(IsObjectNull(TutorialArrowParent.gameObject)) return;
            
            TutorialArrowParent.gameObject.SetActive(status);
            
            if(_tutorialArrowCoroutine != null) StopCoroutine(_tutorialArrowCoroutine);
            
            if (!status) return;

            TutorialArrowParent.transform.position = _tutorialArrowWorldSpacePositions[index];
            
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
            if(IsObjectNull(TutorialTextParent)) return;
            TutorialTextParent.transform.DOScale(Vector3.one * 0.85f, 1.5f).SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }

        private void AnimateTutorialArrow()
        {
            if(IsObjectNull(TutorialArrowParent.gameObject)) return;
            
            TutorialArrowParent.transform.GetChild(0).DOMoveY(1f, 1f).SetEase(Ease.Linear)
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
