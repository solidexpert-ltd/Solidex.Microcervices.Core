
namespace Solidex.Microcervices.Core.Infrasructure
{
    public static class Paginator
    {
        public static int Skip(int page, int count)
        {
            return (page - 1 < 0 ? 0 : page - 1) * count;
        }

    }
}