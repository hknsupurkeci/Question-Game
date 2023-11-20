using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TransparentEffect : MonoBehaviour
{
    [SerializeField] List<Image> images;

    public float speed = 1.0f; // Yanýp sönme hýzý

    void Update()
    {
        // Mathf.PingPong, zamanla gidip gelen bir deðer döndürür, bu nedenle bu deðeri alpha deðeri olarak kullanabiliriz.
        float alpha = Mathf.PingPong(Time.time * speed, 1);

        foreach (Image image in images)
        {
            if (image != null) // Image'ýn null olmadýðýndan emin ol
            {
                Color imageColor = image.color; // Image'ýn mevcut rengini al
                imageColor.a = alpha; // Alpha deðerini hesaplanan deðere ayarlayýn
                image.color = imageColor; // Yeni rengi Image'a uygula
            }
        }
    }
}
