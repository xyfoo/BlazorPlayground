using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Data
{
    public class ShowAvailability
    {
        public Guid Id { get; } = Guid.NewGuid();
        public Venue Venue { get; set; }
        public DateTime DateTime { get; set; }
        public uint MaxAttendeesCount { get; set; } = 1;
        public List<Attendee> Attendees { get; set; } = new List<Attendee>();
        public bool IsAvailable => Attendees.Count < this.MaxAttendeesCount;
    }

    public class Venue
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
    }

    public class Attendee
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Phone { get; set; }
    }

    public class ShowAvailabilityService
    {
        public readonly List<Venue> Venues = new List<Venue>();
        public readonly List<ShowAvailability> ShowAvailabilities = new List<ShowAvailability>();

        public ShowAvailabilityService()
        {
            for (int x = 1; x <= 5; x++)
            {
                Venue v = new Venue { Name = $"Venue {x}" };
                Venues.Add(v);

                for (int d = 1; d <= 10; d++)
                {
                    ShowAvailability sa = new ShowAvailability
                    {
                        Venue = v,
                        DateTime = DateTime.Now.AddDays(d),
                    };
                    ShowAvailabilities.Add(sa);
                }
            }
        }

        public ShowAvailability[] GetAvailability()
        {
            return ShowAvailabilities.ToArray();
        }

        public bool Book(ShowAvailability sa, Attendee att)
        {
            sa = ShowAvailabilities.Where(x => x.Id == sa.Id).First();
            if (sa.IsAvailable)
            {
                sa.Attendees.Add(att);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
