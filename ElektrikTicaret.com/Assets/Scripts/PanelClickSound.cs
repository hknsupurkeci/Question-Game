using UnityEngine;
using UnityEngine.EventSystems; // EventSystems kütüphanesini eklemeyi unutmayýn

[RequireComponent(typeof(AudioSource))] // AudioSource bileþeninin bu GameObject'te olmasýný zorunlu kýlýyoruz
public class PanelClickSound : MonoBehaviour, IPointerClickHandler // IPointerClickHandler arayüzünü implement ediyoruz
{
    private AudioSource audioSource;
    public GameObject electricPref;
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource bileþenini alýyoruz
    }

    // IPointerClickHandler arayüzünden gelen bu metod, panel týklandýðýnda tetiklenir
    public void OnPointerClick(PointerEventData eventData)
    {
        Instantiate(electricPref, Vector3.zero, Quaternion.identity);
        audioSource.Play(); // Ses dosyasýný çal
    }
}
