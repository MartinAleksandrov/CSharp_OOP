﻿namespace BorderControl.Models
{
    using Interfaces;
    public class Robot : IRobot
    {
        public Robot(string model, string id)
        {
            Model = model;
            RobotId = id;
        }

        public string Model { get; private set; }

        public string RobotId { get; private set; }
    }
}
