using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TransparentEffect : MonoBehaviour
{
    [SerializeField] List<Image> images;

    public float speed = 1.0f; // Yan�p s�nme h�z�

    void Update()
    {
        // Mathf.PingPong, zamanla gidip gelen bir de�er d�nd�r�r, bu nedenle bu de�eri alpha de�eri olarak kullanabiliriz.
        float alpha = Mathf.PingPong(Time.time * speed, 1);

        foreach (Image image in images)
        {
            if (image != null) // Image'�n null olmad���ndan emin ol
            {
                Color imageColor = image.color; // Image'�n mevcut rengini al
                imageColor.a = alpha; // Alpha de�erini hesaplanan de�ere ayarlay�n
                image.color = imageColor; // Yeni rengi Image'a uygula
            }
        }
    }
}
