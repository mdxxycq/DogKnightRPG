using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseInfo", menuName = "CharacterData/AttackInfo")]
public class Attack_os : ScriptableObject
{
    public int attackForce;
    public int attackRange;
    public float  criticalStrikeRate;
    public float criticalStrike;
    public int attackInterval;
}
