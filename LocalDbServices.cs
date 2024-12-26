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
            _connection.CreateTableAsync<Harmonogram>();
            _connection.CreateTableAsync<Frekwencja>();
            _connection.CreateTableAsync<OsiagnieciaDruzyny>();
            _connection.CreateTableAsync<OsiagnieciaZawodnika>();
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

        // Zwraca listę harmonogramów
        public async Task<List<Harmonogram>> GetHarmonogram()
        {
            return await _connection.Table<Harmonogram>().ToListAsync();
        }

      

        // Tworzy harmonogram
        public async Task CreateHarmonogram(Harmonogram harmonogram)
        {
            await _connection.InsertAsync(harmonogram);
        }


        // Usuwa harmonogram
        public async Task DeleteHarmonogram(Harmonogram harmonogram)
        {
            await _connection.DeleteAsync(harmonogram);
        }

        public async Task CreateFrekwencja(Frekwencja frekwencja)
        {
            await _connection.InsertAsync(frekwencja);
        }

        public async Task<List<Frekwencja>> GetFrekwencjaByHarmonogramId(int harmonogramId)
        {
            return await _connection.Table<Frekwencja>().Where(f => f.HarmonogramId == harmonogramId).ToListAsync();
        }

        public async Task<Frekwencja> GetFrekwencjaByZawodnikAndHarmonogram(string zawodnikImieNazwisko, int harmonogramId)
        {
            return await _connection.Table<Frekwencja>()
                                     .Where(f => f.ZawodnikImieNazwisko == zawodnikImieNazwisko && f.HarmonogramId == harmonogramId)
                                     .FirstOrDefaultAsync();
        }
        //osiagniecia druzyny
        public async Task<List<OsiagnieciaDruzyny>> GetOsiagnieciaDruzyny()
        {
            return await Task.Run(() => _connection.Table<OsiagnieciaDruzyny>().ToListAsync());
        }

        public async Task CreateOsiagniecieDruzyny(OsiagnieciaDruzyny osiagniecie)
        {
            await Task.Run(() => _connection.InsertAsync(osiagniecie));
        }
        //osiagiecia posczegolnego zawodnika
        public async Task<List<OsiagnieciaZawodnika>> GetOsiagnieciaZawodnika(int zawodnikId)
        {
            return await Task.Run(() => _connection.Table<OsiagnieciaZawodnika>().Where(o => o.ZawodnikId == zawodnikId).ToListAsync());
        }

        public async Task CreateOsiagniecieZawodnika(OsiagnieciaZawodnika osiagniecie)  
        {
            await Task.Run(() => _connection.InsertAsync(osiagniecie));
        }

        public async Task CreateMecz(Mecze Mecz)
        {
            await _connection.InsertAsync(Mecz);
        }
    }
}
