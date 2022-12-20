using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using MyCell.Weapon;
using System.Collections.Generic;
using System;
using System.Linq;

public class WeaponEditor : EditorWindow
{
    // // Weapon Data
    // private WeaponDataList_SO _dataBase;
    // private List<WeaponDetails> _weaponList = new List<WeaponDetails>();
    // private WeaponDetails _activeWeaponItem;
    // private WeaponAttackDetails _activeAttackItem;
    // private List<WeaponAttackDetails> _attackList = new List<WeaponAttackDetails>();
    // [SerializeField]
    // private List<Sprite> _activeAttackSprites;
    // private Sprite _activeSprite;
    //
    // // UI Item
    // private ListView _weaponItemListView;
    // private ScrollView _weaponDetailsSection;
    // private ListView _weaponAttackListView;
    // private ScrollView _attackDetailsSection;
    // private ListView _attackSpritesListView;
    // //private PropertyField _attackSpritesPropField;
    //
    // // Template
    // private VisualTreeAsset _weaponItemRowTemplate;
    // private VisualTreeAsset _weaponAttackRowTemplate;
    // private VisualTreeAsset _weaponSpriteRowTemplate;
    //
    // // Default View
    // private Sprite _defaultIcon;
    //
    // // test
    // //[SerializeField]
    // //Sprite[] WeaponSprites;
    // //[SerializeField]
    // //List<Sprite> test = new List<Sprite>();
    //
    // [MenuItem("MyCell/WeaponEditor")]
    // public static void ShowExample()
    // {
    //     WeaponEditor wnd = GetWindow<WeaponEditor>();
    //     wnd.titleContent = new GUIContent("WeaponEditor");
    // }
    //
    // public void CreateGUI()
    // {
    //     // Each editor window contains a root VisualElement object
    //     VisualElement root = rootVisualElement;
    //
    //     // Import UXML
    //     var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Weapon_UI_Builder/WeaponEditor.uxml");
    //     VisualElement labelFromUXML = visualTree.Instantiate();
    //     root.Add(labelFromUXML);
    //
    //     // Get Default Icon
    //     _defaultIcon = AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprite/MyIcon.png");
    //
    //     // Get TemplateData
    //     _weaponItemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Weapon_UI_Builder/WeaponItemRowTemplate.uxml");
    //     _weaponAttackRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Weapon_UI_Builder/WeaponAttackRowTemplate.uxml");
    //     _weaponSpriteRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/Weapon_UI_Builder/WeaponSpriteRowTemplate.uxml");
    //
    //     // variable assignment
    //     _weaponItemListView = root.Q<VisualElement>("WeaponList").Q<ListView>("ListView");
    //     _weaponDetailsSection = root.Q<ScrollView>("WeaponDetails");
    //     _weaponAttackListView = _weaponDetailsSection.Q<VisualElement>("AttackData").Q<ListView>("ListView");
    //     _attackDetailsSection = _weaponDetailsSection.Q<VisualElement>("AttackData").Q<ScrollView>("AttackDetails");
    //     _attackSpritesListView = _attackDetailsSection.Q<ListView>("SpriteList");
    //     //_attackSpritesPropField = _attackDetailsSection.Q<PropertyField>("Sprites");
    //
    //     // Subscrib Button Event
    //     root.Q<Button>("AddWeaponButton").clicked += OnAddWeaponClicked;
    //     root.Q<Button>("DeleteWeaponButton").clicked += OnDeleteWeaponClicked;
    //     root.Q<Button>("AddAttackButton").clicked += OnAddAttakClicked;
    //     root.Q<Button>("DeleteAttackButton").clicked += OnDeleteAttackClicked;
    //     root.Q<Button>("AddSpriteButton").clicked += OnAddAttackSpriteClicked;
    //     root.Q<Button>("DeleteSpriteButton").clicked += OnDeleteAttackSpriteClicked;
    //
    //     // Load Data
    //     LoadDataBase();
    //
    //     // Generate List View
    //     GenerateWeaponItemListView();
    // }
    //
    // #region Button Event
    // private void OnAddWeaponClicked()
    // {
    //     WeaponDetails newWeaponDetail = new WeaponDetails();
    //     newWeaponDetail.WeaponName = "New Weapon";
    //     newWeaponDetail.WeaponID = 1000 + _weaponList.Count + 1;
    //     _weaponList.Add(newWeaponDetail);
    //     _weaponItemListView.Rebuild();
    // }
    //
    // private void OnDeleteWeaponClicked()
    // {
    //     _weaponList.Remove(_activeWeaponItem);
    //     _weaponItemListView.Rebuild();
    //     _weaponDetailsSection.visible = false;
    // }
    //
    // private void OnAddAttakClicked()
    // {
    //     WeaponAttackDetails newAttackDetails = new WeaponAttackDetails();
    //     newAttackDetails.AttackName = $"Attack {_activeWeaponItem.WeaponAttackDetials.Count}";
    //     _activeWeaponItem.WeaponAttackDetials.Add(newAttackDetails);
    //     _weaponAttackListView.Rebuild();
    // }
    //
    // private void OnDeleteAttackClicked()
    // {
    //     _activeWeaponItem.WeaponAttackDetials.Remove(_activeAttackItem);
    //     _weaponAttackListView.Rebuild();
    //     _attackDetailsSection.visible = false;
    // }
    //
    // private void OnAddAttackSpriteClicked()
    // {
    //     Sprite newSprite = Sprite.Create(null, Rect.zero, Vector2.zero);
    //     _activeAttackSprites.Add(newSprite);
    //     _attackSpritesListView.Rebuild();
    // }
    //
    // private void OnDeleteAttackSpriteClicked()
    // {
    //     _activeAttackSprites.Remove(_activeSprite);
    //     _attackSpritesListView.Rebuild();
    // }
    //
    // #endregion
    //
    // private void LoadDataBase()
    // {
    //     var dataArray = AssetDatabase.FindAssets("WeaponDataList_SO");
    //
    //     if (dataArray.Length > 1)
    //     {
    //         var path = AssetDatabase.GUIDToAssetPath(dataArray[0]);
    //         _dataBase = AssetDatabase.LoadAssetAtPath(path, typeof(WeaponDataList_SO)) as WeaponDataList_SO;
    //     }
    //
    //     _weaponList = _dataBase.WeaponDetails;
    //
    //     // �����������޷���������
    //     EditorUtility.SetDirty(_dataBase);
    // }
    //
    // #region WeaponItemListView & ItemDetailSection
    // private void GenerateWeaponItemListView()
    // {
    //     Func<VisualElement> makeItem = () => _weaponItemRowTemplate.CloneTree();
    //
    //     Action<VisualElement, int> bindItem = (e, i) =>
    //     {
    //         if (i < _weaponList.Count)
    //         {
    //             if (_weaponList[i].WeaponIcon != null)
    //                 e.Q<VisualElement>("Icon").style.backgroundImage = _weaponList[i].WeaponIcon.texture;
    //
    //             e.Q<Label>("Name").text = _weaponList[i] == null ? "NO ITEM" : _weaponList[i].WeaponName;
    //         }
    //     };
    //
    //     _weaponItemListView.fixedItemHeight = 60;
    //     _weaponItemListView.itemsSource = _weaponList;
    //     _weaponItemListView.makeItem = makeItem;
    //     _weaponItemListView.bindItem = bindItem;
    //
    //     // Callback invoked when the user changes the selection inside the ListView
    //     _weaponItemListView.onSelectionChange += OnWeaponItemListSelectionChange;
    //     _weaponDetailsSection.visible = false;
    // }
    //
    // private void GetItemDetails()
    // {
    //     _weaponDetailsSection.MarkDirtyRepaint();
    //
    //     // icon
    //     _weaponDetailsSection.Q<VisualElement>("Icon").style.backgroundImage = _activeWeaponItem.WeaponIcon == null ? _defaultIcon.texture : _activeWeaponItem.WeaponIcon.texture;
    //
    //     // ID
    //     _weaponDetailsSection.Q<IntegerField>("ItemID").value = _activeWeaponItem.WeaponID;
    //     _weaponDetailsSection.Q<IntegerField>("ItemID").RegisterValueChangedCallback(evt =>
    //     {
    //         _activeWeaponItem.WeaponID = evt.newValue;
    //     });
    //
    //     // Name
    //     _weaponDetailsSection.Q<TextField>("ItemName").value = _activeWeaponItem.WeaponName;
    //     _weaponDetailsSection.Q<TextField>("ItemName").RegisterValueChangedCallback(evt =>
    //     {
    //         _activeWeaponItem.WeaponName = evt.newValue;
    //     });
    //
    //     // Icon
    //     _weaponDetailsSection.Q<ObjectField>("ItemIcon").value = _activeWeaponItem.WeaponIcon;
    //     _weaponDetailsSection.Q<ObjectField>("ItemIcon").RegisterValueChangedCallback(evt =>
    //     {
    //         Sprite newIcon = evt.newValue as Sprite;
    //         _activeWeaponItem.WeaponIcon = newIcon;
    //         _weaponItemListView.Rebuild();
    //         _weaponDetailsSection.Q<VisualElement>("Icon").style.backgroundImage = newIcon == null ? _defaultIcon.texture : newIcon.texture;
    //     });
    //
    //     // WorldSprite
    //     _weaponDetailsSection.Q<ObjectField>("ItemWorldSprite").value = _activeWeaponItem.WeaponWorldSprite;
    //     _weaponDetailsSection.Q<ObjectField>("ItemWorldSprite").RegisterValueChangedCallback(evt =>
    //     {
    //         _activeWeaponItem.WeaponWorldSprite = (Sprite)evt.newValue;
    //     });
    //
    //     // Description
    //     _weaponDetailsSection.Q<TextField>("Description").value = _activeWeaponItem.WeaponDescription;
    //     _weaponDetailsSection.Q<TextField>("Description").RegisterValueChangedCallback(evt =>
    //     {
    //         _activeWeaponItem.WeaponDescription = evt.newValue;
    //     });
    //
    //     GenerateWeaponAttackListView();
    // }
    //
    // private void OnWeaponItemListSelectionChange(IEnumerable<object> selectedItem)
    // {
    //     _activeWeaponItem = (WeaponDetails)selectedItem.First();
    //     _attackList = _activeWeaponItem.WeaponAttackDetials;
    //     GetItemDetails();
    //     _weaponDetailsSection.visible = true;
    // }
    //
    // #endregion
    //
    // #region WeaponAttackListView & AttackDetailSection
    // private void GenerateWeaponAttackListView()
    // {
    //     Func<VisualElement> makeItem = () => _weaponAttackRowTemplate.CloneTree();
    //
    //     Action<VisualElement, int> bindItem = (e, i) =>
    //     {
    //         if (i < _attackList.Count)
    //         {
    //             e.Q<Label>("Label").text = _attackList[i].AttackName;
    //         }
    //     };
    //
    //     _weaponAttackListView.fixedItemHeight = 60;
    //     _weaponAttackListView.itemsSource = _attackList;
    //     _weaponAttackListView.makeItem = makeItem;
    //     _weaponAttackListView.bindItem = bindItem;
    //
    //     // Callback invoked when the user changes the selection inside the ListView
    //     _weaponAttackListView.onSelectionChange += OnWeaponAttackListSelectionChange;
    //     _attackDetailsSection.visible = false;
    // }
    //
    // private void OnWeaponAttackListSelectionChange(IEnumerable<object> selectedItem)
    // {
    //     _activeAttackItem = (WeaponAttackDetails)selectedItem.First();
    //     _activeAttackSprites = _activeAttackItem.Sprites;
    //     GetAttackDetails();
    //     _attackDetailsSection.visible = true;
    // }
    //
    // private void GetAttackDetails()
    // {
    //     _attackDetailsSection.MarkDirtyRepaint();
    //
    //     //Name
    //     _attackDetailsSection.Q<TextField>("NameOfAttack").value = _activeAttackItem.AttackName;
    //     _attackDetailsSection.Q<TextField>("NameOfAttack").RegisterValueChangedCallback(evt =>
    //     {
    //         _activeAttackItem.AttackName = evt.newValue;
    //     });
    //
    //     // DamageOfAttack
    //     _attackDetailsSection.Q<IntegerField>("DamageOfAttack").value = _activeAttackItem.DamageOfAttack;
    //     _attackDetailsSection.Q<IntegerField>("DamageOfAttack").RegisterValueChangedCallback(evt =>
    //     {
    //         _activeAttackItem.DamageOfAttack = evt.newValue;
    //     });
    //
    //     // CoolDown
    //     _attackDetailsSection.Q<IntegerField>("CoolDown").value = _activeAttackItem.CoolDownOfAttack;
    //     _attackDetailsSection.Q<IntegerField>("CoolDown").RegisterValueChangedCallback(evt =>
    //     {
    //         _activeAttackItem.CoolDownOfAttack = evt.newValue;
    //     });
    //
    //     // HitBox
    //     _attackDetailsSection.Q<Vector4Field>("HitBox").value = _activeAttackItem.HitBox;
    //     _attackDetailsSection.Q<Vector4Field>("HitBox").RegisterValueChangedCallback(evt =>
    //     {
    //         _activeAttackItem.HitBox = evt.newValue;
    //     });
    //     
    //     GenerateAttackSpriteListView();
    //
    // }
    //
    // private void GenerateAttackSpriteListView()
    // {
    //     //_attackSpritesPropField.MarkDirtyRepaint();
    //
    //     //SerializedObject serializedObj = new(this);
    //     //SerializedProperty prop = serializedObj.FindProperty("_activeAttackSprites");
    //     //_attackSpritesPropField.BindProperty(prop);
    //     //_attackSpritesPropField.Bind(serializedObj);
    //
    //     _attackSpritesListView.MarkDirtyRepaint();
    //
    //     Func<VisualElement> makeItem = () => _weaponSpriteRowTemplate.CloneTree();
    //             
    //     Action<VisualElement, int> bindItem = (e, i) =>
    //     {
    //         if (i < _activeAttackSprites.Count)
    //             e.Q<ObjectField>("Sprite").value = _activeAttackSprites[i];
    //     };
    //
    //     //_weaponAttackListView.fixedItemHeight = 60;
    //     _attackSpritesListView.itemsSource = _activeAttackSprites;
    //     _attackSpritesListView.makeItem = makeItem;
    //     _attackSpritesListView.bindItem = bindItem;
    //     _attackSpritesListView.selectionType = SelectionType.Multiple;
    //
    //     _attackSpritesListView.onSelectionChange += OnSpriteItemListSelectionChange;
    // }
    //
    // public void OnSpriteItemListSelectionChange(IEnumerable<object> selectedItem)
    // {
    //     _activeSprite = (Sprite)selectedItem.First();
    // }
    //
    // #endregion
    //
    // // TODO: attack sprite �������ɾ����������sprite��������д������
}