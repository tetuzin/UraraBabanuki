using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ShunLib.Manager.CommonScene;
using ShunLib.Manager.Scene;
using ShunLib.Utils.Random;
using UnityEditor.Experimental.GraphView;

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

        [Header("カードデザインオブジェクト")]
        [SerializeField] private CardDesignScriptableObject _cardDesignObject = default;
        [Header("カードプレハブ")]
        [SerializeField] private CardUnit _cardUnitPrefab = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        // 選択中カードNo
        private CardUnit _selectCard = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        // ---------- Private関数 ----------

        // お互いの手札を初期化
        private void InitCardUnitList()
        {
            List<CardUnit> _playerCardUnitList = CreateCardUnitList();
            List<CardUnit> _enemyCardUnitList = CreateCardUnitList();

            // ジョーカー(ランダムに割り振る)
            CardUnit joker = CreateCardUnit(0);
            if (RandomUtils.GetRandomBool(2))
                _playerCardUnitList.Add(joker);
            else
                _enemyCardUnitList.Add(joker);

            // 手札格納
            _playerHand.InitCardUnitList(_playerCardUnitList);
            _enemyHand.InitCardUnitList(_enemyCardUnitList);

            // エネミー手札にイベントを設定
            foreach (CardUnit card in _enemyCardUnitList)
            {
                card.SetOnClickAction(() => {
                    if (card == _selectCard)
                    {
                        Debug.Log("!!!!");
                    }  
                    else if (_selectCard != default)
                    {
                        Debug.Log("Number = " + _selectCard.Number);
                        _enemyHand.SelectCardDown(_selectCard.Number);
                        _selectCard = card;
                        _enemyHand.SelectCardUp(card.Number);
                    }
                    else
                    {
                        _selectCard = card;
                        _enemyHand.SelectCardUp(card.Number);
                    }
                });
            }
            
            // カード整列
            _playerHand.Alignment();
            _enemyHand.Alignment();
        }

        // 手札生成
        private List<CardUnit> CreateCardUnitList()
        {
            List<CardUnit> _cardUnitList = new List<CardUnit>();
            for (int i = 1; i <= GameConst.MAX_HAND_COUNT; i++)
            {
                _cardUnitList.Add(CreateCardUnit(i));
            }
            return _cardUnitList;
        }

        // カード生成
        private CardUnit CreateCardUnit(int number)
        {
            CardUnit unit = Instantiate(_cardUnitPrefab, Vector3.zero, Quaternion.identity);
            unit.transform.localPosition = Vector3.zero;
            unit.transform.localScale = Vector3.one;
            unit.Initialize();
            unit.Number = number;
            unit.SetImage(_cardDesignObject.CardImageList[number], _cardDesignObject.CardBackImage);
            return unit;
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
            if (_cardDesignObject == default) return;
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


