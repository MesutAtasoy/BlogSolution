using System.Threading.Tasks;

namespace BlogSolution.Shared.Initializers
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}
