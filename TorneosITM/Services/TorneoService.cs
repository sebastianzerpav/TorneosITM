using Microsoft.EntityFrameworkCore;
using TorneosITM.Data;
using TorneosITM.Data.Models;

namespace TorneosITM.Services
{
    public class TorneoService : ITorneoService
    {
        //CRUD Torneos
        public readonly AppDbContext context;
        public TorneoService(AppDbContext context) { 
            this.context = context;
        }

        public async Task<bool> Insert(Torneo torneo) {
            try
            {
                context.Torneos.Add(torneo);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> Update(int idTorneo, Torneo torneo) {
            try
            {
                Torneo? foundTorneo = await context.Torneos.FindAsync(idTorneo);
                if (foundTorneo == null) {
                    return false;
                }
                else
                {
                    context.Entry(foundTorneo).CurrentValues.SetValues(torneo);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<bool> Delete(int idTorneo)
        {
            try
            {
                Torneo? foundTorneo = await context.Torneos.FindAsync(idTorneo);
                if (foundTorneo == null) { return false; }
                else
                {
                    context.Torneos.Remove(foundTorneo);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task<IEnumerable<Torneo>> GetByType(string type) {
            IQueryable<Torneo> torneos = context.Torneos.Where(torneo => torneo.TipoTorneo == type);
            return await torneos.ToListAsync();
        }

        public async Task<IEnumerable<Torneo>> GetByName(string name)
        {
            IQueryable<Torneo> torneos = context.Torneos.Where(torneo => torneo.NombreTorneo.Contains(name));
            return await torneos.ToListAsync();
        }

        public async Task<IEnumerable<Torneo>> GetByDate(DateOnly date)
        {
            IQueryable<Torneo> torneos = context.Torneos.Where(torneo => torneo.FechaTorneo == date);
            return await torneos.ToListAsync();
        }

    }

    public interface ITorneoService
    {
        Task<bool> Insert(Torneo torneo);
        Task<bool> Update(int idTorneo, Torneo torneo);
        Task<bool> Delete(int idTorneo);
        Task<IEnumerable<Torneo>> GetByType(string type);
        Task<IEnumerable<Torneo>> GetByName(string name);
        Task<IEnumerable<Torneo>> GetByDate(DateOnly date);
    }
}
