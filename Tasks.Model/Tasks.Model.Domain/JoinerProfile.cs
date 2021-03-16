using  System.Text.Json;

namespace Tasks.Model.Domain
{
    public class JoinerProfile
    {
        public int? Id { get; set; }
        public string IdNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Stack { get; set; }
        public string Role { get; set; }
        public string EnglishLevel { get; set; }
        public string DomainExperience { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, null);
        }
    }
    
}