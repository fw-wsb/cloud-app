using Backend.Models;
using Xunit;

namespace TaskManager.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void NewTask_ShouldNotBeCompleted()
        {
            var task = new TaskItem { Name = "Przetestować bezpiecznik" };

            Assert.False(task.IsCompleted);
        }
    }
}