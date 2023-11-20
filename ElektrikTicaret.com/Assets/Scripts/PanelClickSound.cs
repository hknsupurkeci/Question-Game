using UnityEngine;
using UnityEngine.EventSystems; // EventSystems k�t�phanesini eklemeyi unutmay�n

[RequireComponent(typeof(AudioSource))] // AudioSource bile�eninin bu GameObject'te olmas�n� zorunlu k�l�yoruz
public class PanelClickSound : MonoBehaviour, IPointerClickHandler // IPointerClickHandler aray�z�n� implement ediyoruz
{
    private AudioSource audioSource;
    public GameObject electricPref;
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // AudioSource bile�enini al�yoruz
    }

    // IPointerClickHandler aray�z�nden gelen bu metod, panel t�kland���nda tetiklenir
    public void OnPointerClick(PointerEventData eventData)
    {
        Instantiate(electricPref, Vector3.zero, Quaternion.identity);
        audioSource.Play(); // Ses dosyas�n� �al
    }
}
