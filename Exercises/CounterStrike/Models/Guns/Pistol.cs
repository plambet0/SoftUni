namespace CounterStrike.Models.Guns
{
    public class Pistol : Gun
    {
        private const int firedBulletsPerFire = 1;
        
        public Pistol(string name, int bulletsCount)
            : base(name, bulletsCount)
        {
        }

        public override int Fire()
        {
            if (this.BulletsCount - firedBulletsPerFire >= 0)
            {
                this.BulletsCount -= firedBulletsPerFire;
                return firedBulletsPerFire;
            }
            else
            {
                int resultingBullets = this.BulletsCount;
                this.BulletsCount = 0;
                return resultingBullets;
            }
            
        }
    }
}
