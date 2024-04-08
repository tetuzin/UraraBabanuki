using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShunLib.Controller.UpdateAction
{
    public class UpdateActionController : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        private Dictionary<string, UpdateActionData> _updateActionList = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void Initialize()
        {
            _updateActionList = new Dictionary<string, UpdateActionData>();
        }

        // 処理を追加
        public void AddUpdateAction(string key, UpdateActionData updateAction)
        {
            if (_updateActionList.ContainsKey(key))
            {
                float progressTime = _updateActionList[key].progressTime;
                _updateActionList[key] = updateAction;
                _updateActionList[key].progressTime = progressTime;
            }
            else
            {
                _updateActionList.Add(key, updateAction);
            }
        }

        // Update文で処理はしらせるとき
        public void UpdateAction()
        {
            UpdateAction(Time.deltaTime);
        }

        // Update文以外で処理を走らせるとき
        public void UpdateAction(float progressTime)
        {
            foreach(var kvp in _updateActionList)
            {
                UpdateActionData data = kvp.Value;
                data.progressTime += progressTime;

                // 処理の実行
                if (data.progressTime >= data.actionTime)
                {
                    data.progressTime = 0f;
                    data.action?.Invoke();
                    data.count++;
                }
            }
            RemoveUpdateAction();
        }

        // ---------- Private関数 ----------

        // 一回限りの実行済みUpdateActionを削除する
        private void RemoveUpdateAction()
        {
            List<string> removeDataList = new List<string>();
            foreach (var kvp in _updateActionList)
            {
                UpdateActionData data = kvp.Value;
                if (data.actionType != UpdateActionType.ROOP)
                {
                    if (data.count > 0)
                    {
                        removeDataList.Add(kvp.Key);
                    }
                }
            }
            foreach (string key in removeDataList)
            {
                _updateActionList.Remove(key);
            }
        }

        // ---------- protected関数 ---------
        // ---------- デバッグ用関数 ---------
    }

    public class UpdateActionData
    {
        // 経過時間
        public float progressTime = 0f;

        // 実行時間
        public float actionTime = 0f;

        // 実行処理
        public Action action = default;

        // 実行種別
        public UpdateActionType actionType = UpdateActionType.NONE;

        // 実行回数
        public int count = 0;
    }

    public enum UpdateActionType
    {
        NONE = 0,
        ONCE = 1,
        ROOP = 2,
    }
}

