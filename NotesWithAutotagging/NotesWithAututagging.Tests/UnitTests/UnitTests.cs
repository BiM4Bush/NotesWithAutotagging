using NotesWithAutotagging.Models;
using Xunit;

namespace NotesWithAututagging.Tests.UnitTests
{
	public class UnitTests
	{
		[Fact]
		public void NoteSetTag_ContentWithoutPhoneOrEmail_SetTagToNull()
		{
			string noteContent = "Lista zakupow na dzisiaj: chleb, mleko, ser";

			var note = new Note(noteContent);

			Assert.Equal(NoteTag.NULL, note.Tag);
		}

        [Fact]
        public void NoteSetTag_ContentWithPhoneNumber_SetTagToPhone()
        {
            string noteContent = "Moj numer telefonu 518-362-447";

            var note = new Note(noteContent);

            Assert.Equal(NoteTag.PHONE, note.Tag);
        }

        [Fact]
        public void NoteSetTag_ContentWithWrongFormatOfPhoneNumber_SetTagToNull()
        {
            string noteContent = "Moj numer telefonu 518 362 447";

            var note = new Note(noteContent);

            Assert.Equal(NoteTag.NULL, note.Tag);
        }

        [Fact]
        public void NoteSetTag_ContentWithEmailAdress_SetTagToMail()
        {
            string noteContent = "Po wykonaniu zadania wyslac repo na ag@sellintegro.cloud";

            var note = new Note(noteContent);

            Assert.Equal(NoteTag.MAIL, note.Tag);
        }


    }
}

