namespace NetAdmin.Domain.Enums.Tsk;

/// <summary>
///     任务指令
/// </summary>
[Export]
public enum TaskCmds
{
    /// <summary>
    ///     登录
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Login))]
    Login = 10000

   ,

    /// <summary>
    ///     加好友
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.AddFriend))]
    AddFriend = 10001

   ,

    /// <summary>
    ///     获取好友列表
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.GetFriendList))]
    GetFriendList = 10002

   ,

    /// <summary>
    ///     创建群聊
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.CreateGroup))]
    CreateGroup = 10003

   ,

    /// <summary>
    ///     获取群信息
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.GetGroupInfo))]
    GetGroupInfo = 10004

   ,

    /// <summary>
    ///     邀请好友入群
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.InviteFriendsToGroup))]
    InviteFriendsToGroup = 10005

   ,

    /// <summary>
    ///     退群
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.QuitGroup))]
    QuitGroup = 10006

   ,

    /// <summary>
    ///     发消息
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.SendMessage))]
    SendMessage = 10007

   ,

    /// <summary>
    ///     获取群列表
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.GetGroupList))]
    GetGroupList = 10008

   ,

    /// <summary>
    ///     退出登录
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Logout))]
    Logout = 10009

   ,

    /// <summary>
    ///     同步通讯录
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.SyncContacts))]
    SyncContacts = 10010

   ,

    /// <summary>
    ///     获取群链接
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.GetGroupInviteLink))]
    GetGroupInviteLink = 10011

   ,

    /// <summary>
    ///     通过群链接入群
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.AcceptGroupInviteLink))]
    AcceptGroupInviteLink = 10012

   ,

    /// <summary>
    ///     修改群名称
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.UpdateGroupName))]
    UpdateGroupName = 10013

   ,

    /// <summary>
    ///     根据手机号搜索好友
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Find_contact_by_phone))]
    FindContactByPhone = 10014

   ,

    /// <summary>
    ///     获取朋友圈
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Get_voom))]
    GetVoom = 10015

   ,

    /// <summary>
    ///     朋友圈发帖
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Post_voom))]
    PostVoom = 10016
}