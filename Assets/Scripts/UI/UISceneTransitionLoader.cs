using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UISceneTransitionLoader : MonoBehaviour
{
    [SerializeField] private bool _autoFadeIn;
    [SerializeField] private Color _autoFadeColor =  Color.white;
    [SerializeField] private float _fadeDuration = 1f;
    [SerializeField] private List<SceneColorTransition> _sceneColorTransitions;
    [Serializable]
    public class SceneColorTransition
    {
        [SerializeField] public Color color = Color.black;
        [SerializeField] public string sceneName;
    }

    private Image _image;
    
    private void Start()
    {
        _image = GetComponent<Image>();
        if  (_autoFadeIn) FadeIn();
    }

    public void FadeOut(string sceneName)
    {
        Color color = _autoFadeColor;
        foreach (var sceneColorTransition in _sceneColorTransitions)
        {
            if (sceneColorTransition.sceneName == sceneName)
            {
                color = sceneColorTransition.color;
            }
        }
        StartCoroutine(FadeCoroutine(Color.clear,color, sceneName));
    }

    private void FadeIn()
    {
        StartCoroutine(FadeCoroutine(_autoFadeColor, Color.clear));
    }

    private IEnumerator FadeCoroutine(Color firstColor, Color secondColor, string sceneName = null)
    {
        float time = 0;
        while (time < _fadeDuration)
        {
            _image.color = Color.Lerp(firstColor, secondColor, time / _fadeDuration);
            time += Time.deltaTime;
            yield return new WaitForNextFrameUnit();
        }
        if (sceneName != null) 
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
