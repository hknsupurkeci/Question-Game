using UnityEngine;
using UnityEngine.EventSystems; // EventSystems kütüphanesini eklemeyi unutmayın

[RequireComponent(typeof(AudioSource))] // AudioSource bileşeninin bu GameObject'te olmasını zorunlu kılıyoruz
public class PanelClickSound : MonoBehaviour, IPointerClickHandler // IPointerClickHandler arayüzünü implement ediyoruz
{
    private AudioSource audioSource;
    public GameObject electricPref;
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource bileşenini alıyoruz
    }

    // IPointerClickHandler arayüzünden gelen bu metod, panel tıklandığında tetiklenir
    public void OnPointerClick(PointerEventData eventData)
    {
        Instantiate(electricPref, Vector3.zero, Quaternion.identity);
        audioSource.Play(); // Ses dosyasını çal
    }
}
