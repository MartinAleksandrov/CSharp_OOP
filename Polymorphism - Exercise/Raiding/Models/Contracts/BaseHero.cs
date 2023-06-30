namespace Raiding.Models.Contracts
{
    using Interfaces;
    public abstract class BaseHero : IBaseHero
    {
        protected BaseHero(string name,int currPower)
        {
            Name = name;
            TotalHeroPower = currPower;
        }

        public string Name { get; private set; }

        public  long TotalHeroPower { get; private set; }

        public virtual void CastAbility()
        {
            Console.WriteLine($"{this.GetType().Name} - {Name} healed for {TotalHeroPower}");
        }
    }
}
