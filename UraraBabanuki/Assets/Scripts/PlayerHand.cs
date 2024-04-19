using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UraraBabanuki.Scripts
{
    public class PlayerHand : MonoBehaviour
    {
        // ---------- 定数宣言 ----------

        // 左端カードの傾き
        private const float _leftCardRotate = 35f;
        // 右端カードの傾き
        private const float _rightCardRotate = -35f;
        // 手札カード持ち手高さ
        private const float _handCardHeight = 70f;
        // 比例定数
        private const float _cons = -0.005f;

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
            float[] _cardPosX = new float[_cardUnitList.Count];
            float[] _cardPosY = new float[_cardUnitList.Count];
            float[] _cardRotZ = new float[_cardUnitList.Count];

            // カードのX座標を計算
            float _posXDistance = _rightHandObject.transform.localPosition.x - _leftHandObject.transform.localPosition.x;
            float _posXInterval = _posXDistance / (_cardUnitList.Count - 1);
            float _posXTotalInterval = 0f;
            float _rotZLeftInterval = _leftCardRotate / (_cardUnitList.Count / 2);
            float _rotZTotalLeftInterval = 0f;
            float _rotZRightInterval = _rightCardRotate / (_cardUnitList.Count / 2);
            float _rotZTotalRightInterval = 0f;
            for (int i = 0; i < _cardUnitList.Count; i++)
            {
                _cardPosX[i] = (float)(_leftHandObject.transform.localPosition.x + _posXTotalInterval);
                _posXTotalInterval +=  _posXInterval;
                if (_cardPosX[i] != 0)
                {
                    _cardPosY[i] = _handCardHeight + (_cons * _cardPosX[i] * _cardPosX[i]);
                }
                else
                {
                    _cardPosY[i] = _handCardHeight + _cons;
                }
                

                // カードのZ傾きを計算
                if (i < (_cardUnitList.Count / 2))
                {
                    _cardRotZ[i] = _leftCardRotate - _rotZTotalLeftInterval;
                    _rotZTotalLeftInterval += _rotZLeftInterval;
                }
                else if (_cardUnitList.Count % 2 == 1 && i == (_cardUnitList.Count / 2))
                {
                    _cardRotZ[i] = 0f;
                }
                else
                {
                    _rotZTotalRightInterval += _rotZRightInterval;
                    _cardRotZ[i] = _rotZTotalRightInterval;
                }
            }
            
            // カードオブジェクトを移動&傾ける
            for (int i = 0; i < _cardUnitList.Count; i++)
            {
                _cardUnitList[i].transform.SetParent(_playerHandObject.transform);
                _cardUnitList[i].transform.localPosition = new Vector3(_cardPosX[i], _cardPosY[i], 0f);
                _cardUnitList[i].transform.localRotation = Quaternion.Euler(0f, 0f, _cardRotZ[i]);
                _cardUnitList[i].transform.localScale = Vector3.one;
            }
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        // ---------- デバッグ用関数 ---------
    }
}

