using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private WeaponBase playerLeftGun;
    [SerializeField] private WeaponBase playerRightGun;

    [SerializeField] private GameObject leftAmmoContainer;
    [SerializeField] private GameObject rightAmmoContainer;
    [SerializeField] private GameObject ammoIconPrefab;

    private List<Image> leftAmmoImages = new List<Image>();
    private List<Image> rightAmmoImages = new List<Image>();

    private void OnEnable()
    {
        playerLeftGun.OnAmmoChanged += UpdateLeftAmmoCounter;
        playerRightGun.OnAmmoChanged += UpdateRightAmmoCounter;
    }

    private void OnDisable()
    {
        playerLeftGun.OnAmmoChanged -= UpdateLeftAmmoCounter;
        playerRightGun.OnAmmoChanged -= UpdateRightAmmoCounter;
    }

    private void Start()
    {
        CreateAmmoIcons(rightAmmoContainer,playerRightGun.CurrentAmmo, rightAmmoImages);
        CreateAmmoIcons(leftAmmoContainer, playerLeftGun.CurrentAmmo, leftAmmoImages);
    }
    private void UpdateLeftAmmoCounter(int ammoCount)
    {
        UpdateAmmoCounter(ammoCount, leftAmmoImages);
    }

    private void UpdateRightAmmoCounter(int ammoCount)
    {
        UpdateAmmoCounter(ammoCount, rightAmmoImages);
    }

    void UpdateAmmoCounter(int ammoCount, List<Image> ammoImages)
    {
        for (int i = 0; i < ammoImages.Count; i++)
        {
            ammoImages[i].enabled = i < ammoCount;
        }
    }

    private void CreateAmmoIcons(GameObject ammoContainer,int ammoCount, List<Image> ammoImages)
    {
        for (int i = 0; i < ammoCount; i++)
        {
            var newImage = Instantiate(ammoIconPrefab, ammoContainer.transform);
            ammoImages.Add(newImage.GetComponent<Image>());
        }
    }
}
