using System.ComponentModel;
using Furion.FriendlyException;
using NetAdmin.Infrastructure.Attributes;
using NSExt.Attributes;

namespace NetAdmin.Infrastructure.Constant;

/// <summary>
///     基础枚举常量
/// </summary>
/// <remarks>
///     添加[Export]特性暴露给前端
/// </remarks>
public static class Enums
{
    /// <summary>
    ///     证件类型
    /// </summary>
    [Export]
    public enum CertificateTypes
    {
        /// <summary>
        ///     身份证
        /// </summary>
        [Description(nameof(Ln.Identity_card))]
        [Localization(typeof(Ln))]
        IdentityCard = 1

       ,

        /// <summary>
        ///     护照
        /// </summary>
        [Description(nameof(Ln.Passport))]
        [Localization(typeof(Ln))]
        Passport = 2

       ,

        /// <summary>
        ///     外国人居留证
        /// </summary>
        [Description(nameof(Ln.Alien_residence_permit))]
        [Localization(typeof(Ln))]
        ForeignerResidencePermit = 3

       ,

        /// <summary>
        ///     港澳台通行证
        /// </summary>
        [Description(nameof(Ln.Hong_Kong_Macao_and_Taiwan_Pass))]
        [Localization(typeof(Ln))]
        HKorMacauPermit = 4

       ,

        /// <summary>
        ///     出生证
        /// </summary>
        [Description(nameof(Ln.Birth_certificate))]
        [Localization(typeof(Ln))]
        BirthCertificate = 5
    }

    /// <summary>
    ///     学历
    /// </summary>
    [Export]
    public enum Educations
    {
        /// <summary>
        ///     小学
        /// </summary>
        [Description(nameof(Ln.Pupil))]
        [Localization(typeof(Ln))]
        Primary = 1

       ,

        /// <summary>
        ///     初中
        /// </summary>
        [Description(nameof(Ln.Junior_school_student))]
        [Localization(typeof(Ln))]
        Junior = 2

       ,

        /// <summary>
        ///     高中
        /// </summary>
        [Description(nameof(Ln.High_school))]
        [Localization(typeof(Ln))]
        Higher = 3

       ,

        /// <summary>
        ///     中专
        /// </summary>
        [Description(nameof(Ln.Technical_secondary_school_students))]
        [Localization(typeof(Ln))]
        Technical = 4

       ,

        /// <summary>
        ///     大专
        /// </summary>
        [Description(nameof(Ln.Junior_college_students))]
        [Localization(typeof(Ln))]
        College = 5

       ,

        /// <summary>
        ///     本科
        /// </summary>
        [Description(nameof(Ln.Undergraduate))]
        [Localization(typeof(Ln))]
        Bachelor = 6

       ,

        /// <summary>
        ///     硕士
        /// </summary>
        [Description(nameof(Ln.Postgraduate))]
        [Localization(typeof(Ln))]
        Master = 7

       ,

        /// <summary>
        ///     博士
        /// </summary>
        [Description(nameof(Ln.Doctoral_students))]
        [Localization(typeof(Ln))]
        Doctor = 8

       ,

        /// <summary>
        ///     博士后
        /// </summary>
        [Description(nameof(Ln.Post_doctoral))]
        [Localization(typeof(Ln))]
        Post = 9
    }

    /// <summary>
    ///     日志等级
    /// </summary>
    public enum LogLevels
    {
        /// <summary>
        ///     Trace
        /// </summary>
        [Description("[gray]TCE[/]")]
        Trace = 1

       ,

        /// <summary>
        ///     Debug
        /// </summary>
        [Description("[gray]DBG[/]")]
        Debug = 2

       ,

        /// <summary>
        ///     Information
        /// </summary>
        [Description("[green]INF[/]")]
        Information = 3

       ,

        /// <summary>
        ///     Warning
        /// </summary>
        [Description("[yellow]WRN[/]")]
        Warning = 4

       ,

