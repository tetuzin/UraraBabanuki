using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UraraBabanuki.Scripts
{
    public class CardUnit : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        [Header("カード画像")] 
        [SerializeField] private Image _frontImage = default;
        [Header("カード背景画像")] 
        [SerializeField] private Image _backImage = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        public int Number = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void Initialize()
        {
            
        }

        // カード画像設定
        public void SetImage(Sprite frontSprite, Sprite backSprite)
        {
            _frontImage.sprite = frontSprite;
            _backImage.sprite = backSprite;
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        // ---------- デバッグ用関数 ---------
    }
}