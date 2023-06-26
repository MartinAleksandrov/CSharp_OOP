namespace Military_Elite.Classes
{
    using Models.Interfaces;
    public class Repair : IRepair
    {
        public Repair(string partName, int hours) 
        {
            PartName = partName;
            HoursWorked = hours;
        }

        public string PartName { get; private set; }

        public int HoursWorked { get; private set; }

        public override string ToString()
        {
            return $"  Part Name: {PartName} Hours Worked: {HoursWorked}";
        }
    }
}
