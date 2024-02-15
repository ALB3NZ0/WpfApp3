using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using NoteApp.Models;
using NoteApp.Utilities;


namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        private const string FilePath = "notes.json";
        private List<Note> notes;

        public MainWindow()
        {
            InitializeComponent();
            LoadNotes(DateTime.Today);
        }

        private void LoadNotes(DateTime date)
        {
            notes = JsonSerializer.Deserialize<Note>(FilePath)
                .Where(note => note.Date.Date == date.Date)
                .ToList();
            NotesListBox.ItemsSource = notes.Select(note => note.Title);
        }

        private void DatePicker_SelectedDateChanged(object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (DatePicker.SelectedDate.HasValue)
            {
                LoadNotes(DatePicker.SelectedDate.Value);
            }
        }

        private void NotesListBox_SelectionChanged(object sender,
            System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (NotesListBox.SelectedIndex != -1)
            {
                Note selectedNote = notes[NotesListBox.SelectedIndex];
                NoteTitleTextBox.Text = selectedNote.Title;
                NoteDescriptionTextBox.Text = selectedNote.Description;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotesListBox.SelectedIndex != -1)
            {
                Note selectedNote = notes[NotesListBox.SelectedIndex];
                selectedNote.Title = NoteTitleTextBox.Text;
                selectedNote.Description = NoteDescriptionTextBox.Text;
                JsonSerializer.Serialize(notes, FilePath);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Создать новую заметку
            Note newNote = new Note
            {
                Title = NoteTitleTextBox.Text, // Использовать введенное название
                Description = "",
                Date = DatePicker.SelectedDate ?? DateTime.Today
            };
            notes.Add(newNote); // Добавить новую заметку в коллекцию
            JsonSerializer.Serialize(notes, FilePath); // Сохранить заметки в файл
            LoadNotes(DatePicker.SelectedDate ?? DateTime.Today); // Обновить ListBox
            NotesListBox.SelectedIndex = NotesListBox.Items.Count - 1; // Выбрать новую заметку в ListBox

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (NotesListBox.SelectedIndex != -1)
            {
                notes.RemoveAt(NotesListBox.SelectedIndex);
                JsonSerializer.Serialize(notes, FilePath);
                LoadNotes(DatePicker.SelectedDate ?? DateTime.Today);
            }
        }

   
    }
}
