using System.Collections.Generic;
using AMVC.Core;

namespace AMVC.Models
{
     [System.Serializable]
    public class RocketModel 
    
    {
        public string rocket_name;
        public string rocket_id;
        public string description;
        public string country;
        public string company;
        public List<string> flickr_images ;
    }
}
