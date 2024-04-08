using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace ShunLib.UI.Text_Window
{
    public class TextWindow : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [Header("テキスト")]
        [SerializeField] protected TextMeshProUGUI mainText = default;

        [Header("ウィンドウ")]
        [SerializeField] protected CanvasGroup window = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public virtual void Initialize()
        {
            SetActive(false);
            SetText("");
        }


        // メインテキストの設定
        public virtual void SetText(string main)
        {
            if (mainText == default) return;
            mainText.text = main;
        }

        // ウィンドウの表示・非表示
        public void SetActive(bool isActive)
        {
            if (window == default) return;

            window.alpha = isActive ? 1 : 0;
            window.interactable = isActive;
            window.blocksRaycasts = isActive;
        }

        // ウィンドウのフェード
        public void Fade(bool isActive, float speed, Action callback = null)
        {
            if (window == default)
            {
                callback?.Invoke();
                return;
            }

            window.DOFade(isActive ? 1f : 0f, speed).OnComplete(() => {
                SetActive(isActive);
                callback?.Invoke();
            });
        }
        
        // ウィンドウのアルファ値設定
        public void SetAlpha(string key, float alpha)
        {
            if (window == default) return;

            if (alpha > 1.0f)
            {
                window.alpha = 1.0f;
            }
            else if (alpha < 0)
            {
                window.alpha = 0.0f;
            }
            else
            {
                window.alpha = alpha;
            }
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        // ---------- デバッグ用関数 ---------
    }
}