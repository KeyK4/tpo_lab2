using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tpo_lab2.domain
{
    public class Cinema
    {
        public string name { get;}
        public List<Movie> movies { get; }
        public uint maxMoviesNumber { get; }
        public CinemaState state { get; private set; }

        public Cinema(string name, uint maxMoviesNumber)
        {
            this.name = name;
            this.maxMoviesNumber = maxMoviesNumber;
            movies = new List<Movie>();
            state = CinemaState.Open;
        }

        public void addMovie(Movie movie)
        {
            if (movies.Count < maxMoviesNumber)
            {
                movies.Add(movie);
            }
            else
            {
                throw new Exception("Превышено максимальное количество фильмов в кинотеатре");
            }
        }

        public void setOpen()
        {
            if (state == CinemaState.Closed)
            {
                state = CinemaState.Open;
            }
                else throw new WrongStateException("Кинотеатр уже открыт");
        }

        public void setClosed()
        {
            if (state == CinemaState.Open)
            {
                state = CinemaState.Closed;
            }
            else throw new WrongStateException("Кинотеатр уже закрыт");
        }

        public class WrongStateException : Exception
        {
            public WrongStateException(string message) : base(message)
            {
            }
        }
        
        public enum CinemaState
        {
            Open,
            Closed
        }
    }
}
