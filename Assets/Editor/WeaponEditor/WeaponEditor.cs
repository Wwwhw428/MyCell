using UnityEditor;
using UnityEngine;
using MyCell.Weapon;

namespace MyCell.Editor.WeaponEditor
{
    [CustomEditor(typeof(Weapon.Weapon))]
    public class WeaponEditor : UnityEditor.Editor
    {
        private Weapon.Weapon _weapon;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            _weapon = target as Weapon.Weapon;

            if (_weapon == null || _weapon.WeaponData == null) return;

            if (GUILayout.Button("Set Up Weapon"))
            {
                _weapon.GenerateWeapon();
            }
        }
    }
}