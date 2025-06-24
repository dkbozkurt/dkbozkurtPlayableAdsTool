using DG.Tweening;
using PlayableAdsKit.Scripts.Helpers;
using PlayableAdsKit.Scripts.Utilities;
using UnityEngine;

namespace PlayableAdsKit.Scripts.PlaygroundConnections
{
    public class TutorialController : SingletonBehaviour<TutorialController>
    {
        protected override void OnAwake() { }

        [SerializeField] private RectTransform _textParent;
        [SerializeField] private RectTransform _handParent;
        
        private CanvasGroup _textCanvasGroup;
        private Animator _handAnimator;
        private CanvasGroup _handCanvasGroup;

        private void Awake()
        {
            _textCanvasGroup = _textParent.GetComponent<CanvasGroup>();
            _handAnimator = _handParent.GetComponent<Animator>();
            _handCanvasGroup = _handParent.GetComponent<CanvasGroup>();
        }
        
        private void Start()
        {
            Activate();
        }
        
        public void Activate()
        {
            TutorialTextSetter(true);
            AnimateTutorialText();
        }
        
        public void Deactivate(float duration = 0f)
        {
            DOVirtual.DelayedCall(duration, () =>
            {
                TutorialTextSetter(false);
                TutorialHandSetter(false);
            });
        }


        public void TutorialTextSetter(bool status)
        {
            if (!status)
            {
                _textCanvasGroup.DOFade(0f, 0.2f).OnComplete(() =>
                {
                    _textParent.gameObject.SetActive(false);
                });
                return;
            }

            _textParent.gameObject.SetActive(true);
            _textCanvasGroup.alpha = 1f;
        }
        
        public void TutorialHandSetter(bool status, string animName="", string animToSetFalse = "")
        {
            if (!status)
            {
                _handCanvasGroup.DOFade(0f,0.2f).OnComplete(() =>
                {
                    _handAnimator.gameObject.SetActive(false);
                });
                return;
            }

            if (animName == "")
            {
                Debug.LogError("Animation name can not be empty");
                return;
            }
            
            _handAnimator.gameObject.SetActive(true);
            _handCanvasGroup.alpha = 1f;
            
            if(animToSetFalse != "")
                _handAnimator.SetBool(animToSetFalse, false);
            
            _handAnimator.SetBool(animName, true);
        }

        public void SetHandPositionWithClick(Transform obj)
        {
            _handParent.position = PlyAdsKitUtils.GetScreenPositionOfObject(obj);
            TutorialHandSetter(true,"Click");
        }
        
        public void SetHandPositionForUIWithClick(RectTransform obj)
        {
            _handParent.anchoredPosition = obj.anchoredPosition;
            TutorialHandSetter(true,"Click");
        }

        private void AnimateTutorialText()
        {
            var initialScale = _textParent.transform.localScale;
            var targetScale = initialScale * 0.85f;
            _textParent.transform.DOScale(targetScale, 1.5f).SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }
    }
}