using StoreApplication.Library.Models;
using System.Collections.Generic;

namespace StoreApplication.Library
{
    public interface ILocationRepository
    {
        Location GetByID(object id);
        IEnumerable<Location> GetAll();
    }
}
