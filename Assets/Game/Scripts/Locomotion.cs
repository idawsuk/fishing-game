using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public class Locomotion : MonoBehaviour
    {
        [SerializeField] private PlayerInputs inputs;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float moveSpeed;
        [SerializeField] private Transform facingTransform;

        private Vector2 movement;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            MoveRelativeToCamera();
        }

        private void MoveRelativeToCamera()
        {
            Vector3 forward = mainCamera.transform.forward;
            Vector3 right = mainCamera.transform.right;
            forward.y = 0;
            right.y = 0;
            forward = forward.normalized;
            right = right.normalized;

            movement = (forward * inputs.Movement.y) + (right * inputs.Movement.x);
            this.transform.Translate(movement, Space.World);
        }
    }
}
