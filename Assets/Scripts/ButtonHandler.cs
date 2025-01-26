using System;
using System.Collections;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script
{
    public class ButtonHandler : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler
    {
        public Sprite[] sprites;
        private Vector3 _originalScale;
        private int _currentVolume = 3;
        public float hoverScale = 1.2f;
        public float animationSpeed = 0.2f;
        public Image image;
        public float scrollSpeed = 20f;
        public float stepDelay = 0.15f;
        private bool _scroll = false;
        
        public GameObject playButton;
        public GameObject creditButton;
        public GameObject exitButton;
        public GameObject shareButton;
        public GameObject volumeButton;
        public AudioListener audioListener;
        public GameObject selectionBackground;
        public GameObject title;
        public RectTransform creditTransform;
        
        
        
        public void ButtonPlay()
        {
            MenuManager.instance.PlayGame ();
        }

        public void ButtonExit()
        {
            Application.Quit();
        }

        public void ButtonCredit()
        {
                // StartCoroutine(CreditMove());
                _scroll = true;
                
                //1720
                var vector2 = creditTransform.anchoredPosition;
                vector2.y = -1720f;
                creditTransform.anchoredPosition = vector2;
        }
        private void Update()
        {
            if (creditButton != null)
            {
                creditButton.GetComponent<Button>().interactable = !_scroll;
                creditButton.GetComponent<Image>().color = new Color(1, 1, 1, _scroll ? 0 : 1);
                creditButton.transform.GetChild(0).GetComponent<TMP_Text>().color = new Color32(50, 50, 50, (byte)(_scroll ? 0 : 255));
            }

            if (_scroll)
            {
                Test();
            }
        }

        private void Test()
        {
            if(creditTransform.anchoredPosition.y < 900)
            {
                creditTransform.anchoredPosition += Vector2.up * (scrollSpeed * Time.deltaTime);
                MenuManager.instance.credits = true;
            }
            else if(creditTransform.anchoredPosition.y > 900)
            {
                MenuManager.instance.credits = false;
                _scroll = false;
                
            }
        }

        public void ButtonVolume()
        {
            _currentVolume++;
            if (_currentVolume > 1)
                _currentVolume = 0;
            switch (_currentVolume)
            {
                case 0:
                    AudioListener.volume = 0f;
                    break;
                case 1:
                    AudioListener.volume = 1f;
                    break;
            }
            Settings.volume = _currentVolume * 1f;
            image.sprite = sprites[_currentVolume];
        }

        public void ButtonShare()
        {
            Application.OpenURL("https://www.youtube.com/watch?v=dQw4w9WgXcQ&pp=ygUJcmljayByb2xs");
        }
        private void Start()
        {
            // Store the original scale of the button
            _originalScale = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            // Scale up the button
            StopAllCoroutines(); // Stop other animations on this object
            StartCoroutine(ScaleTo(_originalScale * hoverScale));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            // Scale back the button
            StopAllCoroutines();
            StartCoroutine(ScaleTo(_originalScale));
        }

        private System.Collections.IEnumerator ScaleTo(Vector3 targetScale)
        {
            float time = 0f;
            Vector3 initialScale = transform.localScale;

            while (time < animationSpeed)
            {
                time += Time.deltaTime;
                transform.localScale = Vector3.Lerp(initialScale, targetScale, time / animationSpeed);
                yield return null;
            }

            transform.localScale = targetScale;
        }
    }
}
