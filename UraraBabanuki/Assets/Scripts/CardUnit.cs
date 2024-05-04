using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using ShunLib.Btn.Common;
using System;

namespace UraraBabanuki.Scripts
{
    public class CardUnit : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        [Header("RectTransform")] 
        [SerializeField] private RectTransform _rect = default;
        [Header("カード画像")] 
        [SerializeField] private Image _frontImage = default;
        [Header("カード背景画像")] 
        [SerializeField] private Image _backImage = default;
        [Header("クリック判定用ボタン")] 
        [SerializeField] private CommonButton _button = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        public int Number = default;
        private Action _onClickAction = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void Initialize()
        {
            _button.Initialize();
            _button.SetOnEvent(() => {
                _onClickAction?.Invoke();
            });
        }

        // Rect取得
        public Vector2 GetCardSize()
        {
            return _rect.sizeDelta;
        }

        // カード画像設定
        public void SetImage(Sprite frontSprite, Sprite backSprite)
        {
            _frontImage.sprite = frontSprite;
            _backImage.sprite = backSprite;
        }

        // カードクリック時処理
        public void SetOnClickAction(Action action)
        {
            _onClickAction = action;
        }

        // カード削除
        public void Delete()
        {
            Destroy(this.gameObject);
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        // ---------- デバッグ用関数 ---------
    }
}