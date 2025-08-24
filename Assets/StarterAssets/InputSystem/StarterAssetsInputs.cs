using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{
    [RequireComponent(typeof(PlayerInput))]
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool sit;
        public bool standUp;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        private bool _isInputActive = true;
        private PlayerInput _playerInput;

        private void Awake()
        {
            _playerInput = GetComponent<PlayerInput>();
        }

        private void OnEnable()
        {
            // Bind actions ketika aktif
            var actions = _playerInput.actions;

            actions["Move"].performed += OnMove;
            actions["Move"].canceled += OnMove;

            actions["Look"].performed += OnLook;
            actions["Look"].canceled += OnLook;

            actions["Jump"].performed += OnJump;
            actions["Jump"].canceled += OnJump;

            actions["Sprint"].performed += OnSprint;
            actions["Sprint"].canceled += OnSprint;

            actions["Sit"].performed += OnSit;
            actions["Sit"].canceled += OnSit;

            actions["StandUp"].performed += OnStandUp;
            actions["StandUp"].canceled += OnStandUp;
        }

        private void OnDisable()
        {
            // Unbind untuk mencegah memory leak
            var actions = _playerInput.actions;

            actions["Move"].performed -= OnMove;
            actions["Move"].canceled -= OnMove;

            actions["Look"].performed -= OnLook;
            actions["Look"].canceled -= OnLook;

            actions["Jump"].performed -= OnJump;
            actions["Jump"].canceled -= OnJump;

            actions["Sprint"].performed -= OnSprint;
            actions["Sprint"].canceled -= OnSprint;

            actions["Sit"].performed -= OnSit;
            actions["Sit"].canceled -= OnSit;

            actions["StandUp"].performed -= OnStandUp;
            actions["StandUp"].canceled -= OnStandUp;
        }

        /// <summary>
        /// Aktifkan/Nonaktifkan input player (misal saat dialog).
        /// </summary>
        public void SetInputActive(bool state)
        {
            _isInputActive = state;
            SetCursorState(state);
        }

        // ---------------- ACTION CALLBACKS ---------------- //

        private void OnMove(InputAction.CallbackContext context)
        {
            if (_isInputActive)
                MoveInput(context.ReadValue<Vector2>());
            else
                MoveInput(Vector2.zero);
        }

        private void OnLook(InputAction.CallbackContext context)
        {
            if (_isInputActive && cursorInputForLook)
                LookInput(context.ReadValue<Vector2>());
            else
                LookInput(Vector2.zero);
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            JumpInput(_isInputActive && context.performed);
        }

        private void OnSprint(InputAction.CallbackContext context)
        {
            SprintInput(_isInputActive && context.performed);
        }

        private void OnSit(InputAction.CallbackContext context)
        {
            SitInput(_isInputActive && context.performed);
        }

        private void OnStandUp(InputAction.CallbackContext context)
        {
            StandUpInput(_isInputActive && context.performed);
        }

        // ---------------- SETTERS ---------------- //

        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        public void SitInput(bool newSitState)
        {
            sit = newSitState;
        }

        public void StandUpInput(bool newStandUpState)
        {
            standUp = newStandUpState;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }
    }
}
