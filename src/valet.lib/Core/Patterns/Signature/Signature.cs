using FluentValidation;
using valet.lib.Core.Domain.Interfaces;
using ValidationException = valet.lib.Core.Exception.ValidationException;

namespace valet.lib.Core.Patterns.Signature;

/// <summary>
/// Provides a base implementation for validating signature objects using FluentValidation.
/// </summary>
/// <typeparam name="TSignature">
/// The concrete signature type. This type must inherit from
/// <see cref="Signature{TSignature, TValidator}"/> (self-referencing generic constraint).
/// </typeparam>
/// <typeparam name="TValidator">
/// The FluentValidation validator responsible for validating the signature.
/// </typeparam>
/// <remarks>
/// This abstraction applies the Signature Pattern combined with FluentValidation,
/// allowing signature objects to encapsulate their own validation rules.
/// Validation failures result in a domain-specific <see cref="ValidationException"/>.
/// </remarks>
public abstract class Signature<TSignature, TValidator> : ISignature
    where TSignature : Signature<TSignature, TValidator>
    where TValidator : AbstractValidator<TSignature>, new()
{
    /// <summary>
    /// The validator instance used to validate the signature.
    /// </summary>
    private static readonly TValidator SignatureValidator = new();

    /// <summary>
    /// Validates the current signature instance using the configured validator.
    /// </summary>
    /// <returns>
    /// <c>true</c> if the signature is valid.
    /// </returns>
    /// <exception cref="ValidationException">
    /// Thrown when the signature validation fails.
    /// The exception contains a list of validation error messages.
    /// </exception>
    public virtual bool Validate()
    {
        var validationResult =
            SignatureValidator.Validate((TSignature)this);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors.Select(error => error.ErrorMessage).ToList());

        return validationResult.IsValid;
    }
}
