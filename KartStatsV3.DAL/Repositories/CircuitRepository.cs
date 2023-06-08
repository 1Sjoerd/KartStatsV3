using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using KartStatsV3.Models;
using System.Data;
using KartStatsV3.BLL.Interfaces;


namespace YourNamespace.DAL.Repositories
{
    public class CircuitRepository : ICircuitRepository
    {
        private readonly string _connectionString;

        public CircuitRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyConnectionStringName");
        }

        public List<Circuit> GetAllCircuits()
        {
            List<Circuit> circuits = new List<Circuit>();

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT CircuitId, Name FROM Circuits";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Circuit circuit = new Circuit(
                                Convert.ToInt32(reader["CircuitId"]),
                                reader["Name"].ToString()
                            );

                            circuits.Add(circuit);
                        }
                    }
                }
            }

            return circuits;
        }

        public Circuit GetCircuitById(int circuitId)
        {
            Circuit circuit = null;

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SELECT CircuitId, Name FROM Circuits WHERE CircuitId = @circuitId";
                    cmd.Parameters.AddWithValue("@circuitId", circuitId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            circuit = new Circuit(
                                Convert.ToInt32(reader["CircuitId"]),
                                reader["Name"].ToString()
                            );
                        }
                    }
                }
            }

            return circuit;
        }

        public void AddCircuit(Circuit circuit)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO Circuits (Name) VALUES (@name)";
                    cmd.Parameters.AddWithValue("@name", circuit.Name);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCircuit(Circuit circuit)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "UPDATE Circuits SET Name = @name WHERE CircuitId = @circuitId";
                    cmd.Parameters.AddWithValue("@name", circuit.Name);
                    cmd.Parameters.AddWithValue("@circuitId", circuit.CircuitId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteCircuit(int circuitId)
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM Circuits WHERE CircuitId = @circuitId";
                    cmd.Parameters.AddWithValue("@circuitId", circuitId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
