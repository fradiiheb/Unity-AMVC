using System;
using System.Collections.Generic;
using AMVC.Core;
using AMVC.Views.Main.Rocket;
using UnityEngine;
using Application = AMVC.Core.Application;
namespace AMVC.Systems.Main
{
    public class RocketsSystem : AppSystem
    {
        [SerializeField] private string rocketItemName;
        private List<RocketItem> _items;
        private bool _isGenerated;
        private RocketItem _selectedRocket;
       private RocketPanel _p;

        private RocketPanel _panel
        {
            get
            {
                if (_p == null) _p = GetPanel<RocketPanel>();
                return _p;
            }
        }
         protected override void ReleaseReferences()
        {
            base.ReleaseReferences();
            _items = null;
            _p = null;
            RocketItem.OnSelect -= OnSelectRocketItem;
        }
        public override void Initialize(AppController controller, Application app)
        {
            base.Initialize(controller, app);
            RocketItem.OnSelect += OnSelectRocketItem;
        }
        private void OnSelectRocketItem(RocketItem missionItem)
        {
            
            _selectedRocket = missionItem;
            _panel.Show(_selectedRocket.model);
        }

         public void Generate()
        {
            if(!_isGenerated) Clear();
            var pool = GetSystem<PoolSystem>();
            
            foreach (var rocketModel in application.models.rocket)
            {
                var item = pool.Spawn<RocketItem>(this.rocketItemName);
                item.Initialize(this.application);
                item.BindData(rocketModel);
                _items.Add(item);
                _panel.AddItem(item);
            }
            
            _isGenerated = true;
            
        }
        
     public void Clear()
        {
            if (_items == null)
            {
                _items = new List<RocketItem>();
                return;
            }

            foreach (var item in _items)
                item.Remove();
            _items.Clear();
        }

       /* public Vector2 GetItemPosition(int index)
        {
            if(index >= this._items.Count)
                throw new Exception($"index {index} is out of range {_items.Count}");
            return _items[index].Position();
        }*/
    }
}
