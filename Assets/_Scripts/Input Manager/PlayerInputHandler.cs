using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    PlayerInput input;
    InputActionAsset inputActionMap;
    public Vector2 MovementInput;
    public int InputX;
    public int InputY;
    public bool JumpInput;
    public bool[] AttackInput;
    public event Action<bool> OnInteract;
    public bool[] AttackInputsHold { get; private set; }

    private void Awake()
    {
        inputActionMap = GetComponent<PlayerInput>().actions;
        input = GetComponent<PlayerInput>();
        int count = Enum.GetValues(typeof(CombatInput)).Length;
        AttackInput = new bool[count];
        AttackInputsHold = new bool[count];
    }

    private void Start()
    {
        inputActionMap.Enable();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
        InputX = (int)(MovementInput * Vector2.right).normalized.x;
        InputY = (int)(MovementInput * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
            JumpInput = true;
    }

    public void OnPrimaryWeaponInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInput[(int)CombatInput.primary] = true;
            AttackInputsHold[(int)CombatInput.primary] = true;
        }
        if (context.canceled)
        {
            AttackInput[(int)CombatInput.primary] = false;
            AttackInputsHold[(int)CombatInput.primary] = false;
        }
    }

    public void OnSeconderyWeaponInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            AttackInput[(int)CombatInput.secondary] = true;
            AttackInputsHold[(int)CombatInput.secondary] = true;
        }
        if (context.canceled)
        {
            AttackInput[(int)CombatInput.secondary] = false;
            AttackInputsHold[(int)CombatInput.secondary] = false;
        }
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnInteract?.Invoke(true);
        }

        if (context.canceled)
        {
            OnInteract?.Invoke(false);
        }
    }

    public void UseJumpInput() => JumpInput = false;

}
public enum CombatInput
{
    primary,
    secondary
}