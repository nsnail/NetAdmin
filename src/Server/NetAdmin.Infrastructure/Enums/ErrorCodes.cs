namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     错误码
/// </summary>
[Export]
public enum ErrorCodes
{
    /// <summary>
    ///     成功
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Succeed))]
    Succeed = 0

   ,

    /// <summary>
    ///     意外错误
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.UnhandledError))]
    Unhandled = 9000

   ,

    /// <summary>
    ///     结果非预期
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.ResultUnexpected))]
    Unexpected = 9100

   ,

    /// <summary>
    ///     无效输入
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Invalid_input))]
    InvalidInput = 9200

   ,

    /// <summary>
    ///     签名缺失
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Signature_is_missing))]
    SignatureMissing = 9201

   ,

    /// <summary>
    ///     签名不正确
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Incorrect_signature))]
    IncorrectSignature = 9202

   ,

    /// <summary>
    ///     时间戳缺失或误差过大
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Timestamp_Error))]
    TimestampError = 9203

   ,

    /// <summary>
    ///     未指定AppId参数
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.AppId_parameter_not_specified))]
    AppIdMissing = 9204

   ,

    /// <summary>
    ///     信息头丢失
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Header_loss))]
    HeaderMissing = 9205

   ,

    /// <summary>
    ///     无效操作
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Invalid_operation))]
    InvalidOperation = 9300

   ,

    /// <summary>
    ///     机器人不存在
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Robots_do_not_exist))]
    RobotNotExists = 9301

   ,

    /// <summary>
    ///     机器人不在线
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Robots_do_not_exist))]
    RobotNotOnline = 9302

   ,

    /// <summary>
    ///     任务状态变更非法
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Task_status_change_is_illegal))]
    TaskStatusChangeIllegal = 9303

   ,

    /// <summary>
    ///     AppId不存在
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.AppId_does_not_exist))]
    AppIdNotExists = 9304

   ,

    /// <summary>
    ///     App密钥错误
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.App_secret_error))]
    AppSecretError = 9305

   ,

    /// <summary>
    ///     AppId被禁用
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.AppId_is_disabled))]
    AppIdDisabled = 9306

   ,

    /// <summary>
    ///     用户已被禁用
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.User_disabled))]
    UserDisabled = 9307
}