        /// <summary>
        ///     Error
        /// </summary>
        [Description("[red]ERR[/]")]
        Error = 5

       ,

        /// <summary>
        ///     Critical
        /// </summary>
        [Description("[red]CTL[/]")]
        Critical = 6
    }

    /// <summary>
    ///     婚姻状况
    /// </summary>
    [Export]
    public enum MarriageStatues
    {
        /// <summary>
        ///     未婚
        /// </summary>
        [Description(nameof(Ln.Unmarried))]
        [Localization(typeof(Ln))]
        Unmarried = 1

       ,

        /// <summary>
        ///     已婚
        /// </summary>
        [Description(nameof(Ln.Married))]
        [Localization(typeof(Ln))]
        Married = 2

       ,

        /// <summary>
        ///     离异
        /// </summary>
        [Description(nameof(Ln.Divorced))]
        [Localization(typeof(Ln))]
        Divorced = 3

       ,

        /// <summary>
        ///     丧偶
        /// </summary>
        [Description(nameof(Ln.Berefted))]
        [Localization(typeof(Ln))]
        Berefted = 4
    }

    /// <summary>
    ///     模块类型
    /// </summary>
    [Export]
    public enum ModuleTypes
    {
        /// <summary>
        ///     系统模块
        /// </summary>
        [Description(nameof(Ln.System_module))]
        [Localization(typeof(Ln))]
        Sys = 1

       ,

        /// <summary>
        ///     业务模块
        /// </summary>
        [Description(nameof(Ln.Business_module))]
        [Localization(typeof(Ln))]
        Biz = 2
    }

    /// <summary>
    ///     民族
    /// </summary>
    [Export]
    public enum Nations
    {
        /// <summary>
        ///     汉族
        /// </summary>
        [Description(nameof(Ln.Nation_han))]
        [Localization(typeof(Ln))]
        Han = 1

       ,

        /// <summary>
        ///     壮族
        /// </summary>
        [Description(nameof(Ln.Nation_zhuang))]
        [Localization(typeof(Ln))]
        Zhuang = 2

       ,

        /// <summary>
        ///     满族
        /// </summary>
        [Description(nameof(Ln.Nation_manchu))]
        [Localization(typeof(Ln))]
        Manchu = 3

       ,

        /// <summary>
        ///     回族
        /// </summary>
        [Description(nameof(Ln.Nation_hui))]
        [Localization(typeof(Ln))]
        Hui = 4

       ,

        /// <summary>
        ///     苗族
        /// </summary>
        [Description(nameof(Ln.Nation_miao))]
        [Localization(typeof(Ln))]
        Miao = 5

       ,

        /// <summary>
        ///     维吾尔族
        /// </summary>
        [Description(nameof(Ln.Nation_uyghur))]
        [Localization(typeof(Ln))]
        Uyghur = 6

       ,

        /// <summary>
        ///     土家族
        /// </summary>
        [Description(nameof(Ln.Nation_tujia))]
        [Localization(typeof(Ln))]
        Tujia = 7

       ,

        /// <summary>
        ///     彝族
        /// </summary>
        [Description(nameof(Ln.Nation_yi))]
        [Localization(typeof(Ln))]
        Yi = 8

       ,

        /// <summary>
        ///     蒙古族
        /// </summary>
        [Description(nameof(Ln.Nation_mongolian))]
        [Localization(typeof(Ln))]
        Mongolian = 9

       ,

        /// <summary>
        ///     藏族
        /// </summary>
        [Description(nameof(Ln.Nation_tibetan))]
        [Localization(typeof(Ln))]
        Tibetan = 10

       ,

        /// <summary>
        ///     布依族
        /// </summary>
        [Description(nameof(Ln.Nation_buyei))]
        [Localization(typeof(Ln))]
        Buyei = 11

       ,

        /// <summary>
        ///     侗族
        /// </summary>
        [Description(nameof(Ln.Nation_dong))]
        [Localization(typeof(Ln))]
        Dong = 12

