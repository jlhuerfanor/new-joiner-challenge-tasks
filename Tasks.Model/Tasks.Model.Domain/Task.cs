using System.Collections.Generic;

namespace Tasks.Model.Domain
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public int EstimatedRequiredHours { get; set; }
        public string Stack { get; set; }
        public IList<string> MinimumRoles { get; set; }   
        public long AssignedIdNumber { get; set; }
        
        public Task ParentTask { get; set; }
        public IList<Task> LinkedTasks { get; set; }
    }
}