using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShunLib.UI.Adv.Character
{
    public class AdvCharacter : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [Header("キャラクター画像")]
        [SerializeField] protected Image characterImage = default;

        [Header("CanvasGroup")]
        [SerializeField] protected CanvasGroup canvasGroup = default;

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
            SetImage(default);
        }

        // 画像の設定
        public virtual void SetImage(Sprite sprite)
        {
            characterImage.sprite = sprite;
        }

        // ウィンドウの表示・非表示
        public void SetActive(bool isActive)
        {
            if (canvasGroup == default) return;

            canvasGroup.alpha = isActive ? 1 : 0;
            canvasGroup.interactable = isActive;
            canvasGroup.blocksRaycasts = isActive;
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        // ---------- デバッグ用関数 ---------
    }
}

