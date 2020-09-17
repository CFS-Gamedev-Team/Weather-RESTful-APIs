using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    [Serializable]
    class WeatherInfo
    {
        public int id;
        public string name;
        public List<Weather> weather;
    }
}
