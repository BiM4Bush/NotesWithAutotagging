using System;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NotesWithAutotagging.Data;
using NotesWithAutotagging.Models;
using Xunit;

namespace NotesWithAututagging.Tests.IntegrationTests
{
    public class IntegrationTests : IDisposable
    {
        private readonly NoteContext _context;

        public IntegrationTests()
        {
            var options = new DbContextOptionsBuilder<NoteContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            _context = new NoteContext(options);

            // Examples
            _context.Notes.Add(new Note("Moj nr telefonu  123-456-789"));
            _context.Notes.Add(new Note("Moj adres email to test@example.com"));
            _context.SaveChanges();
        }

        [Fact]
        public void GetNotes_ShouldReturnAllNotes()
        {
            var notes = _context.Notes.ToList();

            Assert.NotNull(notes);
            Assert.Equal(2, notes.Count);
        }

        [Fact]
        public void GetNoteById_ShouldReturnCorrectNote()
        {
            var existingNote = _context.Notes.First();

            var retrievedNote = _context.Notes.Find(existingNote.Id);

            Assert.NotNull(retrievedNote);
            Assert.Equal(existingNote.Content, retrievedNote.Content);
        }

        [Fact]
        public void CreateNote_ShouldAddNewNoteToDatabase()
        {
            var newNote = new Note("Zrobic pranie");

            _context.Notes.Add(newNote);
            _context.SaveChanges();

            var retrievedNote = _context.Notes.Find(newNote.Id);
            Assert.NotNull(retrievedNote);
            Assert.Equal(newNote.Content, retrievedNote.Content);
        }

        [Fact]
        public void UpdateNote_ShouldUpdateContentOfNote()
        {
            var existingNote = _context.Notes.First();
            var updatedContent = "Pranie zostalo zrobione";

            existingNote.Content = updatedContent;
            _context.SaveChanges();

            var retrievedNote = _context.Notes.Find(existingNote.Id);
            Assert.NotNull(retrievedNote);
            Assert.Equal(updatedContent, retrievedNote.Content);
        }

        [Fact]
        public void DeleteNote_ShouldRemoveNote()
        {
            var existingNote = _context.Notes.First();

            _context.Notes.Remove(existingNote);
            _context.SaveChanges();

            var retrievedNote = _context.Notes.Find(existingNote.Id);
            Assert.Null(retrievedNote);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

