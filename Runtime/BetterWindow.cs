using System;
using UnityEngine;
using UnityEngine.Events;

namespace RandomExtensions
{
    public class BetterWindow : MonoBehaviour
    {
        public event Action OnEnabled;
        public event Action OnDisabled;

        public bool Opened => _opened;
        
        [SerializeField] private bool autoClose;
        [SerializeField] private bool autoOpen;
        
        [SerializeField] private UnityEvent onEnable;
        [SerializeField] private UnityEvent onDisable;
        
        private bool _opened;

        public void Open()
        {
            if (_opened)
                return;
            
            _opened = true;

            if (autoOpen)
            {
                gameObject.SetActive(true);
            }
            
            OnEnabled?.Invoke();
            onEnable?.Invoke();
        }

        public void Close()
        {
            if (!_opened)
                return;
            
            _opened = false;

            if (autoClose)
            {
                gameObject.SetActive(false);
            }
            
            OnDisabled?.Invoke();
            onDisable?.Invoke();
        }
    }
}