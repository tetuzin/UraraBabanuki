using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ShunLib.Manager.Audio;
using ShunLib.UI.Message_Window;
using ShunLib.UI.Adv.Member;

namespace ShunLib.UI.Adv
{
    public class AdvWindow : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [Header("メッセージウィンドウ")]
        [SerializeField] protected MessageWindow messageWindow = default;

        [Header("キャラクター立ち絵")]
        [SerializeField] protected AdvMember standMember = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------

        // オーディオマネージャー
        public AudioManager AudioManager { get; set; }

        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        // ---------- デバッグ用関数 ---------
    }
}

