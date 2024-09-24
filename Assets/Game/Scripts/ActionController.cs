using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FishingGame
{
    public class ActionController : MonoBehaviour
    {
        [SerializeField] private CastFishingRodAction castingAction;
        [SerializeField] private FishingAction fishingAction;
        [SerializeField] private Tackle tackle;
        [SerializeField] private State currentState;
        [SerializeField] private PlayerController playerController;
        private BaseAction currentAction;

        private enum State
        {
            Casting, Fishing
        }

        // Start is called before the first frame update
        void Start()
        {
            ChangeAction(State.Casting);
            tackle.OnTouchWater += OnTackleTouchWater;
            tackle.OnTouchGround += OnTackleTouchGround;
            fishingAction.OnFishingEnd += OnFishingEnd;
            castingAction.OnBackCastingStarted += OnCastingStarted;
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void ChangeAction(State state)
        {
            currentState = state;

            currentAction?.End();

            switch (currentState)
            {
                case State.Casting:
                    currentAction = castingAction;
                    break;
                case State.Fishing:
                    currentAction = fishingAction;
                    break;
            }

            currentAction.Begin();
        }

        private void OnTackleTouchWater()
        {
            ChangeAction(State.Fishing);
        }

        private void OnTackleTouchGround()
        {
            playerController.CanMove = true;
            ChangeAction(State.Casting);
        }

        private void OnFishingEnd(Fish fish)
        {
            if(fish != null)
            {
                //show fish
                Debug.Log("You got " + fish.DisplayName);
            }

            playerController.CanMove = true;
            ChangeAction(State.Casting);
        }

        private void OnCastingStarted(bool started)
        {
            if(started)
            {
                playerController.CanMove = false;
            }
        }
    }
}
