using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public class FishingAction : BaseAction
    {
        [SerializeField] private PlayerInputs input;
        [SerializeField] private Tackle tackle;

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
        }

        private void OnDisable()
        {
            input.Fishing.started -= Fishing_started;
        }

        private void Fishing_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            tackle.PullTackle();
        }
    }
}
