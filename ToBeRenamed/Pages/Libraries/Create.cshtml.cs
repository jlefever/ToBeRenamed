using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToBeRenamed.Commands;
using ToBeRenamed.Dtos;
using ToBeRenamed.Queries;

namespace ToBeRenamed.Pages.Libraries
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IMediator _mediator;

        public CreateModel(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [BindProperty]
        public string Title { get; set; }
        
        [BindProperty]
        public string Description { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var userDto = await _mediator.Send(new GetSignedInUserDto(User));

            await _mediator.Send(new CreateLibrary(userDto.Id, Title, Description));

            return RedirectToPage("/Libraries/Index");
        }
    }
}