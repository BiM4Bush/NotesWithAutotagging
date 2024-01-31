using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace NotesWithAutotagging.Models
{
	[Table("notes")]
	public class Note
	{
		[Column("id")]
        public Guid Id { get; private set; }

        [Column("content")]
        public string Content { get; set; }
        
		[Column("tag")]
        public NoteTag Tag { get; private set; }

		public Note(string content)		
		{
			Id = Guid.NewGuid();
			Content = content;
			Tag = SetTag(content);
		}

		private NoteTag SetTag(string content)
		{
			if(ContainsPhoneNumber(content))
			{
				return NoteTag.PHONE;
			} 
			else if (ContainsMail(content))
			{
				return NoteTag.MAIL; 
			}

			return NoteTag.NULL;
		}

		private bool ContainsPhoneNumber(string content)
		{
            Regex phoneNumberRegex = new Regex(@"\b\d{3}-\d{3}-\d{3}\b");// format XXX-XXX-XXX
            return phoneNumberRegex.IsMatch(content);
        }

		private bool ContainsMail(string content)
		{
            Regex emailRegex = new Regex(@"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b");// format abc@abc.abc
            return emailRegex.IsMatch(content);
        }
    }

	public enum NoteTag
	{
		NULL,
		PHONE,
		MAIL 
    }
}

