using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private GameObject heldObject;
    //private bool isHoldingObject = false;
    private float objectHeightOffset = 1.5f; // Поправка высоты объекта при взятии
    //private float pickupRadius = 0.01f; // Радиус подбора объектов
    private float pickupRadius = 0.1f;
    public bool isHoldingObject { get; private set; } // Флаг захвата объекта


    /*void Update()
    {
        if (isHoldingObject && heldObject != null) // Проверяем, если удерживаем объект
        {
            // Держим объект перед персонажем
            heldObject.transform.position = transform.position + transform.forward * 0.7f + transform.up * objectHeightOffset;
        }
    }*/

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isHoldingObject)
            {
                ReleaseObject();
            }
            else
            {
                PickObjectFromFloor();
            }
        }

        if (isHoldingObject && heldObject != null) // Добавляем проверку на null
        {
            // Обновляем позицию объекта, добавляя поправку высоты
            heldObject.transform.position = transform.position + transform.forward * 2f + transform.up * objectHeightOffset;
        }
    }

    public void PickObjectFromFloor()
    {
        RaycastHit hit;

        // Проводим сферический луч, чтобы обнаружить объекты на полу вокруг игрока
        if (Physics.SphereCast(transform.position, pickupRadius, transform.forward, out hit, 2f))
        {
            if (hit.collider.CompareTag("Ball"))
            {
                // Если найден объект, подбираем его
                heldObject = hit.collider.gameObject;
                heldObject.transform.parent = transform;
                isHoldingObject = true;
            }
        }
    }

    public void ReleaseObject()
    {
        if (heldObject != null) // Добавляем проверку на null
        {
            // Устанавливаем объекту родителя null, чтобы он перестал быть дочерним объектом игрока
            heldObject.transform.parent = null;

            // Сбрасываем объект
            Rigidbody rb = heldObject.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            heldObject = null;
            isHoldingObject = false;
        }
    }
}
