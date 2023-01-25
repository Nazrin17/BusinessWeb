using FluentValidation;
using No10.Dtos.Employee;

namespace No10.Validators
{
    public class EmployeeCreateValidation:AbstractValidator<EmployeePostDto>
    {
        public EmployeeCreateValidation()
        {
            RuleFor(e=>e.Icons).NotEmpty().NotNull();
            RuleFor(e=>e.Name).NotEmpty().NotNull();
            RuleFor(e=>e.Position).NotEmpty().NotNull();
            RuleFor(e=>e.formFile).NotEmpty().NotNull();
        }

    }
}
