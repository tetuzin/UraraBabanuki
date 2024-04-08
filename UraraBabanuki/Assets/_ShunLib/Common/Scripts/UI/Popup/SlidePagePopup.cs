using System.Collections.Generic;
using UnityEngine;

using ShunLib.Btn.Arrow;
using ShunLib.UI.Scroll;
using ShunLib.UI.PageIcon;

namespace ShunLib.Popup.Slide
{
    public class SlidePagePopup : BasePopup
    {
        // ---------- 定数宣言 ----------
        // ---------- ゲームオブジェクト参照変数宣言 ----------

        [Header("ScrollRect")]
        [SerializeField] protected CommonScrollRect scrollRect = default;

        [Header("Arrowボタン")]
        [SerializeField] protected ArrowButton arrowButton = default;

        [Header("ページ数アイコン")]
        [SerializeField] protected Indicator indicator = default;

        [Header("ページオブジェクト")]
        [SerializeField] protected List<GameObject> pageObjects = default;

        // ---------- プレハブ ----------
        // ---------- プロパティ ----------
        // ---------- クラス変数宣言 ----------
        // ---------- インスタンス変数宣言 ----------

        protected int curPage = default;

        // ---------- Unity組込関数 ----------
        // ---------- Public関数 ----------
        // ---------- Private関数 ----------

        // ArrowButtonの表示設定
        private void SetActiveArrowButton(int setPage)
        {
            if (setPage == 0)
            {
                arrowButton.SetActiveLeftButton(false);
                arrowButton.SetActiveRightButton(true);
            }
            else if(setPage == pageObjects.Count - 1)
            {
                arrowButton.SetActiveLeftButton(true);
                arrowButton.SetActiveRightButton(false);
            }
            else
            {
                arrowButton.SetActiveLeftButton(true);
                arrowButton.SetActiveRightButton(true);
            }
        }

        // ---------- protected関数 ---------

        // 初期化
        protected override void Initialize()
        {
            scrollRect.Initialize();
            List<Vector2> posList = new List<Vector2>();
            Vector2 pos = scrollRect.content.anchoredPosition;
            posList.Add(pos);
            for (int i = 1; i < pageObjects.Count; i++)
            {
                pos = new Vector2(pos.x - scrollRect.viewport.sizeDelta.x, pos.y);
                posList.Add(pos);
            }
            scrollRect.InitializePageScroll(posList);
            curPage = 0;
            arrowButton.SetLeftButtonEvent(ScrollPrevPage);
            arrowButton.SetRightButtonEvent(ScrollNextPage);
            SetActiveArrowButton(curPage);
        }

        // 次のページへスクロール
        protected virtual void ScrollNextPage()
        {
            if (scrollRect.IsMove) return;

            curPage++;
            SetActiveArrowButton(curPage);
            scrollRect.MovePage(curPage);
        }

        // 前のページへスクロール
        protected virtual void ScrollPrevPage()
        {
            if (scrollRect.IsMove) return;

            curPage--;
            SetActiveArrowButton(curPage);
            scrollRect.MovePage(curPage);
        }
    }
}


