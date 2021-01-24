using NotesApp.Services.Abstractions;
using NotesApp.Services.Models;
using NotesApp.Services.Services;
using System;
using Xunit;
using Moq;

namespace NotesApp.Services.Tests
{
    public class NotesServiceTests 
    {
        private NotesService _notesService;

        public NotesServiceTests() { }

        [Fact]
        public void AddNote_Should_Fail_If_Null()
        {
            var notesStorageMock = new Mock<INotesStorage>();
            var noteEventsMock = new Mock<INoteEvents>();

            _notesService = new NotesService(notesStorageMock.Object, noteEventsMock.Object);

            Note tempNote = null;
            var tempUserId = 1;

            Assert.Throws<ArgumentNullException>(() => _notesService.AddNote(tempNote, tempUserId));
        }

        [Fact]
        public void AddNote_Should_Success_And_NotifyAdded()
        {
            var notesStorageMock = new Mock<INotesStorage>();
            var noteEventsMock = new Mock<INoteEvents>();

            _notesService = new NotesService(notesStorageMock.Object, noteEventsMock.Object);
            Note tempNote = new Note();
            var tempUserId = 1;

            _notesService.AddNote(tempNote, tempUserId);

            notesStorageMock.Verify(x => x.AddNote(tempNote, tempUserId), Times.Once);
            noteEventsMock.Verify(x => x.NotifyAdded(tempNote, tempUserId), Times.Once);           
        }

        [Fact]
        public void AddNote_Should_Fail_And_Not_NotifyAdded()
        {
            var notesStorageMock = new Mock<INotesStorage>();
            var noteEventsMock = new Mock<INoteEvents>();

            _notesService = new NotesService(notesStorageMock.Object, noteEventsMock.Object);

            Note tempNote = null;
            var tempUserId = 1;    

            Assert.Throws<ArgumentNullException>(() => _notesService.AddNote(tempNote, tempUserId));
            notesStorageMock.Verify(x => x.AddNote(tempNote, tempUserId), Times.Never);
            noteEventsMock.Verify(x => x.NotifyAdded(tempNote, tempUserId), Times.Never);
        }

        [Fact]
        public void DeleteNote_Should_Success_And_NotifyDeleted()
        {
            var notesStorageMock = new Mock<INotesStorage>();
            var noteEventsMock = new Mock<INoteEvents>();

            Note tempNote = new Note();
            var tempUserId = 1;

            notesStorageMock.Setup(x => x.DeleteNote(tempNote.Id)).Returns(true);

            _notesService = new NotesService(notesStorageMock.Object, noteEventsMock.Object);

            _notesService.DeleteNote(tempNote.Id, tempUserId);   

            notesStorageMock.Verify(x => x.DeleteNote(tempNote.Id), Times.Once);
            noteEventsMock.Verify(x => x.NotifyDeleted(tempNote.Id, tempUserId), Times.Once);
        }

        [Fact]
        public void DeleteNote_Should_Fail_And_Not_NotifyDeleted()
        {
            var notesStorageMock = new Mock<INotesStorage>();
            var noteEventsMock = new Mock<INoteEvents>();

            Note tempNote = new Note();
            var tempUserId = 1;

            notesStorageMock.Setup(x => x.DeleteNote(tempNote.Id)).Returns(false);

            _notesService = new NotesService(notesStorageMock.Object, noteEventsMock.Object);

            _notesService.DeleteNote(tempNote.Id, tempUserId);

            Assert.True(_notesService.DeleteNote(tempNote.Id, tempUserId) == false);
            noteEventsMock.Verify(x => x.NotifyDeleted(tempNote.Id, tempUserId), Times.Never);
        }
    }
}
