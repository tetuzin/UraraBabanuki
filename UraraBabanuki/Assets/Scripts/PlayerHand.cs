using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UraraBabanuki.Scripts
{
    public class PlayerHand : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [Header("左手オブジェクト")]
        [SerializeField] private GameObject _leftHandObject = default;

        [Header("右手オブジェクト")]
        [SerializeField] private GameObject _rightHandObject = default;

        [Header("手札Object")]
        [SerializeField] private GameObject _playerHandObject = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        // プレイヤー手札
        private List<CardUnit> _cardUnitList = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 手札初期化
        public void InitCardUnitList(List<CardUnit> _cardUnitPrefabs)
        {
            _cardUnitList = new List<CardUnit>();
            for (int i = 1; i <= GameConst.MAX_HAND_COUNT; i++)
            {
                CardUnit unit = Instantiate(_cardUnitPrefabs[i], Vector3.zero, Quaternion.identity);
                unit.transform.SetParent(_playerHandObject.transform);
                unit.transform.localPosition = Vector3.zero;
                unit.transform.localScale = Vector3.one;
                unit.Initialize();
                _cardUnitList.Add(unit);
            }
        }

        // 手札追加
        public void AddCardUnitList(CardUnit card)
        {
            _cardUnitList.Add(card);
            card.transform.SetParent(_playerHandObject.transform);
            card.transform.localPosition = Vector3.zero;
            card.transform.localScale = Vector3.one;
            Alignment();
        }

        // 手札を扇形に整列
        public void Alignment()
        {
            float[] _cardPos = new float[_cardUnitList.Count];
            float[] _cardRot = new float[_cardUnitList.Count];

            // カードのX座標を計算
            float _distance = _rightHandObject.transform.localPosition.x - _leftHandObject.transform.localPosition.x;
            float _interval = _distance / (_cardUnitList.Count - 1);
            float _totalInterval = 0f;
            for (int i = 0; i < _cardUnitList.Count; i++)
            {
                _cardPos[i] = (float)(_leftHandObject.transform.localPosition.x + _totalInterval);
                _totalInterval +=  _interval;
            }

            // カードのZ傾きを計算
            if (_cardUnitList.Count % 2 == 0)
            {
                // 手札枚数が偶数のとき

            }
            else
            {
                // 手札枚数が奇数のとき
            }
            
            // カードオブジェクトを移動&傾ける
            for (int i = 0; i < _cardUnitList.Count; i++)
            {
                _cardUnitList[i].transform.SetParent(_playerHandObject.transform);
                _cardUnitList[i].transform.localPosition = new Vector3(_cardPos[i], 70f, 0f);
                // _cardUnitList[i].transform.localRotate = new Vector3(0f, 0f, _cardRot[i]);
                _cardUnitList[i].transform.localScale = Vector3.one;
            }
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        // ---------- デバッグ用関数 ---------
    }
}

