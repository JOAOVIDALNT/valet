using FluentValidation;
using valet.lib.Core.Domain.Interfaces;
using ValidationException = valet.lib.Core.Exception.ValidationException;

namespace valet.lib.Core.Patterns.Signature;

public abstract class Signature<TSignature, TValidator> : ISignature
    where TSignature : Signature<TSignature, TValidator>
    where TValidator : AbstractValidator<TSignature>, new()
{
    private static readonly TValidator SignatureValidator = new();

    public virtual bool Validate()
    {
        var validationResult =
            SignatureValidator.Validate((TSignature)this);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors.Select(error => error.ErrorMessage).ToList());

        return validationResult.IsValid;
    }
}
