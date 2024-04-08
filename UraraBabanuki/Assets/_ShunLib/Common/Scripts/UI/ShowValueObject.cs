using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;

namespace ShunLib.UI.ShowValue
{
    public class ShowValueObject : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [Header("表示グループ")]
        [SerializeField] private CanvasGroup _canvasGroup = default;

        [Header("表示値テキスト")]
        [SerializeField] private TextMeshProUGUI _showText = default;

        [Header("フェードフラグ")]
        [SerializeField] private bool _isFade = false;

        [Header("フェード速度")]
        [SerializeField] private float _fadeSpeed = 0.2f;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        // 表示値
        private int _showValue = 0;
        // 表示時間
        private float _showTime = 0f;
        // 経過時間
        private float _progressTime = 0f;
        // 表示フラグ
        private bool _isShow = false;

        // ---------- Unity組込関数 ----------

        void FixedUpdate()
        {
            if (_isShow)
            {
                _progressTime += Time.deltaTime;
                if (_progressTime >= _showTime)
                {
                    EndShow();
                }
            }
        }

        // ---------- Public関数 ----------

        // 初期化
        public void Initialize()
        {
            SetCanvasGroupActive(false);
            _showValue = 0;
            _showTime = 0f;
            _progressTime = 0f;
            _isShow = false;
        }

        // 表示開始
        public void StartShow()
        {
            if (!_isShow)
            {
                _progressTime = 0f;
                SetCanvasGroupActive(true);
                _isShow = true;
            }
        }

        // 表示終了
        public void EndShow()
        {
            _isShow = false;
            if (_isFade)
            {
                FadeCanvasGroup(false, _fadeSpeed, () => {
                    Destroy(this.gameObject);
                });
            }
            else
            {
                SetCanvasGroupActive(false);
                Destroy(this.gameObject);
            }
        }

        // 表示値の設定
        public void SetValue(int value = 0)
        {
            _showValue = value;
            SetText(_showText.ToString());
        }

        // 表示文字列の設定
        public void SetText(string text)
        {
            _showText.text = text;
        }

        // アニメーションの設定
        public void SetAnimation()
        {
            this.gameObject.transform.DOLocalMoveY(200f, _showTime).SetRelative(true);
        }

        // 表示時間の設定
        public void SetShowTime(float time = 0f)
        {
            _showTime = time;
        }

        // ---------- Private関数 ----------

        // キャンバスグループの表示・非表示
        private void SetCanvasGroupActive(bool isActive)
        {
            _canvasGroup.alpha = isActive ? 1 : 0;
            _canvasGroup.interactable = isActive;
            _canvasGroup.blocksRaycasts = isActive;
        }

        // キャンバスグループのフェード
        private void FadeCanvasGroup(bool isActive, float speed, Action callback)
        {
            _canvasGroup.DOFade(isActive ? 1f : 0f, speed).OnComplete(() => {
                SetCanvasGroupActive(isActive);
                callback?.Invoke();
            });
        }

        // ---------- protected関数 ---------
        // ---------- デバッグ用関数 ---------
    }
}

