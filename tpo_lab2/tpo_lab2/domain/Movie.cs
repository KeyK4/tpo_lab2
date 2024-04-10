using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace tpo_lab2.domain
{
    public class Movie
    {
        public Scenario scenario { get;}
        public Crew? crew { get; set; }
        public int? duration { get; private set; }

        public enum MovieState
        {
            NotStarted,
            AtWork,
            Finished
        }

        public MovieState state { get; private set; }

        public Movie(Scenario scenario)
        {
            this.scenario = scenario;
            state = MovieState.NotStarted;
        }
        
        public void film()
        {
            if (crew == null)
            {
                throw new MissingFieldException("Crew is null!");
            }
            
            foreach (var crewMember in crew.crewMembers)
            {
                crewMember.setBusy();
            }
            foreach (var person2actor in crew.roles)
            {
                var actor = person2actor.Value;
                actor.setBusy();
            }
            crew.director.setBusy();
            state = MovieState.AtWork;
            Thread.Sleep(6000);
            state = MovieState.Finished;
            duration = Random.Shared.Next(20, 180);
            
            foreach (var crewMember in crew.crewMembers)
            {
                crewMember.setFree();
            }
            foreach (var person2actor in crew.roles)
            {
                var actor = person2actor.Value;
                actor.setFree();
            }
            crew.director.setFree();
        }
    }
}