       ,

        /// <summary>
        ///     瑶族
        /// </summary>
        [Description(nameof(Ln.Nation_yao))]
        [Localization(typeof(Ln))]
        Yao = 13

       ,

        /// <summary>
        ///     朝鲜族
        /// </summary>
        [Description(nameof(Ln.Nation_korean))]
        [Localization(typeof(Ln))]
        Korean = 14

       ,

        /// <summary>
        ///     白族
        /// </summary>
        [Description(nameof(Ln.Nation_bai))]
        [Localization(typeof(Ln))]
        Bai = 15

       ,

        /// <summary>
        ///     哈尼族
        /// </summary>
        [Description(nameof(Ln.Nation_hani))]
        [Localization(typeof(Ln))]
        Hani = 16

       ,

        /// <summary>
        ///     哈萨克族
        /// </summary>
        [Description(nameof(Ln.Nation_kazakh))]
        [Localization(typeof(Ln))]
        Kazakh = 17

       ,

        /// <summary>
        ///     黎族
        /// </summary>
        [Description(nameof(Ln.Nation_li))]
        [Localization(typeof(Ln))]
        Li = 18

       ,

        /// <summary>
        ///     傣族
        /// </summary>
        [Description(nameof(Ln.Nation_dai))]
        [Localization(typeof(Ln))]
        Dai = 19

       ,

        /// <summary>
        ///     畲族
        /// </summary>
        [Description(nameof(Ln.Nation_she))]
        [Localization(typeof(Ln))]
        She = 20

       ,

        /// <summary>
        ///     傈僳族
        /// </summary>
        [Description(nameof(Ln.Nation_lisu))]
        [Localization(typeof(Ln))]
        Lisu = 21

       ,

        /// <summary>
        ///     仡佬族
        /// </summary>
        [Description(nameof(Ln.Nation_gelao))]
        [Localization(typeof(Ln))]
        Gelao = 22

       ,

        /// <summary>
        ///     东乡族
        /// </summary>
        [Description(nameof(Ln.Nation_dongxiang))]
        [Localization(typeof(Ln))]
        Dongxiang = 23

       ,

        /// <summary>
        ///     高山族
        /// </summary>
        [Description(nameof(Ln.Nation_gaoshan))]
        [Localization(typeof(Ln))]
        Gaoshan = 24

       ,

        /// <summary>
        ///     拉祜族族
        /// </summary>
        [Description(nameof(Ln.Nation_lahu))]
        [Localization(typeof(Ln))]
        Lahu = 25

       ,

        /// <summary>
        ///     水族
        /// </summary>
        [Description(nameof(Ln.Nation_shui))]
        [Localization(typeof(Ln))]
        Shui = 26

       ,

        /// <summary>
        ///     佤族
        /// </summary>
        [Description(nameof(Ln.Nation_va))]
        [Localization(typeof(Ln))]
        Va = 27

       ,

        /// <summary>
        ///     纳西族
        /// </summary>
        [Description(nameof(Ln.Nation_nakhi))]
        [Localization(typeof(Ln))]
        Nakhi = 28

       ,

        /// <summary>
        ///     羌族
        /// </summary>
        [Description(nameof(Ln.Nation_qiang))]
        [Localization(typeof(Ln))]
        Qiang = 29

       ,

        /// <summary>
        ///     土族
        /// </summary>
        [Description(nameof(Ln.Nation_monguor))]
        [Localization(typeof(Ln))]
        Monguor = 30

       ,

        /// <summary>
        ///     仫佬族
        /// </summary>
        [Description(nameof(Ln.Nation_mulao))]
        [Localization(typeof(Ln))]
        Mulao = 31

       ,

        /// <summary>
        ///     锡伯族
        /// </summary>
        [Description(nameof(Ln.Nation_xibe))]
        [Localization(typeof(Ln))]
        Xibe = 32

       ,

