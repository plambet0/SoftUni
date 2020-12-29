namespace CounterStrike.Models.Guns.Contracts
{
    public class Rifle : Gun
    {
        private const int firedBulletsPerFire = 10;

        public Rifle(string name, int bulletsCount)
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
