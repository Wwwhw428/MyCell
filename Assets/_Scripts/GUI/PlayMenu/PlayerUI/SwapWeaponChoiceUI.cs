using System;
using UnityEngine;
using UnityEngine.UI;

namespace MyCell.GUI
{
    public class SwapWeaponChoiceUI : MonoBehaviour
    {
        [SerializeField] private CombatInput input;

        private Button button;

        public event Action<CombatInput> OnChoiceSelected;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable() => button.onClick.AddListener(HandleButtonClick);

        private void OnDisable() => button.onClick.RemoveListener(HandleButtonClick);

        private void HandleButtonClick()
        {
            OnChoiceSelected?.Invoke(input);
        }
    }
}