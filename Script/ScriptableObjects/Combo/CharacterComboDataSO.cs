using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterComboDataSO", menuName = "ScriptableObjects/CharacterComboDataSO")]
public class CharacterComboDataSO : ScriptableObject
{
    [SerializeField]private string _actionName;
    [SerializeField]private List<ComboDamageInfo> _comboDamageInfos;
    [SerializeField]private float _actionColdTime;
    [SerializeField]private CharacterComboDataSO _nextCombo;
    [SerializeField]private CharacterComboDataSO _childCombo;
    [SerializeField]private float _animationOffset;
    [SerializeField]private float _matchPositionOffset;
    public bool _hasChildCombo=>_childCombo!=null;
    public string ActionName
    {
        get
        {
            return _actionName;
        }
    }
    public List<ComboDamageInfo> DamageInfos
    {
        get
        {
            return _comboDamageInfos;
        }
    }
    public float ActionColdTime
    {
        get
        {
            return _actionColdTime;
        }
    }
    public CharacterComboDataSO NextComboAction
    {
        get
        {
            return _nextCombo;
        }
    }
    public CharacterComboDataSO ChildComboAction
    {
        get
        {
            return _childCombo;
        }
    }
    public float AnimationOffset
    {
        get{return _animationOffset;}
    }

    public float MatchPositionOffset
    {
        get{return _matchPositionOffset;}
    }
}

public enum DamageType
{
    WEAPON,
    PUNCH
}

[System.Serializable]
public class ComboDamageInfo
{
    public DamageType DamageType;
    public float Damage;
    public string HitName;
    public string ParryName;
}