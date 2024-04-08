using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShunLib.UI.RadioIcon.Common
{
    public class CommonRadioIcon : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [Header("Onオブジェクト")]
        [SerializeField] protected GameObject onObject = default;

        [Header("Offオブジェクト")]
        [SerializeField] protected GameObject offObject = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        // 現在の状態
        protected bool state = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void Initialize()
        {
            offObject.SetActive(true);
            onObject.SetActive(false);
        }

        // OnOff切り替え
        public void SetIconState(bool isActive)
        {
            state = isActive;
            onObject.SetActive(isActive);
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        // ---------- デバッグ用関数 ---------
    }
}

