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

        [Header("カードプレハブ")]
        [SerializeField] private List<CardUnit> _cardUnitPrefabs = default;

        [Header("プレイヤーの手札Object")]
        [SerializeField] private GameObject _playerHandObject = default;
        [Header("エネミーの手札Object")]
        [SerializeField] private GameObject _enemyHandObject = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        // プレイヤー手札
        private List<CardUnit> _playerCardUnitList = default;
        // エネミー手札
        private List<CardUnit> _enemyCardUnitList = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        // ---------- Private関数 ----------

        // お互いの手札を初期化
        private void InitCardUnitList()
        {
            // プレイヤー
            _playerCardUnitList = new List<CardUnit>();
            for (int i = 1; i <= GameConst.MAX_HAND_COUNT; i++)
            {
                CardUnit unit = Instantiate(_cardUnitPrefabs[i], Vector3.zero, Quaternion.identity);
                unit.transform.SetParent(_playerHandObject.transform);
                unit.transform.localPosition = Vector3.zero;
                unit.transform.localScale = Vector3.one;
                unit.Initialize();
                _playerCardUnitList.Add(unit);
            }

            // エネミー
            _enemyCardUnitList = new List<CardUnit>();
            for (int i = 1; i <= GameConst.MAX_HAND_COUNT; i++)
            {
                CardUnit unit = Instantiate(_cardUnitPrefabs[i], Vector3.zero, Quaternion.identity);
                unit.transform.SetParent(_enemyHandObject.transform);
                unit.transform.localPosition = Vector3.zero;
                unit.transform.localScale = Vector3.one;
                unit.Initialize();
                _enemyCardUnitList.Add(unit);
            }

            // ジョーカー(ランダムに割り振る)
            CardUnit joker = Instantiate(_cardUnitPrefabs[0], Vector3.zero, Quaternion.identity);
            joker.Initialize();
            if (RandomUtils.GetRandomBool(2))
            {
                joker.transform.SetParent(_enemyHandObject.transform);
                joker.transform.localPosition = Vector3.zero;
                joker.transform.localScale = Vector3.one;
                _enemyCardUnitList.Add(joker);
            }
            else
            {
                joker.transform.SetParent(_playerHandObject.transform);
                joker.transform.localPosition = Vector3.zero;
                joker.transform.localScale = Vector3.one;
                _playerCardUnitList.Add(joker);
            }
            
            // TODO カード整列
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


