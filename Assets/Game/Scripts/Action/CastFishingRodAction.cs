using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

namespace FishingGame
{
    public class CastFishingRodAction : BaseAction
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private PlayerInputs input;
        [SerializeField] private float maxDistance = 10f;
        [SerializeField] private Transform fishingRodTip;
        [SerializeField] private Tackle tackle;
        [SerializeField] private float castInterval = 2f;
        [SerializeField] private float power;
        [SerializeField] private Ease castEasing;
        [SerializeField] private AnimationCurve castCurve;
        [SerializeField] private float castDuration = 2f;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private LayerMask worldMask;
        [SerializeField] private Animator animator;
        [SerializeField] private FishingRodString fishingRodString;

        public float Power => power;

        private float time = 0;
        private bool backCastStarted = false;
        private bool castStarted = false;
        private Vector3 tackleTargetPosition;

        private Tween powerTween;

        public delegate void CastingEvent(bool isStarted);
        public CastingEvent OnBackCastingStarted;

        private void Awake()
        {
            if(mainCamera == null)
            {
                mainCamera = Camera.main;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        private void OnEnable()
        {
            input.Casting.started += Casting_started;
            input.Casting.performed += Casting_performed;
            fishingRodString.enabled = false;
        }

        private void OnDisable()
        {
            input.Casting.started -= Casting_started;
            input.Casting.performed -= Casting_performed;
        }

        public override void Begin()
        {
            base.Begin();

            tackle.SetVisible(false);
        }

        private void Casting_performed(InputAction.CallbackContext obj)
        {
            tackle.SetVisible(false);
            if(powerTween != null && powerTween.IsActive())
            {
                powerTween.Kill();
            }

            backCastStarted = true;
            OnBackCastingStarted?.Invoke(backCastStarted);
            power = 0;
            powerTween = DOTween.To(() => power, x => power = x, 1, castInterval).SetLoops(-1, LoopType.Yoyo).SetEase(castEasing);

            Ray ray = mainCamera.ScreenPointToRay(input.MousePosition);
            if(Physics.Raycast(ray, out var hit, Mathf.Infinity, worldMask))
            {
                playerController.LookAt(hit.point);
            }
            fishingRodString.enabled = false;
        }

        private void Casting_started(InputAction.CallbackContext obj)
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if(!input.Casting.IsPressed() && backCastStarted)
            {
                animator.SetTrigger("casting");
                backCastStarted = false;
                OnBackCastingStarted?.Invoke(backCastStarted);
                powerTween.Kill();
            }

            if(castStarted)
            {
                time += Time.deltaTime;

                Vector3 pos = Vector3.Lerp(fishingRodTip.position, tackleTargetPosition, time / castDuration);
                pos.y += castCurve.Evaluate(time / castDuration);

                tackle.transform.position = pos;

                if(time > castDuration)
                {
                    castStarted = false;
                }
            }
        }

        public void Cast()
        {
            time = 0;
            tackle.SetVisible(true);
            tackleTargetPosition = this.transform.position + (power * maxDistance * playerController.transform.forward);
            castStarted = true;
            tackle.Cast();
            fishingRodString.enabled = true;
        }
    }
}
