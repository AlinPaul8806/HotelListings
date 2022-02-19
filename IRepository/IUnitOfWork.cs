using HotelListing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.IRepository
{
    // act like a register for each variation of the generic repository relative to the class<T>
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Country> Countries { get;}

        IGenericRepository<Hotel> Hotels { get;}

        Task Save();

    }
}
