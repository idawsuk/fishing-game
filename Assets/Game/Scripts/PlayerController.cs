using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FishingGame
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Vector3 lookAtDirection;
        [SerializeField] private float turnRate = 3;
        [SerializeField] private PlayerInputs inputs;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float moveSpeed;
        [SerializeField] private Transform facingTransform;
        [SerializeField] private Animator animator;
        [SerializeField] private Rigidbody rb;

        [SerializeField] private Vector3 movement;
        private bool lookAtTarget = false;
        private bool canMove = true;

        public Transform FacingTransform => facingTransform;

        public bool CanMove
        {
            get
            {
                return canMove;
            }
            set
            {
                canMove = value;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            canMove = true;
        }

        // Update is called once per frame
        void Update()
        {
            if(canMove)
            {
                MoveRelativeToCamera();
                lookAtTarget = false;
            }
            if(lookAtTarget)
            {
                Quaternion toRotation = Quaternion.FromToRotation(facingTransform.forward, lookAtDirection);
                facingTransform.rotation = Quaternion.Lerp(facingTransform.rotation, toRotation, turnRate * Time.deltaTime);
            }
        }

        public void LookAt(Vector3 target)
        {
            target.y = facingTransform.position.y;
            lookAtDirection = target - facingTransform.position;
            lookAtTarget = true;
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
            this.transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);

            if(inputs.Movement != Vector2.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
                facingTransform.rotation = Quaternion.Lerp(facingTransform.rotation, toRotation, turnRate * Time.deltaTime);
            }

            animator.SetFloat("velocity", movement.magnitude);
        }
    }
}
