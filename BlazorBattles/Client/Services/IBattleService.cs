using BlazorBattles.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBattles.Client.Services
{
    public interface IBattleService
    {
        Task<BattleResult> StartBattle(int opponentId);
    }
}
