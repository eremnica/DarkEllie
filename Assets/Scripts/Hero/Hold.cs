using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Hold : MonoBehaviour
{
    public Transform holdPoint;
    public float pickupRange = 2f;
    private GameObject heldItem;
    // public TextMeshProUGUI pickupMessage; // Ссылка на UI элемент для вывода сообщения

    public bool IsHoldingItem { get; private set; } // Флаг для проверки, держит ли игрок предмет

    private Transform nearestItem; // Ближайший предмет

    private void Start()
    {
        /* if (pickupMessage != null)
        {
            pickupMessage.gameObject.SetActive(false); // Скрываем сообщение по умолчанию
            pickupMessage.enableAutoSizing = true;
            pickupMessage.fontSizeMin = 10; // Минимальный размер шрифта
            pickupMessage.fontSizeMax = 50; // Максимальный размер шрифта
        }
        else
        {
            Debug.LogError("Pickup Message is not assigned in the inspector!");
        } */
    }

    void Update()
    {
        // Проверяем, есть ли рядом предмет
        ShowPickupMessage();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (heldItem == null)
            {
                TryPickupItem();
            }
            else
            {
                DropItem();
            }
        }

        // Если предмет поднят, перемещаем его к точке удержания
        if (heldItem != null)
        {
            heldItem.transform.position = holdPoint.position;
        }
    }

    void ShowPickupMessage()
    {
        // Поиск всех объектов с коллайдером в зоне действия
        Collider2D[] itemsInRange = Physics2D.OverlapCircleAll(transform.position, pickupRange);

        bool itemNearby = false;
        nearestItem = null;

        foreach (var item in itemsInRange)
        {
            // Проверяем, есть ли у объекта тег "item"
            if (item.CompareTag("item"))
            {
                itemNearby = true;
                nearestItem = item.transform;
                break;
            }
        }

        // Если предмет рядом, показываем сообщение, иначе скрываем его
        /* if (itemNearby)
        {
            if (pickupMessage != null)
            {
                pickupMessage.gameObject.SetActive(true); // Показываем сообщение
                UpdatePickupMessagePosition(); // Обновляем позицию сообщения
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                pickupMessage.gameObject.SetActive(false);
            }
        }
        else
        {
            if (pickupMessage != null)
            {
                pickupMessage.gameObject.SetActive(false); // Скрываем сообщение
            }
        } */
    }

    void TryPickupItem()
    {
        // Поиск всех объектов с коллайдером в зоне действия
        Collider2D[] itemsInRange = Physics2D.OverlapCircleAll(transform.position, pickupRange);

        foreach (var item in itemsInRange)
        {
            // Проверяем, есть ли у объекта тег "item"
            if (item.CompareTag("item"))
            {
                heldItem = item.gameObject;

                // Отключаем физику, чтобы предмет не падал
                heldItem.GetComponent<Rigidbody2D>().isKinematic = true;
                heldItem.GetComponent<Collider2D>().enabled = false;

                // Устанавливаем флаг, что предмет поднят
                IsHoldingItem = true;

                // Перемещаем предмет в точку удержания
                heldItem.transform.position = holdPoint.position;

                // pickupMessage.gameObject.SetActive(false); // Скрываем сообщение после поднятия предмета
                break;
            }
        }
    }

    void DropItem()
    {
        if (heldItem != null)
        {
            // Включаем обратно физику
            heldItem.GetComponent<Rigidbody2D>().isKinematic = false;
            heldItem.GetComponent<Collider2D>().enabled = true;

            // Сбрасываем флаг
            IsHoldingItem = false;

            // Освобождаем ссылку на предмет
            heldItem = null;
        }
    }

    private void UpdatePickupMessagePosition()
    {
        /* if (pickupMessage != null && nearestItem != null)
        {
            // Convert the nearest item's world position to a screen position
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(nearestItem.position);

            // Offset the text to appear above the item's position
            screenPosition.y += 150; // Подстройте это значение для корректного отображения над предметом

            // Apply the updated position to the TextMeshPro element
            pickupMessage.transform.position = screenPosition;
        } */
    }
}

