namespace NetAdmin.Tests.Attributes;

/// <summary>
///     测试用例优先级
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class TestPriorityAttribute(int priority) : Attribute
{
    /// <summary>
    ///     优先级
    /// </summary>
    public int Priority { get; private set; } = priority;
}