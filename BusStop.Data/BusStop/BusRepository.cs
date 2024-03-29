using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusStop.Core.BusStop;
using Microsoft.EntityFrameworkCore;

namespace BusStop.Data.BusStop
{
    public class BusRepository : IBusRepository
    {
        private readonly IMapper _mapper;
        private readonly BusStopContext _context;
        public BusRepository(IMapper mapper,
            BusStopContext busContext)
        {
            _mapper = mapper;
            _context = busContext;
        }
        public async Task<List<Core.BusStop.BusStop>> GetAsync()
        {
            var entities = await _context.BusStations.ToListAsync();
            return _mapper.Map<List<Core.BusStop.BusStop>>(entities);
        }

        public async Task<Core.BusStop.BusStop> GetByIdAsync(int Id)
        {
            var entitie = await _context.BusStations.FirstOrDefaultAsync(x => x.Id == Id);
            return _mapper.Map <Core.BusStop.BusStop> (entitie);
        }

        public async Task<Core.BusStop.BusStop> UpdateAsync(int Id, int count)
        {
            var entetie = await _context.BusStations.FirstOrDefaultAsync(x => x.Id == Id);
            entetie.CountOfBuses = count;
            var result =  _context.Update(entetie);
            await _context.SaveChangesAsync();
            return _mapper.Map<Core.BusStop.BusStop>(result.Entity);
        }

        public async Task Remove(int Id)
        {
            var entetie = await _context.BusStations.FirstOrDefaultAsync(x => x.Id == Id);
            _context.BusStations.Remove(entetie);
            await _context.SaveChangesAsync();
        }

        public async Task<Core.BusStop.BusStop> AddAsync(Core.BusStop.BusStop busStop)
        {
            var entity = _mapper.Map<BusDto>(busStop);
            var result = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<Core.BusStop.BusStop>(result.Entity);
        }
    }
}