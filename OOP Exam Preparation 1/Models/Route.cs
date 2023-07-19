using EDriveRent.Models.Contracts;
using EDriveRent.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Models
{
    public class Route : IRoute
    {
        private bool isLocked;
        public Route(string startPoint, string endPoint, double length, int routeId)
        {
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
            this.Length = length;
            this.routeId = routeId;
            this.isLocked = false;
        }

        private string startPoint;
        public string StartPoint
        {
            get { return startPoint; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.StartPointNull);
                }
                startPoint = value;
            }
        }


        private string endPoint;
        public string EndPoint
        {
            get { return endPoint; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.EndPointNull);
                }
                endPoint = value;
            }
        }


        private double length;
        public double Length
        {
            get { return length; }
            private set
            {
                if (value < 1)
                {
                    throw new ArgumentException(ExceptionMessages.RouteLengthLessThanOne);
                }
                length = value;
            }
        }


        private int routeId;
        public int RouteId => this.routeId;

        public bool IsLocked =>isLocked;

        public void LockRoute()
        {
            isLocked = true;
        }
    }
}
