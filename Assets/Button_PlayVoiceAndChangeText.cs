using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Button_PlayVoiceAndChangeText : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    AudioSource audioSource;
    Button button;
    WaitForSeconds waitForSeconds = new WaitForSeconds(1);
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = this.GetComponent<AudioSource>();
        button = this.GetComponent<Button>();
        button.onClick.AddListener(PlaySource);
        text = this.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void PlaySource()
    {
        //audioSource.Play();
        StartCoroutine(StopSource());
    }
    IEnumerator StopSource()
    {
        yield return waitForSeconds;
        audioSource.Stop();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.color = Color.red;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.color = Color.white;
    }
}
