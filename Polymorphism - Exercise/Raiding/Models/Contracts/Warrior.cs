namespace Raiding.Models.Contracts
{
    public class Warrior : BaseHero
    {
        private const int WarriorPower = 100;

        public Warrior(string name) :
            base(name, WarriorPower)
        {
        }
        public override void CastAbility()
        {
            Console.WriteLine($"{GetType().Name} - {Name} hit for {WarriorPower} damage");
        }
    }
}
