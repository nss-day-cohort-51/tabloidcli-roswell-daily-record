using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class TagManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;
        private string _connectionString;

        public TagManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _tagRepository = new TagRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Tag Menu");
            Console.WriteLine(" 1) List Tags");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Edit Tag");
            Console.WriteLine(" 4) Remove Tag");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    Choose();
                    return this;
                case "3":
                    Edit();
                    return this;
                case "4":
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
            List<Tag> tags = _tagRepository.GetAll();
            foreach(Tag tag in tags)
            {
                Console.WriteLine($"Name: {tag.Name}");
            }
        }
        private Tag Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Tag:";
            }

            Console.WriteLine(prompt);

            List<Tag> tags = _tagRepository.GetAll();

            for (int i = 0; i < tags.Count; i++)
            {
                Tag tag = tags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return tags[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Add()
        {
            Console.WriteLine("Create a new tag: ");
            Tag tag = new Tag();

            Console.WriteLine("Tag name: ");
            tag.Name = Console.ReadLine();

            _tagRepository.Insert(tag);
        }


        private void Edit()
        {
           Tag tagToEdit = Choose("Which tag would you like to edit?");
            if (tagToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Name (blank to leave unchanged: ");
            string content = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(content))
            {
                tagToEdit.Name = content;
            }
            _tagRepository.Update(tagToEdit);
        }

       

        private void Remove()
        {
            Tag tagToDelete = Choose("Which tag would you like to remove?");
            if (tagToDelete != null)
            {
                _tagRepository.Delete(tagToDelete.Id);
            }
        }
    }
}
