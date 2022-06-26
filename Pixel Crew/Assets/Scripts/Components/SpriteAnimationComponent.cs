using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteAnimationComponent : MonoBehaviour
    {
        [SerializeField] [Range(1, 30)] private int _frameRate ;
        [SerializeField] private UnityEvent<string> OnComplete;
        [SerializeField] private AnimationClip[] _clips;

        private SpriteRenderer _spriteRenderer;
        
        private float _secPerFrame;
        private float _nextFarmeTime;
        private int _currentFrame;
        private bool _isPlaying = true;

        private int _currentClip;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _secPerFrame = 1f / _frameRate;

            StartAnimation();
        }

        private void OnBecameVisible()
        {
            enabled = _isPlaying;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }

        public void SetClip(string clipName)
        {
            for (var i = 0; i < _clips.Length; i++)
            {
                if (_clips[i].Name == clipName)
                {
                    _currentClip = i;
                    StartAnimation();
                    return;
                }
            }

            enabled = _isPlaying = false;
        }

        private void StartAnimation()
        {
            _nextFarmeTime = Time.time + _secPerFrame;
            enabled =  _isPlaying = true;
            _currentFrame = 0;
        }

        private void OnEnable()
        {
            _nextFarmeTime = Time.time + _secPerFrame;
        }

        private void Update()
        {
            if ( _nextFarmeTime > Time.time) return;

            var clip = _clips[_currentClip];
            if (_currentFrame >= clip.Sprites.Length)
            {
                if (clip.Loop)    
                {
                    _currentFrame = 0;
                }
                else
                {
                    enabled = _isPlaying = clip.AllowNextClip;    
                    clip.OnComplete?.Invoke();
                        OnComplete?.Invoke(clip.Name);
                        if (clip.AllowNextClip)
                        {
                            _currentFrame = 0;
                            _currentClip = (int) Mathf.Repeat(_currentClip + 1, _clips.Length);
                        }
                }
                return;
            }

            _spriteRenderer.sprite = clip.Sprites[_currentFrame];

            _nextFarmeTime += _secPerFrame;
            _currentFrame++;
        }
    }

    [Serializable]
    public class AnimationClip
    {
        [SerializeField] private string _name;
        [SerializeField] private Sprite[] _sprites;
        [SerializeField] private bool _loop;
        [SerializeField] private bool _allowNextClip;
        [SerializeField] private UnityEvent _onComplete;

        public string Name => _name;
        public Sprite[] Sprites => _sprites;
        public bool Loop => _loop;
        public bool AllowNextClip => _allowNextClip;
        public UnityEvent OnComplete => _onComplete;
    }

    
}
