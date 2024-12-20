using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team_Manager
{
    public class LocalDbServices
    {
        public const string DB_NAME = "demo_local_db.db3";
        private readonly SQLiteAsyncConnection _connection;
        public LocalDbServices()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Zawodnicy>();
            _connection.CreateTableAsync<Wydarzenia>();
        }

        //zwraca listę wszystkich zawodników
        public async Task<List<Zawodnicy>> GetZawodnicy()
        {
            return await _connection.Table<Zawodnicy>().ToListAsync();
        }

        //zwaraca zawodnika po id
        public async Task<Zawodnicy> GetZawodnikById(int id)
        {
            return await _connection.Table<Zawodnicy>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //dodaje zawodnika
        public async Task CreateZawodnik(Zawodnicy zawodnik)
        {
            await _connection.InsertAsync(zawodnik);
        }

        //aktualizuje zawodnika
        public async Task UpdateZawodnik(Zawodnicy zawodnik)
        {
            await _connection.UpdateAsync(zawodnik);
        }

        //usuwa zawodnika
        public async Task DeleteZawodnik(Zawodnicy zawodnik)
        {
            await _connection.DeleteAsync(zawodnik);
        }

        //zwraca listę wszystkich wydarzeń 
        
        public async Task<List<Wydarzenia>> GetWydarzenia()
        {
            return await _connection.Table<Wydarzenia>().ToListAsync();
        }

        //zwraca wydarzenie po id 
        public async Task<Wydarzenia> GetWydarzenieById(int id)
        {
            return await _connection.Table<Wydarzenia>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        //tworzy wydarzenie
        public async Task CreateWydarzenie(Wydarzenia wydarzenie)
        {
            await _connection.InsertAsync(wydarzenie);
        }

        //aktualizuje wydarzenie
        public async Task UpdateWydarzenie(Wydarzenia wydarzenie)
        {
            await _connection.UpdateAsync(wydarzenie);
        }

        //usuwa wydarzenie 
        public async Task DeleteWydarzenie(Wydarzenia wydarzenie)
        {
            await _connection.DeleteAsync(wydarzenie);
        }




    }
}
