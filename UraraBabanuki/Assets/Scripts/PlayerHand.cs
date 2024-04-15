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
            
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        // ---------- デバッグ用関数 ---------
    }
}

