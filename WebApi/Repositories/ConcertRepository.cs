using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class ConcertRepository : IRepository<Concert>
    {
        private List<Concert> concerts = new List<Concert>()
        {
            new Concert { Id = 1, LastUpdated = DateTime.Now, Artist = "Bruce Springsteen", Date = new DateTime(1985, 7, 3), Venue = "Wembley Stadium" },
            new Concert { Id = 2, LastUpdated = DateTime.Now, Artist = "Bruce Springsteen", Date = new DateTime(1988, 6, 25), Venue= "Wembley Stadium" },
            new Concert { Id = 3, LastUpdated = DateTime.Now, Artist = "Bruce Springsteen", Date = new DateTime(1992, 7, 13), Venue= "Wembley Arena" },
            new Concert { Id = 4, LastUpdated = DateTime.Now, Artist = "Bruce Springsteen", Date = new DateTime(1993, 5, 22), Venue= "National Bowl, Milton Keynes" }
        };

        private int nextId = 5;

        public int Count()
        {
            return concerts.Count;
        }

        public void Delete(object id)
        {
            concerts.Remove((Concert)id);
        }

        public void Delete(Concert entityToDelete)
        {
            concerts.Remove(entityToDelete);
        }

        public Concert GetById(object id)
        {
            return concerts.Find(x => x.Id == (int)id);
        }

        public int Insert(Concert entity)
        {
            entity.Id = this.nextId++;
            concerts.Add(entity);
            return entity.Id;
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(Concert entity)
        {
            throw new NotImplementedException();
        }
    }
}