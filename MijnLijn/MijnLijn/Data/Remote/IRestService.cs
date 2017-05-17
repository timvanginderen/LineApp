using MijnLijn.Models;
using System.Threading.Tasks;

namespace MijnLijn.Data.Remote
{
    public interface IRestService
    {
        Task<ApiResponse> PostToGetLines(int[] stopIds);
    }
}
