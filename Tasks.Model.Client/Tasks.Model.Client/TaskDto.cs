using System.Collections.Generic;

namespace Tasks.Model.Client
{
    public class TaskDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? Completed { get; set; }
        public int? EstimatedRequiredHours { get; set; }
        public string Stack { get; set; }
        public IList<string> MinimumRoles { get; set; }
        public long? AssignedIdNumber { get; set; }
        public int? ParentTaskId { get; set; }
    }
}