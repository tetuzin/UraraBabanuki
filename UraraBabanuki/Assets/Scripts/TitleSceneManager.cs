using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ShunLib.Manager.CommonScene;
using ShunLib.Manager.Scene;

namespace UraraBabanuki.Scripts
{
    public class TitleSceneManager : CommonSceneManager
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

        // ゲームプレイ開始
        private void Play()
        {
            // TODO
            SceneLoadManager.Instance.TransitionScene(GameConst.SCENE_GAME);
        }

        // オプション
        private void Option()
        {

        }

        // クレジット
        private void Credit()
        {

        }

        // ---------- protected関数 ---------

        // データ設定
        protected override void InitializeData()
        {

        }

        // UIの設定
        protected override void InitializeUI()
        {
            // スタートUIグループを表示
            uiManager.SetCanvasGroupActive(GameConst.TITLE_START_GROUP, true);

            // スタートボタン処理設定
            uiManager.SetButtonEvent(GameConst.TITLE_START_BUTTON, () => {
                uiManager.SetCanvasGroupActive(GameConst.TITLE_START_GROUP, false);
                uiManager.SetCanvasGroupActive(GameConst.TITLE_MENU_GROUP, true);
            });

            // ゲームプレイボタン処理設定
            uiManager.SetButtonEvent(GameConst.TITLE_PLAY_BUTTON, Play);

            // オプションボタン処理設定
            uiManager.SetButtonEvent(GameConst.TITLE_OPTION_BUTTON, Option);

            // クレジットボタン処理設定
            uiManager.SetButtonEvent(GameConst.TITLE_CREDIT_BUTTON, Credit);

            // もどるボタン処理設定
            uiManager.SetButtonEvent(GameConst.TITLE_BACK_BUTTON, () => {
                uiManager.SetCanvasGroupActive(GameConst.TITLE_START_GROUP, true);
                uiManager.SetCanvasGroupActive(GameConst.TITLE_MENU_GROUP, false);
            });
        }

        // イベントの設定
        protected override void InitEvent()
        {

        }

        // ---------- デバッグ用関数 ---------
    }
}

