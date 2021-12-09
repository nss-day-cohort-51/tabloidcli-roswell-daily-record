using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private string _connectionString;
        

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Post Details");
            Console.WriteLine(" 3) Add Post");
            Console.WriteLine(" 4) Edit Post");
            Console.WriteLine(" 5) Remove Post");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    //Post post = Choose();
                    //if (post == null)
                    //{
                    //    return this;
                    //}
                    //else
                    //{
                    //    return new PostDetailManager(this, _connectionString, post.Id);
                    //}
                    //return this;
                case "3":
                    Add();
                    return this;
                case "4":
                    Edit();
                    return this;
                case "5":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void List()
        {

        }

        //private Post Choose(string prompt = null)
        //{
            
        //}

        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.Write("Title: ");
            post.Title = Console.ReadLine();

            Console.Write("URL: ");
            post.Url = Console.ReadLine();

            post.PublishDateTime = DateTime.Now;

            List<Author> authors = _authorRepository.GetAll();

            for (int i = 0; i < authors.Count; i++)
            {
                Author author = authors[i];
                Console.WriteLine($" {i + 1} {author.FullName}");
            }
            Console.Write("Choose an Author: ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                post.Author = authors[choice];                
            }
            catch(Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return;
            }

            Console.Write("Choose a Blog: ");

            List<Blog> blogs = _blogRepository.GetAll();
            for (int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                Console.WriteLine($" {i + 1} {blog.Title}");
            }
            Console.Write("> ");

            string blogInput = Console.ReadLine();
            try
            {
                int choice = int.Parse(blogInput);
                post.Blog = blogs[choice];
            }
            catch(Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return;
            }

           _postRepository.Insert(post);
        }

        private void Edit()
        {

        }

        private void Remove()
        {

        }


    }
}
