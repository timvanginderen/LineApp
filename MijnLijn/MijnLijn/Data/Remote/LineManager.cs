using MijnLijn.Models;
using System.Threading.Tasks;

namespace MijnLijn.Data.Remote
{
    public class LineManager
    {
        IRestService service;

        public LineManager(IRestService service)
        {
            this.service = service;
        }
        public Task<ApiResponse> GetLines(int[] stopNumbers)
        {
            return service.PostToGetLines(stopNumbers);
        }
    }
}
