using Microsoft.AspNetCore.Mvc;
using NutritionAPI.Models;
using NutritionAPI.Models.Client_Requests;

namespace NutritionAPI.Controllers
{
    [Route("api/foods")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly DataContext _context;
        public Task<List<Food>> _latestList { get; set; }

        public FoodController(DataContext context)
        {
            _context = context;
            _latestList = _context.Foods.ToListAsync();
        }



        [HttpGet]
        public async Task<ActionResult<List<Food>>> GetFoods()
        {
            return Ok( await _latestList );
        }

        [HttpPost]
        public async Task<ActionResult<List<Food>>> CreateFood( Food_CR payload ) 
        {
            //  Create
            Food item = new Food( Guid.NewGuid() );
            
            //  Apply
            item.ApplyFromClientRequest(payload);

            //  Queue
            _context.Foods.Add( item );

            //  Save
            await _context.SaveChangesAsync();

            //  List
            return Ok( await _latestList );
        }


        [HttpPut]
        public async Task<ActionResult<List<Food>>> UpdateFood( Food_CR payload ) 
        {

            //  Exist Check
            var item = await _context.Foods.FindAsync(new Guid( payload.id ));
            if ( item == null)
            {
                return BadRequest("Food not found");
            }

            //  Merge Updates
            item.ApplyFromClientRequest( payload );

            //  Save
            await _context.SaveChangesAsync();

            //  Return Full List
            return Ok(await _latestList );
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Food>>> DeleteFood(string id)
        {
            //  Exist
            var item = await _context.Foods.FindAsync( new Guid(id) );
            if ( item == null)
            {
                return BadRequest("Food not found");
            }

            //  Queue
            _context.Foods.Remove( item );
            
            //  Save
            await _context.SaveChangesAsync();

            //  List
            return Ok(await _latestList );
        }

    }
}
