namespace AssignmentManagement.Tests
{
    using Xunit;
    using AssignmentManagement.Core;
    using System.Collections.Generic;

    public class AssignmentServiceTests
    {
        [Fact]
        public void ListIncomplete_ShouldReturnOnlyAssignmentsThatAreNotCompleted()
        {
            // Arrange
            var service = new AssignmentService();
            var a1 = new Assignment("Incomplete Task", "Do something");
            var a2 = new Assignment("Completed Task", "Do something else");
            a2.MarkComplete();

            service.AddAssignment(a1);
            service.AddAssignment(a2);

            // Act
            var result = service.ListIncomplete();

            // Assert
            Assert.Single(result);
            Assert.Equal("Incomplete Task", result[0].Title);
        }

        [Fact]
        public void ListIncomplete_ShouldReturnEmptyList_WhenNoAssignmentsExist()
        {
            // Arrange
            var service = new AssignmentService();

            // Act
            var result = service.ListIncomplete();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void ListIncomplete_ShouldReturnOnlyIncompleteAssignments_FromMixedList()
        {
            // Arrange
            var service = new AssignmentService();
            var a1 = new Assignment("Task A", "First task"); // incomplete
            var a2 = new Assignment("Task B", "Second task"); // complete
            var a3 = new Assignment("Task C", "Third task"); // incomplete

            a2.MarkComplete();

            service.AddAssignment(a1);
            service.AddAssignment(a2);
            service.AddAssignment(a3);

            // Act
            var result = service.ListIncomplete();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, a => a.Title == "Task A");
            Assert.Contains(result, a => a.Title == "Task C");
        }
    }
}
