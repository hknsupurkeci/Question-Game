using UnityEngine;

public class StartButtonController : MonoBehaviour
{
    public Transform movingObject; // Hareket ettireceðimiz GameObject
    public float hoverHeight = 0.5f; // Hover yüksekliði
    public float hoverSpeed = 5f; // Hareket hýzý

    private Vector3 originalPosition; // Gameobject'in orijinal konumu
    private bool isHovering = false; // Hover durumunu kontrol etmek için flag

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
            // Hover yüksekliðini ekleyerek yukarý doðru hareket ettiriyoruz
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