        /// <summary>
        ///     柯尔克孜族
        /// </summary>
        [Description(nameof(Ln.Nation_kyrgyz))]
        [Localization(typeof(Ln))]
        Kyrgyz = 33

       ,

        /// <summary>
        ///     达斡尔族
        /// </summary>
        [Description(nameof(Ln.Nation_daur))]
        [Localization(typeof(Ln))]
        Daur = 34

       ,

        /// <summary>
        ///     景颇族
        /// </summary>
        [Description(nameof(Ln.Nation_jingpo))]
        [Localization(typeof(Ln))]
        Jingpo = 35

       ,

        /// <summary>
        ///     毛南族
        /// </summary>
        [Description(nameof(Ln.Nation_maonan))]
        [Localization(typeof(Ln))]
        Maonan = 36

       ,

        /// <summary>
        ///     撒拉族
        /// </summary>
        [Description(nameof(Ln.Nation_salar))]
        [Localization(typeof(Ln))]
        Salar = 37

       ,

        /// <summary>
        ///     布朗族
        /// </summary>
        [Description(nameof(Ln.Nation_blang))]
        [Localization(typeof(Ln))]
        Blang = 38

       ,

        /// <summary>
        ///     塔吉克族
        /// </summary>
        [Description(nameof(Ln.Nation_tajik))]
        [Localization(typeof(Ln))]
        Tajik = 39

       ,

        /// <summary>
        ///     阿昌族
        /// </summary>
        [Description(nameof(Ln.Nation_achang))]
        [Localization(typeof(Ln))]
        Achang = 40

       ,

        /// <summary>
        ///     普米族
        /// </summary>
        [Description(nameof(Ln.Nation_pumi))]
        [Localization(typeof(Ln))]
        Pumi = 41

       ,

        /// <summary>
        ///     鄂温克族
        /// </summary>
        [Description(nameof(Ln.Nation_evenk))]
        [Localization(typeof(Ln))]
        Evenk = 42

       ,

        /// <summary>
        ///     怒族
        /// </summary>
        [Description(nameof(Ln.Nation_nu))]
        [Localization(typeof(Ln))]
        Nu = 43

       ,

        /// <summary>
        ///     京族
        /// </summary>
        [Description(nameof(Ln.Nation_kinh))]
        [Localization(typeof(Ln))]
        Kinh = 44

       ,

        /// <summary>
        ///     基诺族
        /// </summary>
        [Description(nameof(Ln.Nation_jino))]
        [Localization(typeof(Ln))]
        Jino = 45

       ,

        /// <summary>
        ///     德昂族
        /// </summary>
        [Description(nameof(Ln.Nation_deang))]
        [Localization(typeof(Ln))]
        Deang = 46

       ,

        /// <summary>
        ///     保安族
        /// </summary>
        [Description(nameof(Ln.Nation_bonan))]
        [Localization(typeof(Ln))]
        Bonan = 47

       ,

        /// <summary>
        ///     俄罗斯族
        /// </summary>
        [Description(nameof(Ln.Nation_russian))]
        [Localization(typeof(Ln))]
        Russian = 48

       ,

        /// <summary>
        ///     裕固族
        /// </summary>
        [Description(nameof(Ln.Nation_yughur))]
        [Localization(typeof(Ln))]
        Yughur = 49

       ,

        /// <summary>
        ///     乌孜别克族
        /// </summary>
        [Description(nameof(Ln.Nation_uzbek))]
        [Localization(typeof(Ln))]
        Uzbek = 50

       ,

        /// <summary>
        ///     门巴族
        /// </summary>
        [Description(nameof(Ln.Nation_monpa))]
        [Localization(typeof(Ln))]
        Monpa = 51

       ,

        /// <summary>
        ///     鄂伦春族
        /// </summary>
        [Description(nameof(Ln.Nation_oroqen))]
        [Localization(typeof(Ln))]
        Oroqen = 52

       ,

        /// <summary>
        ///     独龙族
        /// </summary>
        [Description(nameof(Ln.Nation_derung))]
        [Localization(typeof(Ln))]
        Derung = 53

