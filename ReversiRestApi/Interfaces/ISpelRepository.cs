using ReversiRestApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReversiRestApi.Interfaces
{
    public interface ISpelRepository
    {
        void AddSpel(Spel spel);
        public List<Spel> GetSpellen();
        Spel GetSpel(string spelToken);        // ...    
    }
}
