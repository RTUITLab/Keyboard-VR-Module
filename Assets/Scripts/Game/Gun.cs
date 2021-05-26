using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class Gun : MonoBehaviour
{
    [Header("Управление")]
    [SerializeField] private SteamVR_Action_Boolean fireAction;
    private Interactable interactable;

    [Header("Параметры")] // Стандартные значения сделаны для среднестатистического пистолета
    [SerializeField] private int magazineSize = 16; // Максимальное количество патронов в магазине
    private int currentMagazineSize; // Текущее количество патронов в магазине
    [SerializeField] private int fireRate = 50; // В случае с полуавтоматическим или автоматическим режимом
    [SerializeField] private int bulletSpeed = 5;
    [SerializeField] private FiringMode firingMode = FiringMode.Single;

    [Header("Утилиты")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource fireSound;
    private bool canFire = true;

    private void Start()
    {
        interactable = GetComponent<Interactable>();
        currentMagazineSize = magazineSize;
    }

    private void Update()
    {
        if (interactable.attachedToHand != null)
        {
            SteamVR_Input_Sources source = interactable.attachedToHand.handType;

            if ((firingMode == FiringMode.Single && fireAction[source].stateDown) ||
                (firingMode == FiringMode.Auto && fireAction[source].state))
            {
                if (canFire)
                {
                    StartCoroutine(TryFire());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator TryFire()
    {
        canFire = false;

        if (currentMagazineSize > 0)
        {
            currentMagazineSize--;
            Fire();
        }
        else
        {
            // Нет патронов
            // TODO: Тикающий звук
        }

        yield return new WaitForSeconds(1f / fireRate);
        canFire = true;
    }

    private void Fire()
    {
        var bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletSpeed;
        // TODO: Отдельный скрипт с пулей, содержит информацию об уроне и задает стартовый велосити

        fireSound.Play();
        animator.Play("HandgunFire"); // TODO: Сделать универсальное решение
                                      // TODO: Отключение возможности дотронуться до слайдера
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(2f); // TODO: Перезарядка после выполнения нужных действий

        currentMagazineSize = magazineSize;
    }
}

public enum FiringMode
{
    Single,
    Auto
}
