using System.Threading;
using System.Threading.Tasks;

using Apps.Common.Naming;

namespace Apps.Common
{
    public interface IApplication
    {
        string Name => GetType().Name.RemoveFirstSuffix("Application", "App");

        Task Run(CancellationToken ct = default);
    }
}