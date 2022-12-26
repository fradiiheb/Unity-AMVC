using System;
using AMVC.Core;
using AMVC.Models;
using AMVC.Systems.Main;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Application = AMVC.Core.Application;

namespace AMVC.Views.Main.Rocket
{
    public class RocketPanel : AppPanel
    {
        [Header("Internal References")]
        [SerializeField] private Button backBtn;
        [SerializeField] private RocketPopUp popup; //
        [SerializeField] public RectTransform itemsHolder;

        [Header("Settings")] 
        public float tweenSpeed;
        public Ease showEase;
        public Ease hideEase;


        protected override void ReleaseReferences()
        {
            base.ReleaseReferences();
            backBtn = null;
            itemsHolder = null;
        }
         public override void Initialize(AppView view, Application app)
        {
            base.Initialize(view, app);
            backBtn.onClick.AddListener(BackToMainApp);
            popup.Initialize(this);
            
        }
        private void BackToMainApp()
        {
            ClosePanel(() =>
            {
                GetPanel<MenuPanel>().OpenPanel();
            });
        }
        public void AddItem(RocketItem item)
        {
            item.transform.SetParent(this.itemsHolder);
        }
        public void Show(RocketModel roketModel)
        {
            popup.Show(roketModel);
        }
    }
}
