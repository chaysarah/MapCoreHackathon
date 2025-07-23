using MapCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCTester.Managers.MapWorld
{
    public interface IUserSelectServerLayers
    {
        void AfterUserSelectServerLayers(string urlServer, DNEWebMapServiceType eWebMapServiceType);
    }
}
