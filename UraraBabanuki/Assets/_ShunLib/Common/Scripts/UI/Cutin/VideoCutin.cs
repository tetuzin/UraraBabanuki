using System;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace ShunLib.UI.Cutin.Video
{
    public class VideoCutin : BaseCutin
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [Header("動画再生開始時間")]
        [SerializeField] protected float _startMovieTime = 0f;

        [Header("動画再生プレイヤー")]
        [SerializeField] protected VideoPlayer _videoPlayer = default;

        [Header("動画Clip")]
        [SerializeField] protected VideoClip _videoClip = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------

        // 動画再生時の処理
        public Action PlayVideoCallback { get; set; }

        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------
        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------

        // 初期化
        public override void Initialize()
        {
            base.Initialize();
            _videoPlayer.isLooping = false;
            _videoPlayer.playOnAwake = false;
            _videoPlayer.source = VideoSource.VideoClip;
            _videoPlayer.clip = _videoClip;
            _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            _videoPlayer.prepareCompleted += (vp) => {
                _videoPlayer.Play();
                _videoPlayer.Pause();
            };
            _videoPlayer.Prepare();
        }

        // カットイン表示
        public override async Task Show(Action callback = null)
        {
            if (!_isShow)
            {
                _isShow = true;
                ShowAnimation(callback);
                if (_startMovieTime <= _showTime)
                {
                    int beforeStopTime = (int)(_startMovieTime * 1000);
                    await Task.Delay(beforeStopTime);
                    _videoPlayer.Play();
                    PlayVideoCallback?.Invoke();
                    int afterStopTime = (int)(_showTime * 1000) - beforeStopTime;
                    await Task.Delay(afterStopTime);
                }
                else
                {
                    await Task.Delay((int)(_showTime * 1000));
                }
                Hide();
            }
        }

        // ---------- Private関数 ----------
        // ---------- protected関数 ---------
        // ---------- デバッグ用関数 ---------
    }
}

