using FluentValidation;
using FluentValidation.Results;
using valet.lib.Core.Domain.Interfaces;

namespace valet.lib.Core.Patterns.Signature;

public abstract class Signature<TSignature, TValidator> : ISignature
    where TSignature : Signature<TSignature, TValidator>
    where TValidator : AbstractValidator<TSignature>, new()
{
    private static readonly TValidator SignatureValidator = new();

    public virtual bool Validate()
    {
        ValidationResult validationResult =
            SignatureValidator.Validate((TSignature)this);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return validationResult.IsValid;
    }
}
