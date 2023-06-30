namespace Raiding.Models.Contracts
{
    public class Druid : BaseHero
    {
        private const int DruidPower = 80;
        public Druid(string name) : base(name, DruidPower)
        {
        }

        public override void CastAbility()
        {
            base.CastAbility();
        }
    }
}
