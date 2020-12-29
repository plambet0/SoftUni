using CounterStrike.Models.Maps.Contracts;
using CounterStrike.Models.Players;
using CounterStrike.Models.Players.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CounterStrike.Models.Maps
{
    public class Map : IMap
    {
        private List<IPlayer> terorists;
        private List<IPlayer> counterTerrorists;

        public Map()
        {
            this.terorists = new List<IPlayer>();
            this.counterTerrorists = new List<IPlayer>();
        }

        public string Start(ICollection<IPlayer> players)
        {
            SeparateTeams(players);
            while (IsTeamAlive(terorists) && IsTeamAlive(counterTerrorists))
            {
                AttackTeam(terorists, counterTerrorists);
                AttackTeam(counterTerrorists, terorists);
            }
            if (!IsTeamAlive(counterTerrorists))
            {
                return "Terrorist wins!";
                
            }
            else if(!IsTeamAlive(terorists))
            {
                return "Counter Terrorist wins!";
            }
            return "Something horrible happened";
        }
        private bool IsTeamAlive(ICollection<IPlayer> players)
        {
            return players.Any(p => p.IsAlive);
        }
        private void AttackTeam(List<IPlayer> attackingTeam, List<IPlayer> defendingTeam)
        {
            foreach (var attacker in attackingTeam)
            {
                //if (!attacker.IsAlive) 
                //{
                //    continue;
                //}
                foreach (var defender in defendingTeam)
                {
                    if (!defender.IsAlive)
                    {
                        continue;
                    }
                    defender.TakeDamage(attacker.Gun.Fire());
                }
            }
        }
        private void SeparateTeams(ICollection<IPlayer> players)
        {
            foreach (Player player in players)
            {
                if (player is Terrorist)
                {
                    terorists.Add(player);
                }
                else if (player is CounterTerrorist)
                {
                    counterTerrorists.Add(player);
                }
            }
            
        }
    }
}
