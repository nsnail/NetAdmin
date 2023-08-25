// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace NetAdmin.Domain.Attributes.DataValidation;

/// <summary>
///     Url验证器
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter)]
public sealed class CultureUrlAttribute : RegexAttribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CultureUrlAttribute" /> class.
    /// </summary>
    public CultureUrlAttribute() //
        : base(Chars.RGX_URL)
    {
        ErrorMessageResourceName = nameof(Ln.无效网络地址);
        ErrorMessageResourceType = typeof(Ln);
    }
}