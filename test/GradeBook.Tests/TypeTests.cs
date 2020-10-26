using System;
using Xunit;

namespace GradeBook.Tests
{
    public delegate string WriteLogDelegate(string logMessage);
    public class TypeTests
    {
        int count = 0;
        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage;

            log += new WriteLogDelegate(ReturnMessage);
            log += IncrementCount;

            var result = log("Hello!");
            Assert.Equal("Hello!",result);
        }
        string IncrementCount(string message)
        {
            count++;
            return message.ToLower();
        }
        string ReturnMessage(string message)
        {
            count++;
            return message;
        }

        [Fact]
        public void ValeTypeAlsoPassByValue()
        {
            var x = GetInt();
            //SetInt(x)   - pass by value
            SetInt(ref x);    //pass by ref

            //Assert.Equal(3,x);
            Assert.Equal(42,x);
        }

        private void SetInt(ref int x)
        {
            x = 42;
        }

        private int GetInt()
        {
            return 3;
        }

        [Fact]
        public void CSharpCanPassRef()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(ref book1, "New Name");  //or GetBookSetName(out book1, "New Name");

            Assert.Equal("New Name",book1.Name);
        }

        private void GetBookSetName(ref InMemoryBook book, string name)  //or private void GetBookSetName(out Book book, string name)
        {
            book = new InMemoryBook(name);
        }

        [Fact]
        public void CSharpPassByValue()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name");

            Assert.Equal("Book 1",book1.Name);
        }

        private void GetBookSetName(InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");

            Assert.Equal("New Name",book1.Name);
        }

        private void SetName(InMemoryBook book, string name)
        {
            book.Name = name;
        }

        [Fact]
        public void StringBehaveLikeValueType()
        {
            string name = "akhil";
            var upper = MakeUpperCase(name);

            Assert.Equal("akhil", name);
            Assert.Equal("AKHIL", upper);
        }

        private string MakeUpperCase(string parameter)
        {
            return parameter.ToUpper();
        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1",book1.Name);
            Assert.Equal("Book 2",book2.Name);
            Assert.NotSame(book1, book2);
        }

        [Fact]
        public void TwoVarsCanReferenceSameObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = book1;

            Assert.Equal("Book 1",book1.Name);
            Assert.Equal("Book 1",book2.Name);
            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }
    }
}