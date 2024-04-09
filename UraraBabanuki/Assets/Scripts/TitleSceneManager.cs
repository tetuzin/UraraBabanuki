using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ShunLib.Manager.CommonScene;

namespace UraraBabanuki.Scripts
{
    public class TitleSceneManager : CommonSceneManager
    {
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
        }

        // イベントの設定
        protected override void InitEvent()
        {

        }
    }
}

