namespace WebApi.Repositories
{
    public interface IJsonRepository
    {
        Models.Domain.Policy GetPolicy(int id);

        int AddPolicy(Models.Domain.Policy policy);

        Models.Domain.Policy UpdatePolicy(int id, Models.Domain.Policy policy);
    }
}
