using CounterStrike.Core.Contracts;
using CounterStrike.Models.Guns;
using CounterStrike.Models.Guns.Contracts;
using CounterStrike.Models.Maps.Contracts;
using CounterStrike.Models.Players;
using CounterStrike.Models.Players.Contracts;
using CounterStrike.Repositories;
using CounterStrike.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using CounterStrike.Models.Maps;

namespace CounterStrike.Core
{
    public class Controller : IController
    {
        private GunRepository guns;
        private PlayerRepository players;
        private IMap map;

        public Controller()
        {
            guns = new GunRepository();
            players = new PlayerRepository();
            map = new Map();
        }
        

        public string AddGun(string type, string name, int bulletsCount)
        {
            IGun gun;
            
            if (type == "Pistol")
            {
                gun = new Pistol(name, bulletsCount);
            }
            else if (type == "Rifle")
            {
                gun = new Rifle(name, bulletsCount);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidGunType);
            }
            guns.Add(gun);
            return  $"Successfully added gun {name}.";


        }

        public string AddPlayer(string type, string username, int health, int armor, string gunName)
        {
            IPlayer player;
            IGun gun = guns.FindByName(gunName);
            
            if (gun == null)
            {
                throw new ArgumentException(ExceptionMessages.GunCannotBeFound);
            }
            if (type == "Terrorist")
            {
                player = new Terrorist(username, health, armor, gun);
                
            }
            else if (type == "CounterTerrorist")
            {
                player = new CounterTerrorist(username, health, armor, gun);
                
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidPlayerType);
            }
            players.Add(player);
            return $"Successfully added player {username}.";
        }

        public string Report()
        {
            
            StringBuilder sb = new StringBuilder();
            var sortedPlayers = players.Models.OrderBy(p => p.GetType().Name).ThenByDescending(p => p.Health).ThenBy(p => p.Username);
            foreach (Player player in sortedPlayers)
            {
                
                sb.AppendLine(player.ToString());
                
            }
            return sb.ToString().TrimEnd();
            
        }
        
        public string StartGame()
        {
            return map.Start(players.Models.ToList());
        }
    }
}
