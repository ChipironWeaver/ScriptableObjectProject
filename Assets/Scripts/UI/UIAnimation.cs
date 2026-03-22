using UnityEngine;
using System.Collections.Generic;
using Image = UnityEngine.UI.Image;
public class UIAnimation : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private List<Sprite> _frames;
    [SerializeField] private float _transitionTime = 0.5f;
    [SerializeField] private Image _target;
    [Header("BackGround Gradient")]
    public Gradient backgroundGradient;
    [SerializeField] private float _gradientTime = 2f;
    [SerializeField] private Image _backgroundImage;
    
    
    private float _currentAnimationTime;
    private float _currentGradientTime;
    private int  _currentFrame;

    void Start()
    {
        if (_target == null) 
        {
            _target = GetComponent<Image>();
        }
    }
    void Update()
    {
        _currentAnimationTime += Time.deltaTime;
        _currentGradientTime += Time.deltaTime;
        
        if (_currentAnimationTime >= _transitionTime)
        {
            _currentAnimationTime -= _transitionTime;
            _currentFrame++;
            if (_currentFrame >= _frames.Count)
            {
                _currentFrame = 0;
            }
            _target.sprite = _frames[_currentFrame];
        }
        
        _backgroundImage.color = backgroundGradient.Evaluate( Mathf.PingPong(_currentGradientTime / _gradientTime,1));
    }
}
