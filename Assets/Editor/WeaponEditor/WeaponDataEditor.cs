using System;
using System.Collections.Generic;
using System.Linq;
using MyCell.ScriptableObjects.SO.Weapon;
using MyCell.Weapon.Components;
using MyCell.Weapon.Components.ComponentData;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

[CustomEditor(typeof(WeaponDataSo))]
public class WeaponDataEditor : UnityEditor.Editor
{
    public List<Type> dataCompTypes = new List<Type>();

    public override void OnInspectorGUI()
    {
        WeaponDataSo data = target as WeaponDataSo;;
        foreach (Type T in dataCompTypes)
        {
            if (GUILayout.Button($"Add {T.Name}"))
            {
                var dataType = Activator.CreateInstance(T);
                if (dataType.GetType().IsSubclassOf(typeof(WeaponAttackDataComponent)))
                {
                    data?.AddWeaponComponentData(dataType as WeaponAttackDataComponent);
                }
                
                EditorUtility.SetDirty(this);
            }
        }
        

        DrawDefaultInspector();
    }

    private void OnEnable()
    {
        var dataTypes =  GetAllWeaponAttackDataComponents();
        dataCompTypes.Clear();
        
        dataTypes.ToList().ForEach(item =>
        {
            dataCompTypes.Add(item.GetType());
        });
    }

    private IEnumerable<WeaponAttackDataComponent> GetAllWeaponAttackDataComponents()
    {
        return AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(typeof(WeaponAttackDataComponent)) && type.IsClass && !type.IsAbstract && !type.ContainsGenericParameters)
            .Select(type => Activator.CreateInstance(type) as WeaponAttackDataComponent);
    }
}
