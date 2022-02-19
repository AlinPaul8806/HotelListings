/*if new entities or tables will be added in the future, 
 * you will only have to maintain this list and to add the accordingly*/

using HotelListing.Data;
using HotelListing.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        //fields:
        private readonly DatabaseContext _context;

        private IGenericRepository<Country> _countries;

        private IGenericRepository<Hotel> _hotels;

        //constructor:
        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        // ??= ---> if this is null, what should I do?
        public IGenericRepository<Country> Countries => _countries ??= new GenericRepository<Country>(_context);

        public IGenericRepository<Hotel> Hotels => _hotels ??= new GenericRepository<Hotel>(_context);

        public void Dispose()
        {
            // please dispose of the context
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
