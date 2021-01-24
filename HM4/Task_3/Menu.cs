using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.Json;

namespace Task_3
{
    internal class Menu
    {
        private readonly WorkingWithFile WorkingWithFile;
        private readonly List<Note> ListNotes;
        public Menu()
        {
            WorkingWithFile = new WorkingWithFile();
            try
            {
                ListNotes = string.IsNullOrEmpty(WorkingWithFile.Json) ?
                    new List<Note>()
                    : JsonSerializer.Deserialize<List<Note>>(WorkingWithFile.Json);
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"File read error! {ex.Message}\n");
                return;
            }
            StartMenu();
        }
        private void StartMenu()
        {
            while (true)
            {
                int num;
                Console.WriteLine("1.\tSearch notes");
                Console.WriteLine("2.\tShow notes");
                Console.WriteLine("3.\tCreate note");
                Console.WriteLine("4.\tDelete note");
                Console.WriteLine("5.\tExit");

                while (true)
                {
                    Console.Write("Enter the number: ");
                    if (!int.TryParse(Console.ReadLine(), out num)) Console.WriteLine("The only numbers can be entered. Try again");
                    else if (num < 1 || num > 5) Console.WriteLine("Incorrect number. Try again");
                    else break;
                }
                Console.WriteLine();
                switch (num)
                {
                    case 1:
                        SearchNotesMenu();
                        break;
                    case 2:
                        ShowNotesMenu();
                        break;
                    case 3:
                        CreateNoteMenu();
                        break;
                    case 4:
                        DeleteNoteMenu();
                        break;
                    case 5:
                        Exit();
                        break;
                }
            }
        }

        private void SearchNotesMenu()
        {
            Console.Write("Enter something from the note: ");
            string filter = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(filter)) WorkingWithNotes.ShowAllNotes(ListNotes);
            else if (!WorkingWithNotes.ShowNotesWithFilter(ListNotes, filter)) Console.WriteLine("No matching notes found!\n");
            Console.WriteLine("Done!\n");
        }

        private void ShowNotesMenu()
        {
            int id;
            while (true)
            {
                Console.Write("Enter the id of the note: ");
                if (!int.TryParse(Console.ReadLine(), out id)) Console.WriteLine("The only numbers can be entered. Try again");
                else break;
            }
            if (!WorkingWithNotes.ShowNoteById(ListNotes, id)) Console.WriteLine("No matching notes found!\n");
            Console.WriteLine("Done!\n");
        }
        private void CreateNoteMenu()
        {
            Console.Write("Enter the text for the note: ");
            string text = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Unable to create a blank note!\n");
                return;
            }
            while (text.StartsWith(' ')) text = text.Remove(0, 1);
            while (text.EndsWith(' ')) text = text.Remove(text.Length - 1, 1);
            WorkingWithNotes.CreateNewNote(ListNotes, text);
            WorkingWithFile.PushToFile(ListNotes);
            Console.WriteLine("Done!\n");
        }
        private void DeleteNoteMenu() 
        {
            int id;
            while (true)
            {
                Console.Write("Enter the id of the note: ");
                if (!int.TryParse(Console.ReadLine(), out id)) Console.WriteLine("The only numbers can be entered. Try again");
                else break;
            }
            if (!WorkingWithNotes.ShowNoteById(ListNotes, id))
            {
                Console.WriteLine("No matching notes found!\n");
                return;
            }
            if (WorkingWithNotes.DeleteNote(ListNotes, id))
            {
                ListNotes.RemoveAt(id - 1);
                for (int i = 0; i < ListNotes.Count; i++)
                    ListNotes[i] = new Note(i + 1, ListNotes[i].Title, ListNotes[i].Text, ListNotes[i].CreatedOn);
                WorkingWithFile.PushToFile(ListNotes);
                Console.WriteLine("Note deleted!");
            }
            Console.WriteLine("Done!\n");
        }
        private void Exit()
        {
            Console.Write("See you soon! Press any key and the program will exit.");
            Console.ReadKey();
            Process.GetCurrentProcess().Kill();
        }
    }
}
