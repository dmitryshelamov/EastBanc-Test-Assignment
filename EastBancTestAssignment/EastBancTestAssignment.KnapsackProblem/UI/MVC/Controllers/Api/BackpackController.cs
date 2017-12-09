using System.Threading.Tasks;
using System.Web.Http;
using EastBancTestAssignment.KnapsackProblem.BLL.Services;

namespace EastBancTestAssignment.KnapsackProblem.UI.MVC.Controllers.Api
{
    public class BackpackController : ApiController
    {
        private BackpackTaskService _service;

        public BackpackController()
        {
            _service = new BackpackTaskService();
        }

        // DELETE: api/Backpack/id
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(string id)
        {
            await _service.DelelteBackpackTask(id);
            return Ok();
        }
    }
}
