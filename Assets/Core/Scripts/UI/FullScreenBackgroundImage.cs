using UnityEngine;
using UnityEngine.UI;

public class FullScreenBackgroundImage : MonoBehaviour
{
    public bool AdjustOnce = true;
    Image _image;
    float _screenWidth;
    float _screenHeight;
    // Start is called before the first frame update
    void Start()
    {
        _image = gameObject.GetComponent<Image>();
        _updateImageScaler();
    }

    // Update is called once per frame
    void Update()
    {
        if (!AdjustOnce && (_screenWidth != Screen.width || _screenHeight != Screen.height)) _updateImageScaler();
    }

    void _updateImageScaler()
    {
        float screenAspectRatio = (float)Screen.width / Screen.height;

        float imageAspectRatio = (float)_image.sprite.textureRect.width / _image.sprite.textureRect.height;
        float scaler = (_image.sprite.textureRect.width >= _image.sprite.textureRect.height) ? (float)_image.sprite.textureRect.width / 100 : (float)_image.sprite.textureRect.height / 100;

        if (screenAspectRatio <= imageAspectRatio)
        {
            float times = Screen.height / _image.sprite.textureRect.height * scaler;
            transform.localScale = new Vector3(times, times, times);
        }
        else
        {
            float times = Screen.width / _image.sprite.textureRect.width * scaler;
            transform.localScale = new Vector3(times, times, times);
        }
        _screenHeight = Screen.height;
        _screenWidth = Screen.width;
    }
}
