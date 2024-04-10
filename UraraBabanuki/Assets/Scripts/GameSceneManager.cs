using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ShunLib.Manager.CommonScene;
using ShunLib.Manager.Scene;

namespace UraraBabanuki.Scripts
{
    public class GameSceneManager : CommonSceneManager
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        // ---------- Private関数 ----------

        // オプション
        private void Option()
        {
            // TODO
            SceneLoadManager.Instance.TransitionScene(GameConst.SCENE_TITLE);
        }

        // ---------- protected関数 ---------

        // データ設定
        protected override void InitializeData()
        {

        }

        // UIの設定
        protected override void InitializeUI()
        {
            // オプションボタン処理設定
            uiManager.SetButtonEvent(GameConst.GAME_OPTION_BUTTON, Option);
        }

        // イベントの設定
        protected override void InitEvent()
        {
            
        }

        // ---------- デバッグ用関数 ---------
    }
}


