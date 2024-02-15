using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NutritionAPI.Models;
using NutritionAPI.Models.Client_Requests;
using NutritionAPI.Models.ClientRequests;

namespace NutritionAPI.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly DataContext _context;

        public Task<List<Recipe>> _latestList { get; set; }

        public RecipesController( DataContext context)
        {
            _context = context;
            _latestList = _context.Recipes.Include( r => r.Contents ).ThenInclude( p => p.Food ).ToListAsync();
        }



        [HttpGet]
        public async Task<ActionResult<List<Recipe>>> Get()
        {

            return Ok(await _latestList );

        }

        [HttpPost]
        public async Task<ActionResult<List<Recipe>>> Create(Recipe_CR payload)
        {
            Recipe item = new Recipe( Guid.NewGuid() );
            item.ApplyFromClientRequest(payload);

            payload.contents.ToList().ForEach( ( RecipePart_CR request ) => {
                RecipePart part = new RecipePart( Guid.NewGuid(), item.Id );
                 part.ApplyFromClientRequest( request );

                 //  SetFood from ID
                 Food food = _context.Foods.FirstOrDefault(f => f.Id == new Guid(request.food.id) );
                 part.Food = food;

                 item.Contents.Add( part );
             } );

            //  Queue
            item.Contents.ForEach( p => _context.RecipeParts.Add( p ) );

            _context.Recipes.Add( item );

            //  Also Add Recipe Totals as food for Meal Selection

            await _context.SaveChangesAsync();

            return Ok(await _latestList );
        }


        [HttpPut]
        public async Task<ActionResult<List<Recipe>>> Update( Recipe_CR payload )
        {
            //  Exist Check
            var item = await _context.Recipes.FindAsync( Guid.Parse( payload.id ) );
            if ( item == null ) {
                return BadRequest( "Meal not found" );
            }

            //  Apply Part Updates
            item.ApplyFromClientRequest( payload );

            //  Delete Parts
            List<RecipePart> list = await _context.RecipeParts.Where(mp => mp.RecipeId ==  new Guid(payload.id)).ToListAsync();
            if ( list != null ) {
                list.ForEach( ( RecipePart part ) => {
                    _context.RecipeParts.Remove( part );
                } );
                //  Save
                await _context.SaveChangesAsync();
            }



            //  Apply Food to Part
            payload.contents.ToList().ForEach( ( RecipePart_CR request ) => {
                RecipePart part = new RecipePart( Guid.NewGuid(), item.Id );
                part.ApplyFromClientRequest( request );

                //  SetFood from ID
                Food? food = _context.Foods.Find( new Guid( request.food.id ) );
                if ( food == null ) {
                    BadRequest( "Food not found" );
                }
                part.Food = food;

                item.Contents.Add( part );

            } );

            //  Queue
            item.Contents.ForEach( p => _context.RecipeParts.Add( p ) );

            //  Save
            await _context.SaveChangesAsync();

            //  Return Full List
            return Ok( await _latestList );
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Recipe>>> Delete(string id)
        {

            Guid guid = Guid.Parse(id);

            var item = await _context.Recipes.FindAsync(guid);
            if ( item == null)
            {
                return BadRequest( "Recipe not found" );
            }

            _context.Recipes.Remove( item );
            await _context.SaveChangesAsync();

            return Ok(await _latestList );
        }

    }
}
