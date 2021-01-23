using FaunaAspNet.API.Messages;
using FluentValidation;

namespace FaunaAspNet.API.Validation
{
    public class ArtistMessageValidator : AbstractValidator<ArtistMessage>
    {
        public ArtistMessageValidator()
        {
            RuleFor(artistMessage => artistMessage.Name)
                .Cascade(CascadeMode.Continue)
                .NotNull()
                .WithMessage("Name cannot be null!")
                .NotEmpty()
                .WithMessage("Name cannot be empty!");
        }
    }
}