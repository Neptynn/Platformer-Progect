using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAggressiveWeaponData", menuName = "Data/Weapon Data/AggressiveWeapon")]
public class SO_AggressiveWeaponData : SO_WeaponData
{
    [SerializeField] private WeaponAttackDetails[] attackDetails;
    
    public WeaponAttackDetails[] AttackDetails { get => attackDetails; private set => attackDetails = value; }

    private void OnEnable()
    {
        amountsOfAttacks = attackDetails.Length;

        movementSpeed = new float[amountsOfAttacks];

        for (int i = 0; i < amountsOfAttacks; i++)
        {
            movementSpeed[i] = attackDetails[i].movementSpeed;
        }
    }

}
