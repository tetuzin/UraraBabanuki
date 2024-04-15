using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ShunLib.Manager.CommonScene;
using ShunLib.Manager.Scene;
using ShunLib.Utils.Random;

namespace UraraBabanuki.Scripts
{
    public class GameSceneManager : CommonSceneManager
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [Header("プレイヤーの手札")]
        [SerializeField] private PlayerHand _playerHand = default;
        [Header("エネミーの手札")]
        [SerializeField] private PlayerHand _enemyHand = default;

        [Header("カードプレハブ")]
        [SerializeField] private List<CardUnit> _cardUnitPrefabs = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        // ---------- Private関数 ----------

        // お互いの手札を初期化
        private void InitCardUnitList()
        {
            // プレイヤー
            _playerHand.InitCardUnitList(_cardUnitPrefabs);

            // エネミー
            _enemyHand.InitCardUnitList(_cardUnitPrefabs);

            // ジョーカー(ランダムに割り振る)
            CardUnit joker = Instantiate(_cardUnitPrefabs[0], Vector3.zero, Quaternion.identity);
            joker.Initialize();
            if (RandomUtils.GetRandomBool(2))
            {
                _playerHand.AddCardUnitList(joker);
            }
            else
            {
                _enemyHand.AddCardUnitList(joker);
            }
            
            // カード整列
            _playerHand.Alignment();
            _enemyHand.Alignment();
        }

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
            InitCardUnitList();
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


