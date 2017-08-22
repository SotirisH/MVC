namespace Aurora.SMS.Modeling.Interfaces
{
    /// <summary>
    /// Model for SmsTemplate
    /// </summary>
    public interface ISmsTemplateModel
    {
        string Name { get; set; }
        string Description { get; set; }
        string Text { get; set; }
    }
}
