using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShunLib.Controller.Audio
{
    public class AudioSourceController : MonoBehaviour
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [Header("オーディオソース一覧")] 
        [SerializeField] private List<AudioSource> _audioSourceList = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public void Initialize()
        {
            foreach (AudioSource source in _audioSourceList)
            {
                source.loop = false;
                source.clip = null;
            }
        }

        // 全てのAudioSourceの再生を止める
        public void StopAllAudioSource()
        {
            foreach (AudioSource source in _audioSourceList)
            {
                source.Stop();
            }
        }

        // 使用可能なAudioSourceを返す
        public AudioSource GetAudioSource()
        {
            foreach (AudioSource source in _audioSourceList)
            {
                if (!source.isPlaying)
                {
                    return source;
                }
            }
            return _audioSourceList[0];
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        // ---------- デバッグ用関数 ---------
    }
}

