using System;
using System.Collections.Generic;

namespace Task_3
{
    internal static class WorkingWithNotes
    {
        internal static void ShowAllNotes(List<Note> lst)
        {
            Console.WriteLine();
            foreach (var note in lst)
                Console.WriteLine(note.ToString(false));
            if (lst.Count == 0) Console.WriteLine("The list is empty!");
        }
        internal static bool ShowNotesWithFilter(List<Note> lst, string filter)
        {
            bool stat = false;
            foreach (var note in lst)
            {
                if (note.Id.ToString().Contains(filter) || note.Title.Contains(filter)
                    || note.Text.Contains(filter) || note.CreatedOn.ToString().Contains(filter))
                {
                    Console.WriteLine(note.ToString(false));
                    stat = true;
                }
            }
            return stat;
        }
        internal static bool ShowNoteById(List<Note> lst, int filter)
        {
            bool stat = false;
            foreach (var note in lst)
            {
                if (note.Id == filter)
                {
                    Console.WriteLine(note.ToString(true));
                    stat = true;
                }
            }
            return stat;
        }
        internal static void CreateNewNote(List<Note> lst, string text)
        {
            int id = lst.Count + 1;
            string title = text.Length > 32 ?
                text.Substring(0, 32)
                : text[..];
            DateTime createOn = DateTime.UtcNow;
            lst.Add(new Note(id, title, text, createOn));
        }
        internal static bool DeleteNote(List<Note> lst, int filter)
        {
            Console.WriteLine("Are you sure you want to delete the note?\nY(y)\t—\tyes;\nN(n)\t—\tno;\n");
            Console.Write("Enter 1 of 2 ways: ");
            var tap = Console.ReadKey();
            while (tap.Key != ConsoleKey.Y && tap.Key != ConsoleKey.N) 
            {
                Console.Write("\nPlease choose 1 of 2 ways: ");
                tap = Console.ReadKey();
            }
            Console.WriteLine("\n");
            if (tap.Key == ConsoleKey.N) return false;
            else return true;       
        }
    }
}
