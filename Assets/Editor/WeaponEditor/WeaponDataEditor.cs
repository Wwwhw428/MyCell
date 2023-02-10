using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;
using MyCell.SO.Weapon;
using MyCell.Weapon.Component.Data;

[CustomEditor(typeof(WeaponDataSo))]
public class WeaponDataEditor : UnityEditor.Editor
{
    public List<Type> dataCompTypes = new List<Type>();

    public override void OnInspectorGUI()
    {
        WeaponDataSo data = target as WeaponDataSo; ;
        foreach (Type T in dataCompTypes)
        {
            if (GUILayout.Button($"Add {T.Name}"))
            {
                var dataType = Activator.CreateInstance(T);
                if (dataType.GetType().IsSubclassOf(typeof(WeaponComponentData)))
                {
                    data?.AddComponentData(dataType as WeaponComponentData);
                }

                EditorUtility.SetDirty(this);
            }
        }

        DrawDefaultInspector();
    }

    private void OnEnable()
    {
        var dataTypes = GetAllWeaponAttackDataComponents();
        dataCompTypes.Clear();

        dataTypes.ToList().ForEach(item =>
        {
            dataCompTypes.Add(item.GetType());
        });
    }

    private IEnumerable<WeaponComponentData> GetAllWeaponAttackDataComponents()
    {
        return AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(typeof(WeaponComponentData)) && type.IsClass && !type.IsAbstract && !type.ContainsGenericParameters)
            .Select(type => Activator.CreateInstance(type) as WeaponComponentData);
    }
}