       ,

        /// <summary>
        ///     塔塔尔族
        /// </summary>
        [Description(nameof(Ln.Nation_tatar))]
        [Localization(typeof(Ln))]
        Tatar = 54

       ,

        /// <summary>
        ///     赫哲族
        /// </summary>
        [Description(nameof(Ln.Nation_nanai))]
        [Localization(typeof(Ln))]
        Nanai = 55

       ,

        /// <summary>
        ///     珞巴族
        /// </summary>
        [Description(nameof(Ln.Nation_lhoba))]
        [Localization(typeof(Ln))]
        Lhoba = 56
    }

    /// <summary>
    ///     排序方式
    /// </summary>
    [Export]
    public enum Orders
    {
        /// <summary>
        ///     顺序
        /// </summary>
        [Description(nameof(Ln.Sort_in_order))]
        [Localization(typeof(Ln))]
        Ascending = 1

       ,

        /// <summary>
        ///     倒序
        /// </summary>
        [Description(nameof(Ln.Sort_in_reverse_order))]
        [Localization(typeof(Ln))]
        Descending = 2
    }

    /// <summary>
    ///     政治面貌
    /// </summary>
    [Export]
    public enum PoliticalStatues
    {
        /// <summary>
        ///     中共党员
        /// </summary>
        [Description(nameof(Ln.Member_of_the_Communist_Party_of_China))]
        [Localization(typeof(Ln))]
        MemberOfCommunistParty = 1

       ,

        /// <summary>
        ///     共青团员
        /// </summary>
        [Description(nameof(Ln.Communist_youth_league_member))]
        [Localization(typeof(Ln))]
        MemberOfCommunistYouthLeague = 2

       ,

        /// <summary>
        ///     群众
        /// </summary>
        [Description(nameof(Ln.The_masses))]
        [Localization(typeof(Ln))]
        CommonPeople = 3
    }

    /// <summary>
    ///     状态码
    /// </summary>
    [ErrorCodeType]
    [Export]
    public enum RspCodes
    {
        /// <summary>
        ///     成功
        /// </summary>
        [Description(nameof(Ln.Succeed))]
        [Localization(typeof(Ln))]
        Succeed = 0

       ,

        /// <summary>
        ///     未处理的异常
        /// </summary>
        /// <remarks>
        ///     通过全局异常过滤器捕获的问题
        /// </remarks>
        [ErrorCodeItemMetadata("{0}")]
        [Description(nameof(Ln.Unexpected))]
        [Localization(typeof(Ln))]
        Unexpected = 9000

       ,

        /// <summary>
        ///     无效输入
        /// </summary>
        /// <remarks>
        ///     请求参数格式不对、校验错误等问题
        /// </remarks>
        [ErrorCodeItemMetadata("{0}")]
        [Description(nameof(Ln.Invalid_input))]
        [Localization(typeof(Ln))]
        InvalidInput = 9100

       ,

        /// <summary>
        ///     无效操作
        /// </summary>
        /// <remarks>
        ///     不允许的操作，事务失败等问题
        /// </remarks>
        [ErrorCodeItemMetadata("{0}")]
        [Description(nameof(Ln.Invalid_operation))]
        [Localization(typeof(Ln))]
        InvalidOperation = 9200
    }

    /// <summary>
    ///     性别
    /// </summary>
    [Export]
    public enum Sexes
    {
        /// <summary>
        ///     男
        /// </summary>
        [Description(nameof(Ln.Male))]
        [Localization(typeof(Ln))]
        Male = 1

       ,

        /// <summary>
        ///     女
        /// </summary>
        [Description(nameof(Ln.Female))]
        [Localization(typeof(Ln))]
        Female = 2

       ,

        /// <summary>
        ///     保密
        /// </summary>
        [Description(nameof(Ln.Secrecy))]
        [Localization(typeof(Ln))]
        Secrecy = 3
    }
}