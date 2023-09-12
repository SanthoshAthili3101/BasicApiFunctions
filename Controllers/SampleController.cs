using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SampleApiCheckingAS.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SampleApiCheckingAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly List<Food> foods = new List<Food>
        {
            new Food { Name = "Apple", Explanation = "Apples are a good source of dietary fiber and vitamin C." },
            new Food { Name = "Banana", Explanation = "Bananas are rich in potassium and provide quick energy." },
            new Food { Name = "Carrot", Explanation = "Carrots are high in beta-carotene and support eye health." },
            // Add more food items here
        };

        // GET: api/<SampleController>
        [HttpGet("all")]
        public ActionResult<IEnumerable<Food>> GetAllFoods()
        {
            return Ok(foods);
        }

        [HttpGet("{name}")]
        public ActionResult<Food> GetFoodByName(string name)
        {
            var food = foods.FirstOrDefault(f => string.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase));
            if (food == null)
            {
                return NotFound();
            }
            return Ok(food);
        }

        [HttpPost("CreateFood")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Food> CreateFood(Food food)
        {
            foods.Add(food);
            return CreatedAtAction(nameof(GetFoodByName), new { name = food.Name }, food);
        }

        [HttpPut("UpdateFood{name}")]
        public IActionResult UpdateFood(string name, [FromBody] Food updatedFood)
        {
            if (updatedFood == null || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var existingFood = foods.FirstOrDefault(f => string.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase));
            if (existingFood == null)
            {
                return NotFound();
            }


            existingFood.Name = updatedFood.Name;
            existingFood.Explanation = updatedFood.Explanation;

            return NoContent(); // 204 No Content
        }

        [HttpPatch("PartiallyUpdateFood{name}")]
        public IActionResult PartiallyUpdateFood(string name, [FromBody] JsonPatchDocument<Food> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var existingFood = foods.FirstOrDefault(f => string.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase));
            if (existingFood == null)
            {
                return NotFound();
            }


            // Return the updated food item
            return Ok(existingFood);
        }

        [HttpDelete("DeleteFood{name}")]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public IActionResult DeleteFood(string name)
        {
            var existingFood = foods.FirstOrDefault(f => string.Equals(f.Name, name, StringComparison.OrdinalIgnoreCase));
            if (existingFood == null)
            {
                return NotFound();
            }


            foods.Remove(existingFood);

            return NoContent();
        }
    }
}
