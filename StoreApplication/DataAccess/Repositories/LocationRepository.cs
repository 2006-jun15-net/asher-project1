using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using StoreApplication.Library;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
    public class LocationRepository : ILocationRepository
    {
        private Project0StoreContext context = null;
        private DbSet<Location> table = null;

        public LocationRepository()
        {
            context = new Project0StoreContext();
            table = context.Set<Location>();
        }
        public LocationRepository(Project0StoreContext context)
        {
            this.context = context;
            table = this.context.Set<Location>();
        }

        public IEnumerable<StoreApplication.Library.Models.Location> GetAll()
        {
            var locations = context.Location.ToList();
            return locations.Select(Mapper.MapLocationEntities);
        }

        public StoreApplication.Library.Models.Location GetByID(object id)
        {
            return Mapper.MapLocationEntities(context.Location.Find(id));
        }
    }
}
