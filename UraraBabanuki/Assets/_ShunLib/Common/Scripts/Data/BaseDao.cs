using UnityEngine;
using System.Collections.Generic;
using ShunLib.Utils.Json;

namespace ShunLib.Dao
{
    public interface BaseDao
    {
        void Initialize();
        void SetJsonFile(string fileName, TextAsset json);
        void LoadJsonMasterList();
        void SaveJsonMasterList();
    }

    public class BaseDao<T> : BaseDao where T : new()
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------
        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        protected List<T> list = default;
        protected string jsonFileName = default;
        protected TextAsset json = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // リストの初期化
        public void Initialize()
        {
            list = new List<T>();
        }

        // Modelのリストを取得
        public List<T> Get()
        {
            return list;
        }

        // Modelのリストを設定
        public void Set(List<T> data)
        {
            list = data;
        }

        // JSONファイルの設定
        public void SetJsonFile(string fileName, TextAsset textAsset)
        {
            jsonFileName = fileName;
            json = textAsset;
        }

        // JSONからデータを読み込みリストを返す
        public virtual void LoadJsonMasterList()
        {
            Set(JsonUtils.ConvertJsonToList<T>(json));
        }

        // リストを読み込みJSONに保存する
        public virtual void SaveJsonMasterList()
        {
            JsonUtils.SaveJsonList<T>(Get(), jsonFileName);
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
    }
}
