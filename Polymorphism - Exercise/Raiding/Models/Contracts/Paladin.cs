namespace Raiding.Models.Contracts
{
    public class Paladin : BaseHero
    {
        private const int PaladinPower = 100;
        public Paladin(string name) : base(name,PaladinPower)
        {
        }

        public override void CastAbility()
        {
            base.CastAbility();
        }
    }
}
