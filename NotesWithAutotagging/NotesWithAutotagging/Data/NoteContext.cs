using System;
using Microsoft.EntityFrameworkCore;
using NotesWithAutotagging.Models;

namespace NotesWithAutotagging.Data
{
    public class NoteContext : DbContext
    {
        public NoteContext(DbContextOptions<NoteContext> options) : base(options)
        {

        }

        public DbSet<Note> Notes { get; set; }
    }
}

