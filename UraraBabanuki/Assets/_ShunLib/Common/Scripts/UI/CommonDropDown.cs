using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ShunLib.UI.DropDown.Common
{
    public class CommonDropDown : TMP_Dropdown
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private List<string> _stringItems = default;
        private List<Sprite> _spriteItems = default;

        // 値が変更されたときのコールバック
        private Action _changeValueCallback = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void Initialize()
        {
            InitItem();
            // 入力された値が変更されたときのイベント設定
            this.onValueChanged.AddListener((str) => {
                _changeValueCallback?.Invoke();
            });
        }

        // // 表示
        // public void Show()
        // {
        //     gameObject.SetActive(true);
        // }

        // // 非表示
        // public void Hide()
        // {
        //     gameObject.SetActive(false);
        // }

        // 値設定(テキスト)
        public void AddItems(List<string> items)
        {
            AddOptions(items);
            _stringItems = items;
        }

        // 値設定(画像)
        public void AddItems(List<Sprite> items)
        {
            AddOptions(items);
            _spriteItems = items;
        }

        // 現在の値を取得
        public string GetSelectItem()
        {
            return options[value].text;
        }

        // 入力イベント設定
        public void SetChangeValueCallback(Action callback)
        {
            _changeValueCallback = callback;
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        

        // 値初期化
        private void InitItem()
        {
            ClearOptions();
        }
    }
}