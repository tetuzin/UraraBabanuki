using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

using ShunLib.Utils.Debug;
using ShunLib.Manager.Audio;
using ShunLib.Adv.Model;
using ShunLib.UI.Adv.Character;
using ShunLib.UI.Text_Window;

namespace ShunLib.UI.Message_Window
{
    public enum ShowMessageWindowState
    {
        NONE = 0,
        FADE_SLIDE_LEFT = 1,
        FADE_SLIDE_RIGHT = 2,
        FADE = 3,
        FADE_SCALE_UP = 4,
        FADE_SCALE_DOWN = 5
    }
    public enum HideMessageWindowState
    {
        NONE = 0,
        FADE_SLIDE_LEFT = 1,
        FADE_SLIDE_RIGHT = 2,
        FADE = 3,
        FADE_SCALE_UP = 4,
        FADE_SCALE_DOWN = 5
    }
    public class MessageWindow : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [Header("CanvasGroup")]
        [SerializeField] protected CanvasGroup canvasGroup = default;

        [Header("名前テキスト")]
        [SerializeField] protected TextWindow nameTextWindow = default;

        [Header("本文テキスト")]
        [SerializeField] protected TextWindow mainTextWindow = default;

        [Header("画像（アイコン等）")]
        [SerializeField] protected AdvCharacter advCharacter = default;

        [Header("ウィンドウ表示アニメーション")]
        [SerializeField] protected ShowMessageWindowState showMessageWindowState = ShowMessageWindowState.NONE;

        [Header("ウィンドウ表示速度")]
        [SerializeField] protected float showWindowSpeed = 0.25f;

        [Header("ウィンドウ非表示アニメーション")]
        [SerializeField] protected HideMessageWindowState hideMessageWindowState = HideMessageWindowState.NONE;

        [Header("ウィンドウ非表示速度")]
        [SerializeField] protected float hideWindowSpeed = 0.25f;

        [Header("表示SE")]
        [SerializeField] protected AudioClip showWindowAudioClip = default;

        [Header("非表示SE")]
        [SerializeField] protected AudioClip hideWindowAudioClip = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private string _mainText = default;
        private AudioClip _voiceClip = default;
        private AudioManager _audioManager = default;
        private Vector3 _localPosition = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public virtual void Initialize()
        {
            _mainText = default;
            _audioManager = default;
            _localPosition = canvasGroup.transform.localPosition;
            SetActive(false);
            nameTextWindow.Initialize();
            mainTextWindow.Initialize();
            advCharacter.Initialize();
        }

        // 表示・非表示
        public void SetActive(bool isActive)
        {
            if (canvasGroup == default) return;

            canvasGroup.alpha = isActive ? 1 : 0;
            canvasGroup.interactable = isActive;
            canvasGroup.blocksRaycasts = isActive;
        }

        // ウィンドウ表示
        public virtual void ShowMessageWindow(Action callback = null)
        {
            ShowAnimation(() => {
                SetActive(true);
                callback?.Invoke();
            });

            if (_audioManager != default && showWindowAudioClip != default)
            {
                _audioManager.PlaySE(showWindowAudioClip);
            }
            else
            {
                DebugUtils.LogWarning(
                    "メッセージウィンドウ[" + gameObject.name + 
                    "]にAudioManagerもしくはAudioClipが設定されていないため、ウィンドウ表示SEを再生できませんでした。"
                );
            }
        }

        // ウィンドウ非表示
        public virtual void HideMessageWindow(Action callback = null)
        {
            HideAnimation(() => {
                SetActive(false);
                callback?.Invoke();
            });

            if (_audioManager != default && hideWindowAudioClip != default)
            {
                _audioManager.PlaySE(hideWindowAudioClip);
            }
            else
            {
                DebugUtils.LogWarning(
                    "メッセージウィンドウ[" + gameObject.name + 
                    "]にAudioManagerもしくはAudioClipが設定されていないため、ウィンドウ非表示SEを再生できませんでした。"
                );
            }
        }

        // ウィンドウ表示アニメーションの設定
        public virtual void SetShowAnimation(ShowMessageWindowState state)
        {
            showMessageWindowState = state;
        }

        // ウィンドウ非表示アニメーションの設定
        public virtual void SetHideAnimation(HideMessageWindowState state)
        {
            hideMessageWindowState = state;
        }

        // AudioManagerの設定
        public virtual void SetAudioManager(AudioManager audioManager)
        {
            _audioManager = audioManager;
        }

        // メッセージ設定
        public virtual async Task SetMessage(AdvMessageModel model)
        {
            // 名前テキストの設定
            if (model.Name == "" || model.Name == null || model.Name == default)
            {
                nameTextWindow.SetActive(false);
            }
            else
            {
                nameTextWindow.SetText(model.Name);
                nameTextWindow.SetActive(true);
            }

            // 本文テキストの設定
            if (model.Text == "" || model.Text == null || model.Text == default)
            {
                mainTextWindow.SetActive(false);
            }
            else
            {
                _mainText = model.Text;
                mainTextWindow.SetActive(true);
            }

            // 画像の設定
            if (model.Sprite == null || model.Sprite == default)
            {
                advCharacter.SetActive(false);
            }
            else
            {
                advCharacter.SetImage(model.Sprite);
                advCharacter.SetActive(true);
            }

            // ボイスの設定
            if (model.Voice == null || model.Voice == default)
            {
                _voiceClip = default;
            }
            else
            {
                _voiceClip = model.Voice;
            }
            await Task.CompletedTask;
        }

