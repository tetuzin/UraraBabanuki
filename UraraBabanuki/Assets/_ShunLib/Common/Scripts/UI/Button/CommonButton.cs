using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

using ShunLib.Const.Audio;
using ShunLib.Manager.Game;

namespace ShunLib.Btn.Common
{
    public class CommonButton : Button
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [Header("ボタンオブジェクト")]
        [SerializeField, Tooltip("ボタンオブジェクト")] public GameObject obj = default;

        [Header("イベントトリガー")]
        [SerializeField, Tooltip("イベントトリガー")] public EventTrigger eventTrigger = default;

        [Header("テキスト")]
        [SerializeField, Tooltip("テキスト")] public TextMeshProUGUI text = default;
        
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        
        [Header("長押しする時間")]
        [SerializeField] public float downWaitTime = 1.0f;

        [Header("SEを再生するかどうか")]
        [SerializeField] public bool isPlaySE = false;

        [Header("ボタンホバー時の音")]
        [SerializeField] public AudioClip onHoverAudioClip = default;

        [Header("ボタンホバーアウト時の音")]
        [SerializeField] public AudioClip offHoverAudioClip = default;

        [Header("ホバー時アニメーションを再生するかどうか")]
        [SerializeField] public bool isHoverAnim = true;
        
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private Vector3 _buttonScale = default;

        private bool _isActiveOnEvent = default;
        private bool _isActiveDownEvent = default;

        private bool _isDown = default;
        private bool _isOnDownEvent = default;
        private float _downTime = default;
        private Action _onEvent = default;
        private Action _onDownEvent = default;

        private EventTrigger.Entry _onEnter = default;
        private EventTrigger.Entry _onExit = default;
        private EventTrigger.Entry _onDown = default;
        private EventTrigger.Entry _onUp = default;
        
        // ---------- Unity組込関数 ----------

        private void FixedUpdate()
        {
            // ボタン長押し検知処理
            if (_isDown && !_isOnDownEvent)
            {
                _downTime += Time.deltaTime;
                if (_downTime >= downWaitTime)
                {
                    _isOnDownEvent = true;
                    if (_isActiveDownEvent) _onDownEvent?.Invoke();
                }
            }
        }

        // ---------- Public関数 ----------

        // 初期化
        public virtual void Initialize()
        {
            _isActiveOnEvent = true;
            _isActiveDownEvent = true;

            _buttonScale = this.gameObject.transform.localScale;
            
            _isDown = false;
            _isOnDownEvent = false;
            _downTime = 0.0f;

            RemoveOnEvent();
            RemoveOnDownEvent();
            
            if (eventTrigger == default) return;
            
            
            // ポインターがオブジェクトに入るときのイベント
            _onEnter = new EventTrigger.Entry();
            _onEnter.eventID = EventTriggerType.PointerEnter;
            _onEnter.callback.AddListener((data) => { OnPointerEnterButton((PointerEventData)data); });
            eventTrigger.triggers.Add(_onEnter);
            
            // ポインターがオブジェクトから出たときのイベント
            _onExit = new EventTrigger.Entry();
            _onExit.eventID = EventTriggerType.PointerExit;
            _onExit.callback.AddListener((data) => { OnPointerExitButton((PointerEventData)data); });
            eventTrigger.triggers.Add(_onExit);
            
            // ボタンを長押ししたときのイベント
            _onDown = new EventTrigger.Entry();
            _onDown.eventID = EventTriggerType.PointerDown;
            _onDown.callback.AddListener((data) => { OnPointerDownButton((PointerEventData)data); });
            eventTrigger.triggers.Add(_onDown);
            
            // ボタンを離したときのイベント
            _onUp = new EventTrigger.Entry();
            _onUp.eventID = EventTriggerType.PointerUp;
            _onUp.callback.AddListener((data) => { OnPointerUpButton((PointerEventData)data); });
            eventTrigger.triggers.Add(_onUp);
        }
        
        // ボタン長押し時のイベントを設定
        public void SetOnDownEvent(Action action)
        {
            _onDownEvent = action;
            // _isActiveDownEvent = true;
        }
        
        // ボタン長押し時のイベントを削除
        public void RemoveOnDownEvent()
        {
            _onDownEvent = () => { };
            // _isActiveDownEvent = false;
        }
        
        // ボタン長押し時イベントの活性化・非活性化
        public void SetOnDownActive(bool isActive)
        {
            _isActiveDownEvent = isActive;
        }
        
        // ボタン押下時のイベントを設定
        public void SetOnEvent(Action action)
        {
            _onEvent = action;
            // _isActiveOnEvent = true;
        }
        
        // ボタン押下時のイベントを削除
        public void RemoveOnEvent()
        {
            _onEvent = () => { };
            // _isActiveOnEvent = false;
        }
        
        // ボタン押下時イベントの活性化・非活性化
        public void SetOnActive(bool isActive)
        {
            _isActiveOnEvent = isActive;
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        
        // ボタンにポインターが入ったとき
        protected virtual void OnPointerEnterButton(PointerEventData data)
        {
            if (isPlaySE && GameManager.IsInstance() && onHoverAudioClip != default)
            {
                GameManager.Instance.audioManager.PlaySE(onHoverAudioClip);
            }
            if (isHoverAnim)
            {
                Vector3 scale = _buttonScale * 1.05f;
                obj.transform.DOScale(scale, 0.3f);
            }
        }
        
        // ボタンからポインターが出たとき
        protected virtual void OnPointerExitButton(PointerEventData data)
        {
            if (isHoverAnim)
            {
                obj.transform.DOScale(_buttonScale, 0.3f);
            }
        }
        
        // ボタンを長押ししたとき
        protected virtual void OnPointerDownButton(PointerEventData data)
        {
            _isDown = true;
            _downTime = 0.0f;
        }
        
        // ボタンを離したとき
        protected virtual void OnPointerUpButton(PointerEventData data)
        {
            _isDown = false;
            if (_isOnDownEvent)
            {
                _isOnDownEvent = false;
            }
            else
            {
                if (_isActiveOnEvent)
                {
                    if (isPlaySE && GameManager.IsInstance() && offHoverAudioClip != default)
                    {
                        GameManager.Instance.audioManager.PlaySE(offHoverAudioClip);
                    }
                    _onEvent?.Invoke();
                }
            }
        }
    }
}