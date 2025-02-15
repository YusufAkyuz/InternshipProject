using Emp.Entity.Entities;
 using FluentValidation;
 
 namespace Emp.Service.FluentValidations;
 
 public class UserValidator : AbstractValidator<User>
 {
     public UserValidator()
     {

         RuleFor(x => x.Email)
             .NotEmpty().WithMessage("Email is required.")
             .NotNull().WithMessage("Email cannot be null.")
             .MinimumLength(5).WithMessage("Email must be at least 5 characters long.")
             .EmailAddress().WithMessage("Email format is not valid."); 

         RuleFor(x => x.PhoneNumber)
             .NotEmpty().WithMessage("Phone number is required.")
             .NotNull().WithMessage("Phone number cannot be null.")
             .Matches(@"^\+?(\d{10,15})$").WithMessage("Phone number format is not valid.");

         RuleFor(x => x.Name)
             .NotEmpty().WithMessage("Name is required.")
             .NotNull().WithMessage("Name cannot be null.")
             .MaximumLength(30).WithMessage("Name of employee can max 30 characters.");
         
         RuleFor(x => x.LastName)
             .NotEmpty().WithMessage("Lastname is required.")
             .NotNull().WithMessage("Lastname cannot be null.")
             .MaximumLength(30).WithMessage("Lastname of employee can max 30 characters.");
         
         RuleFor(x => x.Salary)
             .NotEmpty().WithMessage("Salary is required.")
             .NotNull().WithMessage("Salary cannot be null.")
             .MaximumLength(7).WithMessage("Salary of employee can max 9999999.");

         // RuleFor(x => x.DateOfEntry)
         //     .NotEmpty().WithMessage("Date Of Entry is required.")
         //     .NotEmpty().WithMessage("Date Of Entry cannot be null.");

         
         RuleFor(x => x.Department)
             .NotEmpty().WithMessage("Department is required.")
             .NotEmpty().WithMessage("Department cannot be null.");
     }
     private bool BeAValidUtcDate(DateTime date)
     {
         return date.Kind == DateTimeKind.Utc;
     }
 }