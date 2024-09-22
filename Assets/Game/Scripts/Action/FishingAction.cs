using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public class FishingAction : BaseAction
    {
        [SerializeField] private PlayerInputs input;
        [SerializeField] private Tackle tackle;
        [SerializeField] private Animator animator;
        [SerializeField] private FishingRodString fishingRodString;

        private Fish fish;

        public delegate void FishingEvent(Fish fish);
        public FishingEvent OnFishingEnd;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnEnable()
        {
            input.Fishing.started += Fishing_started;
            fish = null;
        }

        private void OnDisable()
        {
            input.Fishing.started -= Fishing_started;
            //fishingRodString.enabled = false;
        }

        private void Fishing_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            animator.SetTrigger("reeling");
            fish = tackle.PullTackle();
        }

        public void FishingEnd()
        {
            if(fish != null)
            {
                fish.gameObject.SetActive(false);
            }
            OnFishingEnd?.Invoke(fish);
        }
    }
}
