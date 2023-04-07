using System;
using System.Collections;
using DkbozkurtPlayableAdsTool.Scripts.Helpers;
using UnityEngine;

namespace PlayableAdsTool.Scripts.PlaygroundConnections
{
    public class TutorialController : SingletonBehaviour<TutorialController>
    {
        protected override void OnAwake()
        {
        }

        [Header("Core Tutorial Properties")] [SerializeField]
        private bool _autoDeactivateTutorial = false;

        [SerializeField] private float _durationToDeactivateTutorial = 2f;

        [Header("Tutorial Hand Properties")] public GameObject TutorialHandParent;
        public Animator TutorialHandAnimator;

        private Coroutine _tutorialHandCoroutine;

        private void OnEnable()
        {
            TutorialHandSetterWithAnimation(true);
        }

        public void TutorialHandSetterWithAnimation(bool status, string animName = "", string animToSetFalse = "")
        {
            if (IsObjectNull(TutorialHandParent)) return;

            if (status && animName == "")
            {
                Debug.LogError("Tutorial hands animation name is empty !!!");
                return;
            }

            TutorialHandParent.SetActive(status);

            if (_tutorialHandCoroutine != null) StopCoroutine(_tutorialHandCoroutine);

            if (!status) return;

            TutorialHandAnimator.SetBool(animName, true);
            if (animToSetFalse != "") TutorialHandAnimator.SetBool(animToSetFalse, false);

            if (!_autoDeactivateTutorial) return;
            _tutorialHandCoroutine =
                StartCoroutine(DeactivatorCoroutine(_durationToDeactivateTutorial,
                    () => TutorialHandSetterWithAnimation(false)));
        }

        public void CloseUI()
        {
            TutorialHandSetterWithAnimation(false);
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