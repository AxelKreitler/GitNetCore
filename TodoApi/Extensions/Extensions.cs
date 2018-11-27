using TaskApi.Models;

namespace TaskApi.Extensions
{
    public static class Extensions
    {
        public static bool isFinalStatus(this TaskStatus status)
        {
            return  status ==TaskStatus.Done ? true :
                    status ==TaskStatus.Canceled ? true : false;
        }
    }
}