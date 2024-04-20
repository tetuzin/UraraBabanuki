using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
using ShunLib.Utils.Editor;
#endif

namespace UraraBabanuki.Scripts
{
    [CreateAssetMenu(fileName = "CardDesign", menuName = "ScriptableObjects/UraraBabanuki/CardDesign")]
    public class CardDesignScriptableObject : ScriptableObject
    {
        [Header("カード画像")] public List<Sprite> CardImageList = default;
        [Header("カード画像背面")] public Sprite CardBackImage = default;

#if UNITY_EDITOR
        [MenuItem ("Assets/Create/ScriptableObject/CardDesign")]
        static void CardDesignScriptableObjectScriptable()
        {
            CardDesignScriptableObject createSceneTemplate = CreateInstance<CardDesignScriptableObject> ();
            string path = AssetDatabase.GenerateUniqueAssetPath(EditorUtils.GetPath() + "/" + "New CardDesignScriptableObject.asset");
            AssetDatabase.CreateAsset(createSceneTemplate, path);
            AssetDatabase.Refresh();
        }
#endif
    }
}

