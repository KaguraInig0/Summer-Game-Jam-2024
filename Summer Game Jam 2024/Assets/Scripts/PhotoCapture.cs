using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotoCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    private Texture2D screenCapture;

    public GameObject photoBttn;
    public bool canTakePicture { get; set; }

    private static PhotoCapture instance;
    public static PhotoCapture GetInstance() { return instance; }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    }
  
    public void takePicture()
    {
        StartCoroutine(CapturePhoto());
    }

    IEnumerator CapturePhoto()
    {
        photoBttn.SetActive(false);

        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect(0, 0, Screen.width, Screen.height);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();
    }

    void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;
        photoDisplayArea.gameObject.SetActive(true);
        Manager.GetInstance().photosTaken.Add(photoSprite);
        StartCoroutine(RemovePhoto());
    }

    IEnumerator RemovePhoto()
    {
        yield return new WaitForSeconds(3);

        photoDisplayArea.gameObject.SetActive(false);
    }
}