using CounterStrike.Models.Guns.Contracts;
using CounterStrike.Models.Players.Contracts;
using CounterStrike.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CounterStrike.Models.Players
{
    public abstract class Player : IPlayer
    {
        private string username;
        private int health;
        private int armor;
        private IGun gun;

        public Player(string username, int health, int armor, IGun gun)
        {
            this.Username = username;
            this.Health = health;
            this.Armor = armor;
            this.Gun = gun;
        }
        
        public string Username
        {
            get
            {
                return this.username;
            }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlayerName);
                }
                else
                {
                    this.username = value;
                }

            }
        }

        public int Health
        {
            get
            {
                return this.health;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlayerHealth);
                }
                else
                {
                    this.health = value;
                }

            }
        }


        public int Armor
        {
            get
            {
                return this.armor;
            }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlayerArmor);
                }
                else
                {
                    this.armor = value;
                }

            }
        }


        public IGun Gun
        {
            get
            {
                return this.gun;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidGun);
                }
                else
                {
                    this.gun = value;
                }

            }
        }

        //bool IPlayer.IsAlive => IsAlive;

        public bool IsAlive
        {
            get
            {
                return this.Health > 0;
            }
            
        }


        public void TakeDamage(int points)
        {

            if (this.armor - points > 0)
            {
                this.armor -= points;
                return;
            }
            else if (this.armor > 0)
            {
                points -= this.armor;
                armor = 0;
            }
            this.health -= points;
            if (this.health < 0)
            {
                this.health = 0;
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name}: {this.username}");
            sb.AppendLine($"--Health: {this.health}");
            sb.AppendLine($"--Armor: {this.armor}");
            sb.AppendLine($"--Gun: {this.Gun.Name}");
            return sb.ToString().TrimEnd();
        }
    }
}
