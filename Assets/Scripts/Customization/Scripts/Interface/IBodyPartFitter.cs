public interface IBodyPartFitter
{
    public BodyPartType BodyPart { get; }
    public void Apply(CustomizationData data);
}