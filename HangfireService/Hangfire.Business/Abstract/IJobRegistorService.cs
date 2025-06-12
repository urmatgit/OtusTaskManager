using Hangfire.Business.Rabbit;

namespace Hangfire.Business.Abstract
{
    public interface IJobRegistorService
    {
        Task RegisterJob();
    }
}