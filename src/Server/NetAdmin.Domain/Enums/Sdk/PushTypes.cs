namespace NetAdmin.Domain.Enums.Sdk;

/// <summary>
///     推送类型
/// </summary>
[Export]
public enum PushTypes
{
    /// <summary>
    ///     机器人心跳
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Robot_heart))]
    RobotHeart = 2000

   ,

    /// <summary>
    ///     机器人离线
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Robot_offline))]
    RobotOffline = 2001
}