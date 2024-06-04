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
    }
}