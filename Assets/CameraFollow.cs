using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Ссылка на трансформ игрока
    private float initialY;    // Начальная высота камеры относительно игрока

    private void Start()
    {
        // Запоминаем начальную высоту камеры относительно игрока
        initialY = transform.position.y - target.position.y;
    }

    private void LateUpdate()
    {
        // Камера следует за игроком по X и сохраняет начальную высоту по Y
        transform.position = new Vector3(target.position.x, target.position.y + initialY, transform.position.z);
    }
}
