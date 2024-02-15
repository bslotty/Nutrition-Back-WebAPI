using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NutritionAPI.Models;
using NutritionAPI.Models.Client_Requests;

namespace NutritionAPI.Controllers
{
    [Route("api/meals")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly DataContext _context;

        public Task<List<Meal>> _latestList { get; set; }

        public MealController(DataContext context)
        {
            _context = context;
            _latestList = _context.Meals.Include( m => m.Parts ).ThenInclude( p => p.Food ).ToListAsync();
        }


        [HttpGet]
        public async Task<ActionResult<List<Meal>>> GetMeals()
        {
            return Ok(await _latestList );
        }


        [HttpPost]
        public async Task<ActionResult<List<Meal>>> CreateMeal( Meal_CR payload )
        {
            //  Create
            Meal item = new Meal( Guid.NewGuid() );

            //  Apply
            item.ApplyFromClientRequest( payload );

            //  Queue List
            payload.parts.ToList().ForEach( ( MealPart_CR request ) => {
                MealPart part = new MealPart( Guid.NewGuid(), item.Id );
                part.ApplyFromClientRequest( request );

                //  SetFood from ID
                Food food = _context.Foods.FirstOrDefault(f => f.Id == new Guid(request.food.id) );
                part.Food = food;

                item.Parts.Add( part );
            });

            //  Queue
            item.Parts.ForEach( p => _context.MealParts.Add( p ) );
            _context.Meals.Add( item );
            

            //  Save
            await _context.SaveChangesAsync();

            //  List
            return Ok(await _latestList );
        }

        [HttpPut]
        public async Task<ActionResult<List<Meal>>> UpdateMeal( Meal_CR payload ) {
            //  Exist Check
            var item = await _context.Meals.FindAsync( new Guid( payload.id ) );
            if ( item == null)
            {
                return BadRequest("Meal not found");
            }

            //  Apply Part Updates
            item.ApplyFromClientRequest( payload );

            //  Delete Meal Parts
            List<MealPart> list = await _context.MealParts.Where(mp => mp.MealId == new Guid(payload.id)).ToListAsync();
            list.ForEach( ( MealPart part ) => {
                _context.MealParts.Remove( part );
            } );
            //  Save
            await _context.SaveChangesAsync();


            //  Apply Food to Part
            payload.parts.ForEach( ( MealPart_CR request ) => {

                MealPart part = new MealPart( Guid.NewGuid(), item.Id );
                part.ApplyFromClientRequest( request );

                //  SetFood from ID
                Food? food = _context.Foods.Find( new Guid( request.food.id ) );
                if ( food == null ) {
                    BadRequest( "Food not found" );
                }
                part.Food = food;

                //  Apply Changes
                _context.MealParts.Add( part );
            } );

            //  Save
            await _context.SaveChangesAsync();

            //  Return Full List
            return Ok(await _latestList );
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Meal>>> DeleteMeal(string id)
        {
            //  Exist
            var dbMeal = await _context.Meals.FindAsync(new Guid(id) );
            if (dbMeal == null)
            {
                return BadRequest("Meal not found");
            }

            //  Queue
            _context.Meals.Remove(dbMeal);
            
            //  Save
            await _context.SaveChangesAsync();

            //  List
            return Ok(await _latestList );
        }

    }
}
