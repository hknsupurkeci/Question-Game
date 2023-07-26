using UnityEngine;

public class StartButtonController : MonoBehaviour
{
    public Transform movingObject; // Hareket ettirece�imiz GameObject
    public float hoverHeight = 0.5f; // Hover y�ksekli�i
    public float hoverSpeed = 5f; // Hareket h�z�

    private Vector3 originalPosition; // Gameobject'in orijinal konumu
    private bool isHovering = false; // Hover durumunu kontrol etmek i�in flag

    private void Start()
    {
        originalPosition = movingObject.position;
    }

    public void Enter()
    {
        isHovering = true;
    }

    public void Exit()
    {
        isHovering = false;
    }

    private void Update()
    {
        if (isHovering)
        {
            // Hover y�ksekli�ini ekleyerek yukar� do�ru hareket ettiriyoruz
            Vector3 targetPosition = originalPosition + Vector3.up * hoverHeight;
            movingObject.position = Vector3.Lerp(movingObject.position, targetPosition, hoverSpeed * Time.deltaTime);
        }
        else
        {
            // Orijinal konumuna geri iniyor
            movingObject.position = Vector3.Lerp(movingObject.position, originalPosition, hoverSpeed * Time.deltaTime);
        }
    }
}
