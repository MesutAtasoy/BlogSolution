using System.Threading.Tasks;

namespace BlogSolution.Framework.Initializers
{
    public interface IInitializer
    {
        Task InitializeAsync();
    }
}