        // メッセージ再生
        public virtual async Task PlayMessage()
        {
            // TODO 再生スタイルは何パターンか作成して、変えられるようにする

            mainTextWindow.SetText(_mainText);
            
            if (_audioManager == default)
            {
                await Task.Delay(3000);
            }
            else
            {
                await _audioManager.PlayVoiceAsync(_voiceClip);
            }
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------

        // ウィンドウ表示アニメーション再生
        protected virtual void ShowAnimation(Action callback = null)
        {
            switch(showMessageWindowState)
            {
                // 左からスライドしながらフェードして表示
                case ShowMessageWindowState.FADE_SLIDE_LEFT:
                    canvasGroup.transform.localPosition = new Vector3(
                        -this.gameObject.GetComponent<RectTransform>().sizeDelta.x,
                        _localPosition.y,
                        _localPosition.z
                    );
                    canvasGroup.transform.DOLocalMoveX(0f, showWindowSpeed);
                    canvasGroup.DOFade(1f, showWindowSpeed).OnComplete(() => {
                        callback?.Invoke();
                    });
                    break;

                // 右からスライドしながらフェードして表示
                case ShowMessageWindowState.FADE_SLIDE_RIGHT:
                    canvasGroup.transform.localPosition = new Vector3(
                        this.gameObject.GetComponent<RectTransform>().sizeDelta.x,
                        _localPosition.y,
                        _localPosition.z
                    );
                    canvasGroup.transform.DOLocalMoveX(0f, showWindowSpeed);
                    canvasGroup.DOFade(1f, showWindowSpeed).OnComplete(() => {
                        callback?.Invoke();
                    });
                    break;

                // フェードしながら拡大表示
                case ShowMessageWindowState.FADE_SCALE_UP:
                    canvasGroup.transform.localScale = Vector3.zero;
                    canvasGroup.transform.DOScale(Vector3.one, showWindowSpeed);
                    canvasGroup.DOFade(1f, showWindowSpeed).OnComplete(() => {
                        callback?.Invoke();
                    });
                    break;

                // フェードしながら縮小表示
                case ShowMessageWindowState.FADE_SCALE_DOWN:
                    canvasGroup.transform.localScale = Vector3.one * 2;
                    canvasGroup.transform.DOScale(Vector3.one, showWindowSpeed);
                    canvasGroup.DOFade(1f, showWindowSpeed).OnComplete(() => {
                        callback?.Invoke();
                    });
                    break;

                // フェードして表示
                case ShowMessageWindowState.FADE:
                    canvasGroup.DOFade(1f, showWindowSpeed).OnComplete(() => {
                        callback?.Invoke();
                    });
                    break;
                
                default:
                    callback?.Invoke();
                    break;
            }
        }

        // ウィンドウ非表示アニメーション再生
        protected virtual void HideAnimation(Action callback = null)
        {
            switch(hideMessageWindowState)
            {
                // 左からスライドしながらフェードして非表示
                case HideMessageWindowState.FADE_SLIDE_LEFT:
                    canvasGroup.transform.localPosition = _localPosition;
                    canvasGroup.transform.DOLocalMoveX(-this.gameObject.GetComponent<RectTransform>().sizeDelta.x, showWindowSpeed);
                    canvasGroup.DOFade(0f, showWindowSpeed).OnComplete(() => {
                        callback?.Invoke();
                    });
                    break;

                // 右からスライドしながらフェードして非表示
                case HideMessageWindowState.FADE_SLIDE_RIGHT:
                    canvasGroup.transform.localPosition = _localPosition;
                    canvasGroup.transform.DOLocalMoveX(this.gameObject.GetComponent<RectTransform>().sizeDelta.x, showWindowSpeed);
                    canvasGroup.DOFade(0f, showWindowSpeed).OnComplete(() => {
                        callback?.Invoke();
                    });
                    break;

                // フェードしながら拡大非表示
                case HideMessageWindowState.FADE_SCALE_UP:
                    canvasGroup.transform.localScale = Vector3.one;
                    canvasGroup.transform.DOScale(Vector3.one * 2, showWindowSpeed);
                    canvasGroup.DOFade(0f, showWindowSpeed).OnComplete(() => {
                        callback?.Invoke();
                    });
                    break;

                // フェードしながら縮小非表示
                case HideMessageWindowState.FADE_SCALE_DOWN:
                    canvasGroup.transform.localScale = Vector3.one;
                    canvasGroup.transform.DOScale(Vector3.zero, showWindowSpeed);
                    canvasGroup.DOFade(0f, showWindowSpeed).OnComplete(() => {
                        callback?.Invoke();
                    });
                    break;

                // フェードして非表示
                case HideMessageWindowState.FADE:
                    canvasGroup.DOFade(0f, hideWindowSpeed).OnComplete(() => {
                        callback?.Invoke();
                    });
                    break;

                default:
                    callback?.Invoke();
                    break;
            }
        }

        // ---------- デバッグ用関数 ---------
    }
}

