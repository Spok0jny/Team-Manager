﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            _connection.CreateTableAsync<Mecze>();
            _connection.CreateTableAsync<przebiegMeczu>();
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
        //zwracja frekwencje po id
        public async Task<List<Frekwencja>> GetFrekwencjaByHarmonogramId(int harmonogramId)
        {
            return await _connection.Table<Frekwencja>().Where(f => f.HarmonogramId == harmonogramId).ToListAsync();
        }
        //zwraca frekwencje
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
        //tworzy osiagniecie druzyny
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
        //tworzy mecz
        public async Task CreateMecz(Mecze Mecz)
        {
            await _connection.InsertAsync(Mecz);
        }
        //aktualizuje akcje
        public async Task CreateAction(przebiegMeczu Akcja)
        {
            await _connection.InsertAsync(Akcja);
        }
        //zwraca ilosc meczy
        public async Task<int> GetCountMecze()
        {
            var listaMeczy = await _connection.Table<Mecze>().ToListAsync();
            return listaMeczy.Count;
        }
        //aktualizuje bramki
        public async Task UpdateBramki(int zawodnikId, int noweBramki)
        {
            var zawodnik = await _connection.Table<Zawodnicy>().Where(z => z.Id == zawodnikId).FirstOrDefaultAsync();

            if (zawodnik != null)
            {
                zawodnik.Bramki += noweBramki;

                await _connection.UpdateAsync(zawodnik);
            }
            else
            {
                Console.WriteLine($"Zawodnik o ID {zawodnikId} nie został znaleziony.");
            }
        }
        //aktualizuje asysty
        public async Task UpdateAsysty(int zawodnikId, int noweAsysty)
        {
            
            var zawodnik = await _connection.Table<Zawodnicy>().Where(z => z.Id == zawodnikId).FirstOrDefaultAsync();

            if (zawodnik != null)
            {
                
                zawodnik.Asysty += noweAsysty;

                
                await _connection.UpdateAsync(zawodnik);
            }
            else
            {
                
                Console.WriteLine($"Zawodnik o ID {zawodnikId} nie został znaleziony.");
            }
        }
        //aktualizuje zolte kartki
        public async Task UpdateZolteKartki(int zawodnikId, int noweZolteKartki)
        {
           
            var zawodnik = await _connection.Table<Zawodnicy>().Where(z => z.Id == zawodnikId).FirstOrDefaultAsync();

            if (zawodnik != null)
            {
                
                zawodnik.ZolteKartki += noweZolteKartki;

                
                await _connection.UpdateAsync(zawodnik);
            }
            else
            {
              
                Console.WriteLine($"Zawodnik o ID {zawodnikId} nie został znaleziony.");
            }
        }
        //aktualizuje czerwone kartki
        public async Task UpdateCzerwoneKartki(int zawodnikId, int noweCzerwoneKartki)
        {
            
            var zawodnik = await _connection.Table<Zawodnicy>().Where(z => z.Id == zawodnikId).FirstOrDefaultAsync();

            if (zawodnik != null)
            {
                
                zawodnik.CzerwoneKartki += noweCzerwoneKartki;

                
                await _connection.UpdateAsync(zawodnik);
            }
            else
            {
                
                Console.WriteLine($"Zawodnik o ID {zawodnikId} nie został znaleziony.");
            }
        }
        //zwraca szczegóły meczu
        public async Task<List<przebiegMeczu>> GetAkcjeWithDetails(int matchId, int teamId)
        {
            // Pobranie akcji meczu dla danego matchId i teamId
            var akcje = await _connection.Table<przebiegMeczu>()
                                          .Where(a => a.idMeczu == matchId && a.ktoraDruzyna == teamId)
                                          .OrderBy(a => a.ktoraMinuta)
                                          .ToListAsync();

            // Pobieramy wszystkie unikalne idZawodnika z akcji
            var zawodnicyIds = akcje
                                .Where(a => a.idZawodnika.HasValue)
                                .Select(a => a.idZawodnika.Value)
                                .Distinct()
                                .ToList();

            if (zawodnicyIds.Any())
            {
                // Pobieramy zawodników, którzy mają idZawodnika powiązane z akcjami
                var zawodnicy = await _connection.Table<Zawodnicy>()
                                                 .Where(z => zawodnicyIds.Contains(z.Id))
                                                 .ToListAsync();

                // Tworzymy słownik, gdzie klucz to Id zawodnika, a wartość to pełne imię i nazwisko
                var zawodnicyDict = zawodnicy.ToDictionary(z => z.Id, z => z.ImieNazwisko);

                // Aktualizujemy nazwiska zawodników w akcji
                foreach (var akcja in akcje)
                {
                    if (akcja.idZawodnika.HasValue && zawodnicyDict.ContainsKey(akcja.idZawodnika.Value))
                    {
                        akcja.nazwaZawodnika = zawodnicyDict[akcja.idZawodnika.Value]; // Ustawiamy pełne imię i nazwisko
                    }
                }
            }

            // Zwracamy listę akcji, zaktualizowaną o pełne imiona zawodników
            return akcje;
        }





        









    }
}
