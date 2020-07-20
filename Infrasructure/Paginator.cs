using Solidex.Core.ViewModels.Querying;

namespace Microcervices.Core.Infrasructure
{
    public static class Paginator
    {
        public static int Skip(int p, int c)
        {
            return (p - 1 < 0 ? 0 : p - 1) * c;
        }

        public static int Skip<T>(this FilterRequest<T> request)
        {
            return Skip(request.Page, request.Limit);
        }
    }
}