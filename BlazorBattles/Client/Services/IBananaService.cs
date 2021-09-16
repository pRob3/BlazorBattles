using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBattles.Client.Services
{
    public interface IBananaService
    {
        event Action OnChange;

        int Bananas { get; set; }
        void EatBananas(int amount);

        void AddBananas(int amount);
    }
}
