using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FishingGame
{
    public class PlayerInputs : MonoBehaviour
    {
        [SerializeField] private InputActions input;
        private InputAction casting;
        private InputAction fishing;
        private InputAction mousePosition;

        public InputAction Casting => casting;
        public InputAction Fishing => fishing;
        public Vector2 MousePosition => mousePosition.ReadValue<Vector2>();

        private void Awake()
        {
            input = new InputActions();
        }

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
            casting = input.CastFishingRod.Casting;
            casting.Enable();

            fishing = input.Fishing.Pull;
            fishing.Enable();

            mousePosition = input.CastFishingRod.MousePosition;
            mousePosition.Enable();
        }

        private void OnDisable()
        {
            casting.Disable();
            fishing.Disable();
            mousePosition.Disable();
        }
    }
}
