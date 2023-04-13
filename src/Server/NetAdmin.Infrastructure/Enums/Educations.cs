namespace NetAdmin.Infrastructure.Enums;

/// <summary>
///     学历
/// </summary>
[Export]
public enum Educations
{
    /// <summary>
    ///     小学
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Pupil))]
    Primary = 1

   ,

    /// <summary>
    ///     初中
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Junior_school_student))]
    Junior = 2

   ,

    /// <summary>
    ///     高中
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.High_school))]
    Higher = 3

   ,

    /// <summary>
    ///     中专
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Technical_secondary_school_students))]
    Technical = 4

   ,

    /// <summary>
    ///     大专
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Junior_college_students))]
    College = 5

   ,

    /// <summary>
    ///     本科
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Undergraduate))]
    Bachelor = 6

   ,

    /// <summary>
    ///     硕士
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Postgraduate))]
    Master = 7

   ,

    /// <summary>
    ///     博士
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Doctoral_students))]
    Doctor = 8

   ,

    /// <summary>
    ///     博士后
    /// </summary>
    [ResourceDescription<Ln>(nameof(Ln.Post_doctoral))]
    Post = 9
}