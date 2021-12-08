using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;
using Microsoft.Data.SqlClient;
using TabloidCLI.UserInterfaceManagers;


namespace TabloidCLI
{
    public class JournalRepository : DatabaseConnector, IRepository<Journal>
    {
        public JournalRepository(string connectionString) : base(connectionString) { }

        public List<Journal> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Title, Content, CreateDateTime FROM Journal";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Journal> journals = new List<Journal>();
                    while (reader.Read())
                      {
                        Journal journal = new Journal
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                        };
                        journals.Add(journal);
                    }
                    reader.Close();
                    return journals;
                }
            }
        }

        public Journal Get(int id)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Journal WHERE Id =@Id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Update(Journal journal)
        {
            throw new NotImplementedException();
        }

        public void Insert(Journal journal)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Journal (Title, Content, CreateDateTime) OUTPUT INSERTED.Id VALUES (@title, @content, @createDateTime)";
                    cmd.Parameters.AddWithValue(@"Title", journal.Title);
                    cmd.Parameters.AddWithValue(@"Content", journal.Content);
                    cmd.Parameters.AddWithValue(@"CreateDateTime", journal.CreateDateTime);
                    int id = (int)cmd.ExecuteScalar();
                    journal.Id = id;

                }
            }
        }
    }
}
