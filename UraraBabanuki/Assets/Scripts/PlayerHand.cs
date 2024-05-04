using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UraraBabanuki.Scripts
{
    public class PlayerHand : MonoBehaviour
    {
        // ---------- 定数宣言 ----------

        // 左端カードの傾き
        private const float LEFT_CARD_ROTATE = 35f;
        // 右端カードの傾き
        private const float RIGHT_CARD_ROTATE = -35f;
        // 手札カード持ち手高さ
        private const float HAND_CARD_HEIGHT = 70f;
        // 比例定数
        private const float CONS = -0.005f;

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
            _cardUnitList = _cardUnitPrefabs;
            foreach (CardUnit card in _cardUnitList)
            {
                card.transform.SetParent(_playerHandObject.transform);
                card.transform.localPosition = Vector3.zero;
                card.transform.localScale = Vector3.one;
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
            // 各カードの座標を計算
            float[] _cardPosX = new float[_cardUnitList.Count];
            float[] _cardPosY = new float[_cardUnitList.Count];
            float[] _cardRotZ = new float[_cardUnitList.Count];
            
            // カードが1枚
            if (_cardUnitList.Count == 1)
            {
                float _posXCenter = (_rightHandObject.transform.localPosition.x + _leftHandObject.transform.localPosition.x) / 2;
                _cardPosX[0] = _posXCenter;
                _cardPosY[0] = HAND_CARD_HEIGHT;
                _cardRotZ[0] = 0f;
            }

            // カードが2枚
            else if (_cardUnitList.Count == 2)
            {
                float _posXCenter = (_rightHandObject.transform.localPosition.x + _leftHandObject.transform.localPosition.x) / 2;

                _cardPosX[0] = (_leftHandObject.transform.localPosition.x + _posXCenter) / 2;
                _cardPosY[0] = HAND_CARD_HEIGHT + (CONS * _cardPosX[0] * _cardPosX[0]);
                _cardRotZ[0] = LEFT_CARD_ROTATE / 2;

                _cardPosX[1] = (_posXCenter + _rightHandObject.transform.localPosition.x) / 2;
                _cardPosY[1] = HAND_CARD_HEIGHT + (CONS * _cardPosX[1] * _cardPosX[1]);
                _cardRotZ[1] = RIGHT_CARD_ROTATE / 2;
            }

            // カードが3枚以上
            else
            {
                float _posXDistance = _rightHandObject.transform.localPosition.x - _leftHandObject.transform.localPosition.x;
                float _posXInterval = _posXDistance / (_cardUnitList.Count - 1);
                float _posXTotalInterval = 0f;
                float _rotZLeftInterval = LEFT_CARD_ROTATE / (_cardUnitList.Count / 2);
                float _rotZTotalLeftInterval = 0f;
                float _rotZRightInterval = RIGHT_CARD_ROTATE / (_cardUnitList.Count / 2);
                float _rotZTotalRightInterval = 0f;
                for (int i = 0; i < _cardUnitList.Count; i++)
                {
                    _cardPosX[i] = (float)(_leftHandObject.transform.localPosition.x + _posXTotalInterval);
                    _posXTotalInterval +=  _posXInterval;
                    if (_cardPosX[i] != 0)
                    {
                        _cardPosY[i] = HAND_CARD_HEIGHT + (CONS * _cardPosX[i] * _cardPosX[i]);
                    }
                    else
                    {
                        _cardPosY[i] = HAND_CARD_HEIGHT + CONS;
                    }

                    // カードのZ傾きを計算
                    if (i < (_cardUnitList.Count / 2))
                    {
                        _cardRotZ[i] = LEFT_CARD_ROTATE - _rotZTotalLeftInterval;
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

        // カードリストを取得
        public List<CardUnit> GetCardUnitList()
        {
            return _cardUnitList;
        }

        // 指定カードを引き上げる
        public void SelectCardUp(int number)
        {
            CardUnit selectCard = default;
            foreach (CardUnit card in _cardUnitList)
            {
                if (card.Number == number) selectCard = card;
            }
            if (selectCard == default) return;
            selectCard.transform.localPosition += 
                selectCard.transform.TransformDirection(Vector3.up) * (selectCard.GetCardSize().y / 2);
        }

        // 選択中カードを引き下げる
        public void SelectCardDown(int number)
        {
            CardUnit selectCard = default;
            foreach (CardUnit card in _cardUnitList)
            {
                if (card.Number == number) selectCard = card;
            }
            if (selectCard == default) return;
            selectCard.transform.localPosition -= 
                selectCard.transform.TransformDirection(Vector3.up) * (selectCard.GetCardSize().y / 2);
        }

        // 指定番号のカードの存在チェック
        public bool CheckCardNumber(int number)
        {
            foreach (CardUnit card in _cardUnitList)
            {
                if (card.Number == number) return true;
            }
            return false;
        }

        // カードを加える
        public void PushCard(CardUnit pushCard)
        {
            _cardUnitList.Add(pushCard);
            pushCard.transform.SetParent(_playerHandObject.transform);
            pushCard.transform.position = Vector3.zero;
            pushCard.transform.rotation = Quaternion.identity;
            Alignment();
        }

        // 指定番号のカードを返す
        public CardUnit PopCard(int number)
        {
            CardUnit popCard = new CardUnit();
            foreach (CardUnit card in _cardUnitList)
            {
                if (card.Number == number) 
                {
                    popCard = card;
                    popCard.transform.SetParent(null);
                    popCard.transform.position = Vector3.zero;
                    popCard.transform.rotation = Quaternion.identity;
                    _cardUnitList.Remove(card);
                    Alignment();
                    return card;
                }
            }
            return popCard;
        }

        // 指定番号のカードを削除
        public void DeleteCard(int number)
        {
            foreach (CardUnit card in _cardUnitList)
            {
                if (card.Number == number)
                {
                    CardUnit deleteCard = card;
                    _cardUnitList.Remove(card);
                    deleteCard.Delete();
                    Alignment();
                    return;
                }
            }
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        // ---------- デバッグ用関数 ---------
    }
}

