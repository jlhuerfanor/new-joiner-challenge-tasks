using Tasks.Model.Domain;

namespace Tasks.Service.Joiner
{
    public interface IJoinerQueryService
    {
        JoinerProfile GetProfile(long joinerIdNumber);
    }
}