using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpo_lab2.domain
{
    public class Scenario
    {
        public Scenario(string name)
        {
            this.name = name;
            theme = null;
            genre = null;
            screenwriter = null;
            state = ScenarioState.NotReady;
        }
        
        private Scenario(){}
        
        public string name { get; private set; }
        public string? theme { get; set; }
        public string? genre { get; set; }
        public Screenwriter? screenwriter { get; set; }

        public List<Personage>? personages { get; private set; }

        public enum ScenarioState
        {
            NotReady,
            Finished
        }

        public ScenarioState state { get; private set; }

        public void write()
        {
            if (theme == null)
            {
                throw new MissingFieldException("Theme is null!");
            }
            if (genre == null)
            {
                throw new MissingFieldException("Genre is null!");
            }
            if (screenwriter == null)
            {
                throw new MissingFieldException("Screenwriter is null!");
            }
            screenwriter.setBusy();
            personages = generatePersonages();
            Thread.Sleep(5000);
            state = ScenarioState.Finished;
            screenwriter.setFree();
        }

        public List<Personage> generatePersonages()
        {
            List<Personage> personages = new List<Personage>();
            int countPersonages = Random.Shared.Next(30, 100);
            for (int i = 0; i < countPersonages; i++)
            {
                personages.Add(new Personage(Person.getRandomPerson()));
            }
            return personages;
        }
    }
}
