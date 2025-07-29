namespace NetAdmin.Domain.Dto.Sys.UserInvite;

/// <summary>
///     请求：设置上级
/// </summary>
public record SetInviterReq : Sys_UserInvite
{
    /// <inheritdoc cref="EntityBase{T}.Id" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Id { get; init; }

    /// <inheritdoc />
    [Required]
    [Range(1, long.MaxValue)]
    public override long? OwnerId { get; init; }

    /// <inheritdoc cref="IFieldVersion.Version" />
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public override long Version { get; init; }

    /// <inheritdoc />
    protected override IEnumerable<ValidationResult> ValidateInternal(ValidationContext validationContext)
    {
        if (OwnerId == Id) {
            yield return new ValidationResult(Ln.不能设置自己为上级, [nameof(OwnerId)]);
        }

        yield return ValidationResult.Success;
    }
}