
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class WeaponSelectUI : MonoBehaviour
{
    [FormerlySerializedAs("weaponData")] public WeaponDataSO weaponDataSo;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => PlayerData.Instance.Loadout.SetLeftWeapon(weaponDataSo));
    }
}
