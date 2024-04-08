using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShunLib.Adv.Model
{
    [System.Serializable]
    public class AdvModel
    {
        // アドベンチャー名
        [SerializeField] private string _advName = default;
        // メッセージ一覧
        [SerializeField] private List<AdvMessageModel> _messageList = new List<AdvMessageModel>();

        public string AdvName
        {
            get { return _advName; }
            set { _advName = value; }
        }
        public List<AdvMessageModel> MessageList
        {
            get { return _messageList; }
            set { _messageList = value; }
        }
    }

    [System.Serializable]
    public class AdvMessageModel
    {
        // 名前
        [SerializeField] private string _name = default;
        // 本文
        [SerializeField] private string _text = default;
        // 画像（アイコン・立ち絵） 
        [SerializeField] private Sprite _sprite = default;
        // 音声
        [SerializeField] private AudioClip _voice = default;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        public Sprite Sprite
        {
            get { return _sprite; }
            set { _sprite = value; }
        }
        public AudioClip Voice
        {
            get { return _voice; }
            set { _voice = value; }
        }
    }
}

