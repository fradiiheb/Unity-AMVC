using System;
using System.Collections;
using System.Windows.Input;
using AMVC.Core;
using AMVC.Models;
using AMVC.Systems;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Application = AMVC.Core.Application;
namespace AMVC.Views.Main.Rocket
{
    public class RocketItem : BaseMonoBehaviour, IPoolItem
    {
        public static event Action<RocketItem> OnSelect;
        public RocketModel model { get; private set; }
        [SerializeField] private Text titleTxt;
        [SerializeField] private Text countryTxt;
        [SerializeField] private Text companyTxt;
        [SerializeField] private Button _Detailsbutton;
        [SerializeField] private RawImage _Image;
        private Application _application;
        private bool _isInitialized;
        

        protected override void ReleaseReferences()
        {
            titleTxt = null;
            countryTxt = null;
            companyTxt=null;
            _application = null;
            model = null;
        }
        public void Initialize(Application app)
        {
            if (_isInitialized) return;
            _isInitialized = true;
            _application = app;
            _Detailsbutton.onClick.AddListener(() =>
           {
               OnSelect?.Invoke(this);
           });
        }
        public void BindData(RocketModel rmodel)
        {
            this.model = rmodel;
            this.titleTxt.text = rmodel.rocket_name;
            this.countryTxt.text = rmodel.country;
            this.companyTxt.text=rmodel.company;
            StartCoroutine(DownloadImage(rmodel.flickr_images[0]));
        }
        public void Remove()
        {
            _application.GetSystem<PoolSystem>().Despawn(this.transform);
        }
        IEnumerator DownloadImage(string MediaUrl)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                //if (request.isNetworkError || request.isHttpError)
                Debug.Log(request.error);
            else
                _Image.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        }
       
    }
}
