using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using ShunLib.Btn.Common;
using System;

namespace ShunLib.UI.ParameterSettingFrame
{
    public class CommonParameterSettingFrame : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [Header("テキスト")]
        [SerializeField] public TextMeshProUGUI mainText = default;

        [Header("値")]
        [SerializeField] public TextMeshProUGUI valueText = default;

        [Header("上ボタン")]
        [SerializeField] protected CommonButton _upButton = default;

        [Header("下ボタン")]
        [SerializeField] protected CommonButton _downButton = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void Initialize()
        {
            valueText.text = "0";
            _upButton.Initialize();
            _downButton.Initialize();
        }

        // 初期化(設定込み)
        public void Initialize(string text, int value, Action upAction, Action downAction)
        {
            Initialize();
            SetText(text);
            SetValue(value);
            SetUpButtonEvent(upAction);
            SetDownButtonEvent(downAction);
        }

        // テキスト設定
        public void SetText(string text)
        {
            mainText.text = text;
        }

        // 値設定
        public void SetValue(int value)
        {
            valueText.text = value.ToString();
        }

        // 上ボタンの処理設定
        public void SetUpButtonEvent(Action action)
        {
            _upButton.SetOnEvent(action);
        }

        // 下ボタンの処理設定
        public void SetDownButtonEvent(Action action)
        {
            _downButton.SetOnEvent(action);
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        // ---------- デバッグ用関数 ---------
    }  
}

