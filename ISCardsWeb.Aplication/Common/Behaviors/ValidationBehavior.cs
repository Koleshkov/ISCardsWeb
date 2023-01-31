using FluentValidation;
using ISCardsWeb.Aplication.Common.Exceptions;
using MediatR;
using ValidationException = ISCardsWeb.Aplication.Common.Exceptions.ValidationException;

namespace ISCardsWeb.Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            this.validators=validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var errors = validators
                .Select(v => v.Validate(context))
                .SelectMany(r => r.Errors)
                .Where(f => f!=null)
                .Select(s=>s.ErrorMessage)
                .ToList();

            if (errors.Count!=0)
            {
                throw new ValidationException(errors);
            }

            return next();
        }
    }
}
