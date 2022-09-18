using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    public Character_os BaseInfo;
    public Attack_os attackInfo;
    [HideInInspector]
    public bool isCriticalStrike = false;
    public int MaxHealth
    {
        get
        {
            if (BaseInfo != null)
            {
                return BaseInfo.maxHealth;
            }
            return 0;
        }
        set
        {
            MaxHealth = value;
        }
    }

    public int CurrentHealth
    {
        get
        {
            if (BaseInfo != null)
            {
                return BaseInfo.currentHealth;
            }
            return 0;
        }
        set
        {
            CurrentHealth = value;
        }
    }

    public int AttackForce
    {
        get
        {
            if (attackInfo != null)
            {
                return attackInfo.attackForce;
            }
            return 0;
        }
        set
        {
            attackInfo.attackForce = value;
        }
    }
    public int AttackRange
    {
        get
        {
            if (attackInfo != null)
            {
                return attackInfo.attackRange;
            }
            return 0;
        }
        set
        {
            attackInfo.attackRange = value;
        }
    }
    public float CriticalStrikeRate
    {
        get
        {
            if (attackInfo != null)
            {
                return attackInfo.criticalStrikeRate;
            }
            return 0;
        }
        set
        {
            attackInfo.criticalStrikeRate = value;
        }
    }
    public float CriticalStrike
    {
        get
        {
            if (attackInfo != null)
            {
                return attackInfo.criticalStrike;
            }
            return 0;
        }
        set
        {
            attackInfo.criticalStrike = value;
        }
    }
    public int AttackInterval
    {
        get
        {
            if (attackInfo != null)
            {
                return attackInfo.attackInterval;
            }
            return 0;
        }
        set
        {
            attackInfo.attackInterval = value;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
