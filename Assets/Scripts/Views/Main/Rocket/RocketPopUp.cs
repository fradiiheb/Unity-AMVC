using AMVC.Core;
using AMVC.Models;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace AMVC.Views.Main.Rocket
{
    public class RocketPopUp : BaseMonoBehaviour
    {
        [SerializeField] private Text titleTxt;
        [SerializeField] private Text descriptionTxt;
        [SerializeField] private Button backBtn;

        [Header("Movement Settings")] 
        [SerializeField] private Vector2 startPos;
        [SerializeField] private float inScreenYPos;
        [SerializeField] private float outScreenYPos;

         public bool onScreen;
        [SerializeField]private CanvasGroup _cGroup;
        private RectTransform _rt;
        private RocketPanel _parentPanel;
        private RectTransform _rectTransform
        {
            get
            {
                if (_rt == null) _rt = GetComponent<RectTransform>();
                return _rt;
            }
        }

        protected override void ReleaseReferences()
        {
            _parentPanel = null;
            backBtn = null;
            _rt = null;
            titleTxt = null;
            _cGroup = null;
            descriptionTxt = null;
        }
         public void Initialize(RocketPanel parentPanel)
        {
            _cGroup = GetComponent<CanvasGroup>();
            _parentPanel = parentPanel;
            _rectTransform.anchoredPosition = startPos;
            backBtn.onClick.AddListener(Hide);
            onScreen = false;
            Debug.Log("Initialize");
        }
    public void Show(RocketModel model)
        {
            onScreen = true;
            //HideItemHolder();
            titleTxt.text = model.rocket_name;
            descriptionTxt.text = model.description;
            //init animation
            _cGroup.alpha = 0;
            
            _rectTransform.anchoredPosition = startPos;
            //do animation
            _cGroup.DOFade(1, _parentPanel.tweenSpeed);
            //_rectTransform.DOScale(Vector3.one, _parentPanel.tweenSpeed).SetEase(_parentPanel.showEase);;
            _rectTransform.DOAnchorPosY(inScreenYPos, _parentPanel.tweenSpeed).SetEase(_parentPanel.showEase);
        }

        public void Hide()
        {
            onScreen = false;
           // ShowItemHolder();
            //init animation
            _rectTransform.anchoredPosition = new Vector2(0, inScreenYPos);
            //do animation
            _cGroup.DOFade(0, _parentPanel.tweenSpeed);
            _rectTransform.DOAnchorPosY(outScreenYPos, _parentPanel.tweenSpeed).SetEase(_parentPanel.hideEase).OnComplete(
                () =>
                {
                    _rectTransform.anchoredPosition = startPos;
                });
        }

        public void HideItemHolder()
        {
            
            //init animation
            _parentPanel.itemsHolder.anchoredPosition = new Vector2(0, inScreenYPos);
            //do animation
            _cGroup.DOFade(0, _parentPanel.tweenSpeed);
            _rectTransform.DOAnchorPosY(outScreenYPos, _parentPanel.tweenSpeed).SetEase(_parentPanel.hideEase).OnComplete(
                () =>
                {
                    _parentPanel.itemsHolder.anchoredPosition = startPos;
                });
        }
        public void ShowItemHolder()
        {
            
            _parentPanel.itemsHolder.anchoredPosition = new Vector2(0, outScreenYPos);
            //do animation
            _cGroup.DOFade(0, _parentPanel.tweenSpeed);
            _rectTransform.DOAnchorPosY(outScreenYPos, _parentPanel.tweenSpeed).SetEase(_parentPanel.hideEase).OnComplete(
                () =>
                {
                    _parentPanel.itemsHolder.anchoredPosition = startPos;
                });
        }
    }
}
