namespace Military_Elite.Classes
{
    using Military_Elite.Enums;
    using Models.Interfaces;
    public class Mission : IMissions
    {
        private MissionState state;
        public Mission( string codeName, string state) 
        {
            CodeName = codeName;
            State = state;
        }

        public string CodeName { get; private set; }

        public string State
        {
            get => state.ToString();
            private set
            {
                MissionState state;

                if (!Enum.TryParse(value, out state))
                {
                    throw new ArgumentException();
                }
                this.state = state;
            }
        }

        public void CompleteMission()
        {
            state = MissionState.Finished;
        }

        public override string ToString()
        {
            return $"  Code Name: {CodeName} State: {State}";
        }
    }
}
