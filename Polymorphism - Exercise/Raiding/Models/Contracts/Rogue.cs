namespace Raiding.Models.Contracts
{
    public class Rogue : BaseHero
    {
        private const int RoguePower = 80;

        public Rogue(string name) :
            base(name, RoguePower)
        {
        }

        public override void CastAbility()
        {
            Console.WriteLine($"{GetType().Name} - {Name} hit for {RoguePower} damage");
        }
    }
}
