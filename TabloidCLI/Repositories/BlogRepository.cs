using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;
using Microsoft.Data.SqlClient;
using TabloidCLI.UserInterfaceManagers;


namespace TabloidCLI
{
    public class BlogRepository : DatabaseConnector, IRepository<Blog>
    {
        public BlogRepository(string connectionString) : base(connectionString) { }

        public List<Blog> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Title, URL FROM Blog";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Blog> blogs = new List<Blog>();
                    while (reader.Read())
                    {
                        Blog blog = new Blog
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Url = reader.GetString(reader.GetOrdinal("URL")),
                        };
                        blogs.Add(blog);
                    }
                    reader.Close();
                    return blogs;
                }
            }
        }

        public Blog Get(int id)
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
                    cmd.CommandText = @"DELETE FROM Blog WHERE Id =@Id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Update(Blog blog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Blog
                                        SET Title = @title,
                                            URL = @url,
                                        WHERE Id = @id";

                    cmd.Parameters.AddWithValue("@title", blog.Title);
                    cmd.Parameters.AddWithValue("@url", blog.Url);
                    cmd.Parameters.AddWithValue("@id", blog.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Insert(Blog blog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Blog (Title, URL) OUTPUT INSERTED.Id VALUES (@title, @url)";
                    cmd.Parameters.AddWithValue(@"Title", blog.Title);
                    cmd.Parameters.AddWithValue(@"URL", blog.Url);
                    int id = (int)cmd.ExecuteScalar();
                    blog.Id = id;

                }
            }
        }

        public void InsertTag(Blog blog, Tag tag)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO BlogTag (BlogId, TagId)
                                                       VALUES (@blogId, @tagId)";
                    cmd.Parameters.AddWithValue("@blogId", blog.Id);
                    cmd.Parameters.AddWithValue("@tagId", tag.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTag(int blogId, int tagId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM BlogTag 
                                         WHERE BlogId = @blogid AND 
                                               TagId = @tagId";
                    cmd.Parameters.AddWithValue("@blogId", blogId);
                    cmd.Parameters.AddWithValue("@tagId", tagId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

