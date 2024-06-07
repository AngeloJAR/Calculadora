using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Tarea1.Models;


namespace Tarea1.Controlador
{
    public class db_Conexion
    {
        public SQLiteAsyncConnection _database;

        public db_Conexion(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Historial>().Wait();
            _database.CreateTableAsync<Persona>().Wait();
        }

        // Métodos para la tabla Historial
        public Task<List<Historial>> GetOperacionesAsync()
        {
            return _database.Table<Historial>().ToListAsync();
        }

        public Task<int> SaveOperacionAsync(Historial operacion)
        {
            return _database.InsertAsync(operacion);
        }

        public Task<int> DeleteOperacionAsync(Historial operacion)
        {
            return _database.DeleteAsync(operacion);
        }
        //Metodos para la tabla Persona
        public Task<List<Persona>> GetPersonasAsync()
        {
            return _database.Table<Persona>().ToListAsync();
        }

        public Task<Persona> GetPersonaAsync(string idCedula)
        {
            return _database.Table<Persona>().Where(i => i.IdCedula == idCedula).FirstOrDefaultAsync();
        }

        public Task<int> SavePersonaAsync(Persona persona)
        {
            return _database.InsertAsync(persona);
        }

        public Task<int> UpdatePersonaAsync(Persona persona)
        {
            return _database.UpdateAsync(persona);
        }

        public Task<int> DeletePersonaAsync(Persona persona)
        {
            return _database.DeleteAsync(persona);
        }
    }
}